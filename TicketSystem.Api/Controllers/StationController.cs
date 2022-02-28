using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Api.DtoParameters;
using TicketSystem.Api.Entities;
using TicketSystem.Api.Models;
using TicketSystem.Api.Services;

namespace TicketSystem.Api.Controllers;

[ApiController]
[Route("api/stations")]
public class StationController : ControllerBase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;

    public StationController(ITicketRepository stationRepository, IMapper mapper)
    {
        _ticketRepository = stationRepository;
        _mapper = mapper;
    }

    //获取所有Stations资源
    //ActionResult 类型表示多种 HTTP 状态代码，是控制器的返回类型
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StationOutputDto>>>
        GetStations([FromQuery] StationDtoParameters? parameters)
    {
        var stations = await _ticketRepository.GetStationsAsync(parameters);

        var stationDtos = _mapper.Map<IEnumerable<StationOutputDto>>(stations);

        return Ok(stationDtos);
    }

    [HttpGet("stationName",Name = nameof(GetStation))]
    public async Task<ActionResult<StationOutputDto>>
        GetStation(string stationName)
    {
        if (!await _ticketRepository.StationExistsAsync(stationName))
        {
            return NotFound();
        }

        var station = await _ticketRepository.GetStationAsync(stationName);
        if (station == null)
        {
            return NotFound();
        }

        var stationDto = _mapper.Map<StationOutputDto>(station);
        return Ok(stationDto);
    }

    [HttpPost]
    public async Task<ActionResult<StationOutputDto>>
        CreateStation(StationAddDto station)
    {
        var entity = _mapper.Map<Station>(station);
        _ticketRepository.AddStation(entity);
        await _ticketRepository.SaveAsync();

        var dtoToReturn = _mapper.Map<StationOutputDto>(entity);

        return CreatedAtRoute(nameof(GetStation), dtoToReturn);
    }
}

