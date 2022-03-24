using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using TicketSystem.Api.Entities;
using TicketSystem.Api.Models;
using TicketSystem.Api.Services;
using TicketSystem.Api.Utils;

namespace TicketSystem.Api.Controllers;

[ApiController]
[Route("api")]
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
        var cacheKey = "Distance_" + startTerminal + "_" + endTerminal + "_" + typeOfTrain;
        var redis = new RedisUtil(_distributedCache);
        DistanceOutputDto distanceDto;
        var redisByte = await _distributedCache.GetAsync(cacheKey);
        if (redisByte != null)
        {
            var distanceList = JsonConvert.DeserializeObject<DistanceOutputDto>(redis.RedisRead(redisByte));
            distanceDto = _mapper.Map<DistanceOutputDto>(distanceList);
        }
        else
        {
            if (string.IsNullOrWhiteSpace(stopStation))
            {
                return NotFound();
            }

            double distanceKm = _ticketRepository.GetDistance(stopStation, startTerminal, endTerminal);
            string time = _ticketRepository.GetTrainAsync(typeOfTrain).Result.Time;
            if (distanceKm == null)
            {
                return NotFound();
            }
            var price = _ticketRepository.GetPrice(distanceKm, typeOfTrain);
            var departureTime = _ticketRepository.GetDeparture(stopStation, startTerminal,time);
            if (departureTime == null)
            {
                return NotFound();
            }

            Distance distance = new Distance();
            distance.StationDistance = distanceKm;
            distance.Price = price;
            distance.DepartureTime = departureTime;

            distanceDto = _mapper.Map<DistanceOutputDto>(distance);
            redis.RedisSave(cacheKey, distanceDto);
        }

        return Ok(distanceDto);
    }
}