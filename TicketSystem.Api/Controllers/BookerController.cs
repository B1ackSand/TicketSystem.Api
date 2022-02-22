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
    private readonly IBookerRepository _bookerRepository;
    private readonly IMapper _mapper;

    public BookerController(IBookerRepository bookerRepository,IMapper mapper)
    {
        _bookerRepository = bookerRepository ?? throw new ArgumentNullException(nameof(bookerRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet(Name = nameof(GetBooker))]
    public async Task<ActionResult<BookerOutputDto>>
        GetBooker(string bookerWx)
    {
        if (!await _bookerRepository.BookerExistsAsync(bookerWx))
        {
            return NotFound();
        }

        var booker = await _bookerRepository.GetBookerAsync(bookerWx);
        if (booker == null)
        {
            return NotFound();
        }

        var bookerDto = _mapper.Map<BookerOutputDto>(booker);

        return Ok(bookerDto);
    }

    [HttpPost]
    public async Task<ActionResult<BookerOutputDto>> 
        CreateBooker(BookerAddDto booker)
    {
        var entity = _mapper.Map<Booker>(booker);
        _bookerRepository.AddBooker(entity);
        await _bookerRepository.SaveAsync();

        var dtoToReturn = _mapper.Map<BookerOutputDto>(entity);

        return CreatedAtRoute(nameof(GetBooker), dtoToReturn);
    }
}