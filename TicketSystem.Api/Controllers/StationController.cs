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
[Route("api/stations")]
public class StationController : ControllerBase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _distributedCache;

    public StationController(ITicketRepository stationRepository, IMapper mapper, IDistributedCache distributedCache)
    {
        _ticketRepository = stationRepository;
        _mapper = mapper;
        _distributedCache = distributedCache;
    }

    //获取所有Stations资源
    //ActionResult 类型表示多种 HTTP 状态代码，是控制器的返回类型
    //redis update
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StationOutputDto>>>
        GetStations([FromQuery] StationDtoParameters? parameters)
    {

        IEnumerable<StationOutputDto> stationDtos;
        var stations = await _ticketRepository.GetStationsAsync(parameters);
        stationDtos = _mapper.Map<IEnumerable<StationOutputDto>>(stations);
        return Ok(stationDtos);
    }


    //redis update
    [HttpGet("station")]
    public async Task<ActionResult<StationOutputDto>>
        GetStationById(int stationId)
    {
        var cacheKey = "Station_" + stationId;
        var redis = new RedisUtil(_distributedCache);
        StationOutputDto stationDto;
        var redisByte = await _distributedCache.GetAsync(cacheKey);
        if (redisByte != null)
        {
            var stationList = JsonConvert.DeserializeObject<Station>(redis.RedisRead(redisByte));
            stationDto = _mapper.Map<StationOutputDto>(stationList);
        }
        else
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

            stationDto = _mapper.Map<StationOutputDto>(station);
            redis.RedisSave(cacheKey, stationDto);
        }

        return Ok(stationDto);
    }

    //redis update
    [HttpGet("stationName", Name = "GetStationByName")]
    public async Task<ActionResult<StationOutputDto>>
        GetStationByName(string stationName)
    {
        var cacheKey = "Station_" + stationName;
        StationOutputDto stationDto;
        var redis = new RedisUtil(_distributedCache);
        var redisByte = await _distributedCache.GetAsync(cacheKey);
        if (redisByte != null)
        {
            var stationList = JsonConvert.DeserializeObject<Station>(redis.RedisRead(redisByte));
            stationDto = _mapper.Map<StationOutputDto>(stationList);
        }
        else
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

            stationDto = _mapper.Map<StationOutputDto>(station);
            redis.RedisSave(cacheKey, stationDto);
        }

        return Ok(stationDto);
    }


    //不需要升级redis
    [HttpPost]
    public async Task<ActionResult<StationOutputDto>>
        CreateStation(StationAddDto station)
    {
        var entity = _mapper.Map<Station>(station);
        _ticketRepository.AddStation(entity);
        await _ticketRepository.SaveAsync();

        var dtoToReturn = _mapper.Map<StationOutputDto>(entity);

        return CreatedAtRoute("GetStationByName",
            new
            {
                stationName = dtoToReturn.StationName
            }, dtoToReturn);
    }


    //redis update
    [HttpPut("updateStation")]
    public async Task<ActionResult<StationAddDto>> UpdateStation(int stationId, StationAddDto station)
    {
        var stationEntity = await _ticketRepository.GetStationAsync(stationId);
        var redis = new RedisUtil(_distributedCache);

        if (stationEntity == null)
        {
            //没获取就用put创建资源
            var stationToAddEntity = _mapper.Map<Station>(station);

            _ticketRepository.AddStation(stationToAddEntity);

            await _ticketRepository.SaveAsync();
            var dtoToReturn = _mapper.Map<StationOutputDto>(stationToAddEntity);

            redis.RedisSave("Station_" + stationId, dtoToReturn);

            return CreatedAtRoute(nameof(GetStationByName), new
            {
                stationName = dtoToReturn.StationName
            }, dtoToReturn);
        }

        _mapper.Map(station, stationEntity);
        _ticketRepository.UpdateStation(stationEntity);
        await _ticketRepository.SaveAsync();

        var redisFlesh = _mapper.Map<StationOutputDto>(stationEntity);
        redis.RedisSave("Station_" + stationId, redisFlesh);
        // 204 无需返回资源（根据实际情况决定）
        return NoContent();
    }


    //redis update
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
        var redis = new RedisUtil(_distributedCache);
        redis.RedisRemove("Station_" + stationId);

        await _ticketRepository.SaveAsync();

        return NoContent();
    }
}

