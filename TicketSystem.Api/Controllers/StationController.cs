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

    [HttpGet("station")]
    public async Task<ActionResult<StationOutputDto>>
        GetStation(int stationId)
    {
        if (!await _ticketRepository.StationExistsAsync(stationId))
        {
            return NotFound();
        }

        var station = await _ticketRepository.GetStationAsync(stationId);
        if (station == null)
        {
            return NotFound();
        }

        var stationDto = _mapper.Map<StationOutputDto>(station);
        return Ok(stationDto);
    }

    [HttpGet("stationName")]
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

    [HttpPut("updateStation")]
    public async Task<ActionResult<StationAddDto>> UpdateStation(int stationId,StationAddDto station)
    {
        var stationEntity = await _ticketRepository.GetStationAsync(stationId);

        if (stationEntity == null)
        {
            //没获取就用put创建资源
            var stationToAddEntity = _mapper.Map<Station>(station);

            _ticketRepository.AddStation(stationToAddEntity);

            await _ticketRepository.SaveAsync();
            var dtoToReturn = _mapper.Map<StationOutputDto>(stationToAddEntity);

            return CreatedAtRoute(nameof(GetStation), new
            {
                stationName = dtoToReturn.StationName
            }, dtoToReturn);
        }

        _mapper.Map(station, stationEntity);
        _ticketRepository.UpdateStation(stationEntity);

        await _ticketRepository.SaveAsync();

        // 204 无需返回资源（根据实际情况决定）
        return NoContent();
    }


    [HttpDelete("deleteStation")]
    public async Task<IActionResult> DeleteStation(int stationId)
    {
        var stationEntity = await _ticketRepository.GetStationAsync(stationId);

        if (stationEntity == null)
        {
            return NotFound();
        }

        await _ticketRepository.GetStationAsync(stationId);
        _ticketRepository.DeleteStation(stationEntity);

        await _ticketRepository.SaveAsync();

        return NoContent();
    }
}

