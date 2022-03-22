using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using TicketSystem.Api.DtoParameters;
using TicketSystem.Api.Entities;
using TicketSystem.Api.Models;
using TicketSystem.Api.Services;
using TicketSystem.Api.Utils;

namespace TicketSystem.Api.Controllers;

[ApiController]
[Route("api/bookers")]
public class BookerController : ControllerBase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _distributedCache;

    public BookerController(ITicketRepository ticketRepository, IMapper mapper, IDistributedCache distributedCache)
    {
        _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
    }

    //redis update
    [HttpGet(Name = nameof(GetBooker))]
    public async Task<ActionResult<BookerOutputDto>>
        GetBooker(string phoneNum)
    {
        var cacheKey = "Booker_" + phoneNum;
        var redis = new RedisUtil(_distributedCache);
        BookerOutputDto bookerDto;
        var redisBookerByte = await _distributedCache.GetAsync(cacheKey);
        if (redisBookerByte != null)
        {
            var bookerList = JsonConvert.DeserializeObject<Booker>(redis.RedisRead(redisBookerByte));
            bookerDto = _mapper.Map<BookerOutputDto>(bookerList);
        }
        else
        {
            if (!await _ticketRepository.BookerExistsAsync(phoneNum))
            {
                return NotFound();
            }

            var booker = await _ticketRepository.GetBookerAsync(phoneNum);
            if (booker == null)
            {
                return NotFound();
            }

            bookerDto = _mapper.Map<BookerOutputDto>(booker);
            redis.RedisSave("Booker_" + bookerDto.PhoneNum, bookerDto);
        }

        return Ok(bookerDto);
    }


    //redis update
    [HttpGet("GetAllBookers")]
    public async Task<ActionResult<BookerDto>> GetAllBookers([FromQuery] PageDtoParameters? parameters)
    {
        var cacheKey = "BookerList";
        IEnumerable<BookerDto> bookerDto;
        var redis = new RedisUtil(_distributedCache);
        var redisBookerByte = await _distributedCache.GetAsync(cacheKey);
        if (redisBookerByte != null)
        {
            var bookerList = JsonConvert.DeserializeObject<List<Booker>>(redis.RedisRead(redisBookerByte));
            bookerDto = _mapper.Map<IEnumerable<BookerDto>>(bookerList);
        }
        else
        {
            var bookers = await _ticketRepository.GetBookersAsync(parameters);
            if (bookers == null)
            {
                return NotFound();
            }
            bookerDto = _mapper.Map<IEnumerable<BookerDto>>(bookers);
            redis.RedisSave(cacheKey, bookerDto);
        }
        return Ok(bookerDto);
    }

    //不需要升级
    [HttpPost("login")]
    public async Task<ActionResult<BookerOutputDto>> BookerLogin(BookerLoginDto booker)
    {
        var entity = _mapper.Map<Booker>(booker);
        if (!await _ticketRepository.BookerExistsAsync(entity.PhoneNum))
        {
            return NotFound();
        }

        if (!await _ticketRepository.BookerPwdVerify(entity))
        {
            return Unauthorized();
        }

        var bookerImf = await _ticketRepository.GetBookerAsync(entity.PhoneNum);

        var bookerDto = _mapper.Map<BookerOutputDto>(bookerImf);
        return Ok(bookerDto);
    }

    //redis update
    [HttpPost]
    public async Task<ActionResult<BookerOutputDto>>
        CreateBooker(BookerAddDto booker)
    {
        var entity = _mapper.Map<Booker>(booker);
        _ticketRepository.AddBooker(entity);
        await _ticketRepository.SaveAsync();

        var dtoToReturn = _mapper.Map<BookerOutputDto>(entity);

        var redis = new RedisUtil(_distributedCache);
        redis.RedisSave("Booker_" + dtoToReturn.bookerId, dtoToReturn);
        redis.RedisRemove("BookerList");

        return CreatedAtRoute(nameof(GetBooker), dtoToReturn);
    }

    //redis update
    [HttpPut("updateBooker")]
    public async Task<ActionResult<BookerAddDto>> UpdateBooker(int bookerId, BookerAddDto booker)
    {
        var bookerEntity = await _ticketRepository.GetBookerAsync(bookerId);
        var redis = new RedisUtil(_distributedCache);

        if (bookerEntity == null)
        {
            //没获取就用put创建资源
            var bookerToAddEntity = _mapper.Map<Booker>(booker);

            _ticketRepository.AddBooker(bookerToAddEntity);

            await _ticketRepository.SaveAsync();
            var dtoToReturn = _mapper.Map<BookerOutputDto>(bookerToAddEntity);

            redis.RedisSave("Booker_" + bookerId, dtoToReturn);

            return CreatedAtRoute(nameof(GetBooker), dtoToReturn);
        }

        _mapper.Map(booker, bookerEntity);
        _ticketRepository.UpdateBooker(bookerEntity);
        var redisFlesh = _mapper.Map<BookerOutputDto>(bookerEntity);

        redis.RedisSave("Booker_" + bookerId, redisFlesh);

        await _ticketRepository.SaveAsync();

        // 204 无需返回资源（根据实际情况决定）
        return NoContent();
    }


    //redis update
    [HttpDelete("deleteBooker")]
    public async Task<IActionResult> DeleteBooker(int bookerId)
    {
        var bookerEntity = await _ticketRepository.GetBookerAsync(bookerId);

        if (bookerEntity == null)
        {
            return NotFound();
        }

        await _ticketRepository.GetBookerAsync(bookerId);
        _ticketRepository.DeleteBooker(bookerEntity);

        await _ticketRepository.SaveAsync();
        var redis = new RedisUtil(_distributedCache);
        redis.RedisRemove("BookerList");
        redis.RedisRemove("Booker_" + bookerId);

        return NoContent();
    }
}