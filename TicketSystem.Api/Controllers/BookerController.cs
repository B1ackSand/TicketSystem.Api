using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Api.Entities;
using TicketSystem.Api.Models;
using TicketSystem.Api.Services;

namespace TicketSystem.Api.Controllers;

[ApiController]
[Route("api/bookers")]
public class BookerController: ControllerBase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;

    public BookerController(ITicketRepository ticketRepository,IMapper mapper)
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
}