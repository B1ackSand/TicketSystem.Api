using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using TicketSystem.Api.DtoParameters;
using TicketSystem.Api.Entities;
using TicketSystem.Api.Models;
using TicketSystem.Api.Services;
using TicketSystem.Api.Utils;

namespace TicketSystem.Api.Controllers
{

    [ApiController]
    [Route("api")]
    public class OrderController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;

        public OrderController(ITicketRepository ticketRepository, IMapper mapper, IDistributedCache distributedCache)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _distributedCache = distributedCache;
        }


        //redis update
        [HttpGet("bookers/{bookerId}/orders", Name = nameof(GetAllHistoryOrdersForBooker))]
        public async Task<ActionResult<IEnumerable<OrderOutputDto>>> GetAllHistoryOrdersForBooker(int bookerId)
        {
            var cacheKey = "OrderList_" + bookerId;
            IEnumerable<OrderOutputDto> orderDto;
            var redis = new RedisUtil(_distributedCache);
            var redisByte = await _distributedCache.GetAsync(cacheKey);
            if (redisByte != null)
            {
                var orderList = JsonConvert.DeserializeObject<List<Order>>(redis.RedisRead(redisByte));
                orderDto = _mapper.Map<IEnumerable<OrderOutputDto>>(orderList);
            }
            else
            {
                if (!await _ticketRepository.BookerExistsAsync(bookerId))
                {
                    return NotFound();
                }

                var orders = await _ticketRepository.GetOrdersAsync(bookerId);
                if (orders == null)
                {
                    return NotFound();
                }

                orderDto = _mapper.Map<IEnumerable<OrderOutputDto>>(orders);
                redis.RedisSave(cacheKey, orderDto);
            }

            return Ok(orderDto);
        }


        //redis update
        [HttpGet("bookers/{bookerId}/orders/{orderId}", Name = nameof(GetOrderForBooker))]
        public async Task<ActionResult<OrderDto>> GetOrderForBooker(int bookerId, Guid orderId)
        {
            var cacheKey = "Order_" + bookerId;
            var redis = new RedisUtil(_distributedCache);
            OrderDto orderDto;
            var redisByte = await _distributedCache.GetAsync(cacheKey);
            if (redisByte != null)
            {
                var orderList = JsonConvert.DeserializeObject<Order>(redis.RedisRead(redisByte));
                orderDto = _mapper.Map<OrderDto>(orderList);
            }
            else
            {
                if (!await _ticketRepository.BookerExistsAsync(bookerId))
                {
                    return NotFound();
                }

                var order = await _ticketRepository.GetOrderAsync(bookerId, orderId);
                if (order == null)
                {
                    return NotFound();
                }

                orderDto = _mapper.Map<OrderDto>(order);
                redis.RedisSave(cacheKey, orderDto);
            }

            return Ok(orderDto);
        }


        [HttpGet("getAllOrders", Name = nameof(GetOrders))]
        public async Task<ActionResult<OrderOutputDto>>
            GetOrders([FromQuery] PageDtoParameters? parameters)
        {
            IEnumerable<OrderOutputDto> ordersDto;
            var orders = await _ticketRepository.GetOrdersAsync(parameters);
            if (orders == null)
            {
                return NotFound();
            }
            ordersDto = _mapper.Map<IEnumerable<OrderOutputDto>>(orders);
            return Ok(ordersDto);
        }

        //redis remove action
        [HttpPost("bookers/{bookerId}/orders")]
        public async Task<ActionResult<OrderAddDto>> CreateOrderForBooker(int bookerId, OrderAddDto order)
        {
            var entity = _mapper.Map<Order>(order);
            var redis = new RedisUtil(_distributedCache);
            _ticketRepository.AddOrder(bookerId, entity);
            await _ticketRepository.SaveAsync();

            var dtoToReturn = _mapper.Map<OrderDto>(entity);
            redis.RedisSave("Order_" + bookerId, dtoToReturn);
            redis.RedisRemove("OrderList_" + bookerId);

            return CreatedAtRoute(nameof(GetOrderForBooker),
                new
                {
                    bookerId = dtoToReturn.BookerId,
                    orderId = dtoToReturn.OrderId
                }, dtoToReturn);
        }

        //redis update
        [HttpPut("orders/updateOrder")]
        public async Task<ActionResult<OrderAddDto>> UpdateOrder(Guid orderId, int bookerId, OrderAddDto order)
        {
            var orderEntity = await _ticketRepository.GetOrderAsync(bookerId, orderId);
            var redis = new RedisUtil(_distributedCache);

            if (orderEntity == null)
            {
                //没获取就用put创建资源
                var orderToAddEntity = _mapper.Map<Order>(order);

                _ticketRepository.AddOrder(bookerId, orderToAddEntity);

                await _ticketRepository.SaveAsync();
                var dtoToReturn = _mapper.Map<OrderDto>(orderToAddEntity);

                redis.RedisSave("Order_" + bookerId, dtoToReturn);

                return CreatedAtRoute(nameof(GetOrderForBooker), new
                {
                    bookerId = dtoToReturn.BookerId,
                    orderId = dtoToReturn.OrderId
                }, dtoToReturn);
            }

            _mapper.Map(order, orderEntity);
            _ticketRepository.UpdateOrder(orderEntity);
            var redisFlesh = _mapper.Map<OrderDto>(orderEntity);

            redis.RedisSave("Order_" + bookerId, redisFlesh);

            await _ticketRepository.SaveAsync();

            // 204 无需返回资源（根据实际情况决定）
            return NoContent();
        }


        //redis update
        [HttpDelete("orders/deleteOrder")]
        public async Task<IActionResult> DeleteOrder(int bookerId, Guid orderId)
        {
            var orderEntity = await _ticketRepository.GetOrderAsync(bookerId, orderId);

            if (orderEntity == null)
            {
                return NotFound();
            }

            await _ticketRepository.GetOrderAsync(bookerId, orderId);
            _ticketRepository.DeleteOrder(orderEntity);

            await _ticketRepository.SaveAsync();
            var redis = new RedisUtil(_distributedCache);
            redis.RedisRemove("OrderList_" + bookerId);
            redis.RedisRemove("Order_" + bookerId);

            return NoContent();
        }
    }
}
