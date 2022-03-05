using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Api.DtoParameters;
using TicketSystem.Api.Entities;
using TicketSystem.Api.Models;
using TicketSystem.Api.Services;

namespace TicketSystem.Api.Controllers
{

    [ApiController]
    [Route("api")]
    public class OrderController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public OrderController(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet("bookers/{bookerId}/orders", Name = nameof(GetAllHistoryOrdersForBooker))]
        public async Task<ActionResult<IEnumerable<OrderOutputDto>>> GetAllHistoryOrdersForBooker(Guid bookerId)
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

            var orderDto = _mapper.Map<IEnumerable<OrderOutputDto>>(orders);
            return Ok(orderDto);
        }

        [HttpGet("bookers/{bookerId}/orders/{orderId}",Name = nameof(GetOrderForBooker))]
        // [ActionName("GetOrderForBooker")]
        public async Task<ActionResult<OrderDto>> GetOrderForBooker(Guid bookerId, Guid orderId)
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

            var orderDto = _mapper.Map<OrderDto>(order);

            return Ok(orderDto);
        }

        [HttpGet("getAllOrders", Name = nameof(GetOrders))]
        public async Task<ActionResult<OrderDto>>
            GetOrders([FromQuery] PageDtoParameters? parameters)
        {
            var orders = await _ticketRepository.GetOrdersAsync(parameters);
            if (orders == null)
            {
                return NotFound();
            }

            var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orders);

            return Ok(ordersDto);
        }

        [HttpPost("bookers/{bookerId}/orders")]
        public async Task<ActionResult<OrderAddDto>> CreateOrderForBooker(Guid bookerId, OrderAddDto order)
        {
            var entity = _mapper.Map<Order>(order);
            _ticketRepository.AddOrder(bookerId,entity);
            await _ticketRepository.SaveAsync();

            var dtoToReturn = _mapper.Map<OrderDto>(entity);

            return CreatedAtRoute(nameof(GetOrderForBooker),
                new
                {
                    bookerId = dtoToReturn.BookerId,
                    orderId = dtoToReturn.OrderId
                }, dtoToReturn);
        }

        [HttpPut("orders/updateOrder")]
        public async Task<ActionResult<OrderAddDto>> UpdateLine(Guid orderId,Guid bookerId, OrderAddDto order)
        {
            var orderEntity = await _ticketRepository.GetOrderAsync(bookerId,orderId);

            if (orderEntity == null)
            {
                //没获取就用put创建资源
                var orderToAddEntity = _mapper.Map<Order>(order);

                _ticketRepository.AddOrder(bookerId,orderToAddEntity);

                await _ticketRepository.SaveAsync();
                var dtoToReturn = _mapper.Map<OrderDto>(orderToAddEntity);

                return CreatedAtRoute(nameof(GetOrderForBooker), new
                {
                    bookerId = dtoToReturn.BookerId,
                    orderId = dtoToReturn.OrderId
                },dtoToReturn);
            }

            _mapper.Map(order, orderEntity);
            _ticketRepository.UpdateOrder(orderEntity);

            await _ticketRepository.SaveAsync();

            // 204 无需返回资源（根据实际情况决定）
            return NoContent();
        }


        [HttpDelete("orders/deleteOrder")]
        public async Task<IActionResult> DeleteOrder(Guid bookerId,Guid orderId)
        {
            var orderEntity = await _ticketRepository.GetOrderAsync(bookerId,orderId);

            if (orderEntity == null)
            {
                return NotFound();
            }

            await _ticketRepository.GetOrderAsync(bookerId,orderId);
            _ticketRepository.DeleteOrder(orderEntity);

            await _ticketRepository.SaveAsync();

            return NoContent();
        }
    }
}
