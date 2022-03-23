using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using TicketSystem.Api.Entities;
using TicketSystem.Api.Models;
using TicketSystem.Api.Services;
using TicketSystem.Api.Utils;

namespace TicketSystem.Api.Controllers;

public class DistanceController : ControllerBase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _distributedCache;

    public DistanceController(ITicketRepository stationRepository, IMapper mapper, IDistributedCache distributedCache)
    {
        _ticketRepository = stationRepository;
        _mapper = mapper;
        _distributedCache = distributedCache;
    }

    [HttpGet("getDistance")]
    public async Task<ActionResult<DistanceOutputDto>> DistanceCal(string stopStation, string startTerminal, string endTerminal, string typeOfTrain)
    {
        var cacheKey = "Distance_" + stopStation;
        var redis = new RedisUtil(_distributedCache);
        DistanceOutputDto distanceDto;
        var redisByte = await _distributedCache.GetAsync(cacheKey);
        if (redisByte != null)
        {
            var distanceList = JsonConvert.DeserializeObject<Distance>(redis.RedisRead(redisByte));
            distanceDto = _mapper.Map<DistanceOutputDto>(distanceList);
        }
        else
        {
            if (string.IsNullOrWhiteSpace(stopStation))
            {
                return NotFound();
            }

            double distanceKm = _ticketRepository.GetDistance(stopStation, startTerminal, endTerminal);
            if (distanceKm == null)
            {
                return NotFound();
            }
            var price = _ticketRepository.GetPrice(distanceKm, typeOfTrain);

            Distance distance = new Distance();
            distance.StationDistance = distanceKm;
            distance.Price = price;

            distanceDto = _mapper.Map<DistanceOutputDto>(distance);
            redis.RedisSave(cacheKey, distanceDto);
        }

        return Ok(distanceDto);
    }
}