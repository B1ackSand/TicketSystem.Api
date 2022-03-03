using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Api.DtoParameters;
using TicketSystem.Api.Entities;
using TicketSystem.Api.Models;
using TicketSystem.Api.Services;

namespace TicketSystem.Api.Controllers;

[ApiController]
[Route("api/bookers")]
public class BookerController : ControllerBase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;

    public BookerController(ITicketRepository ticketRepository, IMapper mapper)
    {
        _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet(Name = nameof(GetBooker))]
    public async Task<ActionResult<BookerOutputDto>>
        GetBooker(string phoneNum)
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

        var bookerDto = _mapper.Map<BookerOutputDto>(booker);

        return Ok(bookerDto);
    }

    [HttpGet("getAllBookers",Name = nameof(GetBookers))]
    public async Task<ActionResult<BookerDto>>
        GetBookers([FromQuery] PageDtoParameters? parameters)
    {
        var bookers = await _ticketRepository.GetBookersAsync(parameters);
        if (bookers == null)
        {
            return NotFound();
        }

        var bookerDto = _mapper.Map<IEnumerable<BookerDto>>(bookers);

        return Ok(bookerDto);
    }

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

    [HttpPost]
    public async Task<ActionResult<BookerOutputDto>>
        CreateBooker(BookerAddDto booker)
    {
        var entity = _mapper.Map<Booker>(booker);
        _ticketRepository.AddBooker(entity);
        await _ticketRepository.SaveAsync();

        var dtoToReturn = _mapper.Map<BookerOutputDto>(entity);

        return CreatedAtRoute(nameof(GetBooker), dtoToReturn);
    }

    [HttpPut("updateBooker")]
    public async Task<ActionResult<BookerAddDto>> UpdateBooker(Guid bookerId,BookerAddDto booker)
    {
        var bookerEntity = await _ticketRepository.GetBookerAsync(bookerId);

        if (bookerEntity == null)
        {
            //没获取就用put创建资源
            var bookerToAddEntity = _mapper.Map<Booker>(booker);
            
            _ticketRepository.AddBooker(bookerToAddEntity);

            await _ticketRepository.SaveAsync();
            var dtoToReturn = _mapper.Map<BookerOutputDto>(bookerToAddEntity);

            return CreatedAtRoute(nameof(GetBooker),dtoToReturn);
        }

        _mapper.Map(booker, bookerEntity);
        _ticketRepository.UpdateBooker(bookerEntity);

        await _ticketRepository.SaveAsync();

        // 204 无需返回资源（根据实际情况决定）
        return NoContent();
    }


    [HttpDelete("deleteBooker")]
    public async Task<IActionResult> DeleteBooker(Guid bookerId)
    {
        var bookerEntity = await _ticketRepository.GetBookerAsync(bookerId);

        if (bookerEntity == null)
        {
            return NotFound();
        }

        await _ticketRepository.GetBookerAsync(bookerId);
        _ticketRepository.DeleteBooker(bookerEntity);

        await _ticketRepository.SaveAsync();

        return NoContent();
    }
}