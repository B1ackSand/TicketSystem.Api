using System.Collections;
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
[Route("api")]
public class TrainController : ControllerBase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _distributedCache;

    public TrainController(ITicketRepository trainRepository, IMapper mapper, IDistributedCache distributedCache)
    {
        _ticketRepository = trainRepository ?? throw new ArgumentNullException(nameof(trainRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
    }


    //redis update
    [HttpGet("train")]
    public async Task<ActionResult<TrainOutputDto>> GetTrain(string trainName)
    {
        var cacheKey = "Train_" + trainName;
        var redis = new RedisUtil(_distributedCache);
        TrainOutputDto trainDto;
        var redisByte = await _distributedCache.GetAsync(cacheKey);
        if (redisByte != null)
        {
            var trainList = JsonConvert.DeserializeObject<Train>(redis.RedisRead(redisByte));
            trainDto = _mapper.Map<TrainOutputDto>(trainList);
        }
        else
        {
            if (!await _ticketRepository.TrainExistsAsync(trainName))
            {
                return NotFound();
            }

            var train = await _ticketRepository.GetTrainAsync(trainName);

            if (train == null)
            {
                return NotFound();
            }

            trainDto = _mapper.Map<TrainOutputDto>(train);
            redis.RedisSave("Train_" + trainName, trainDto);
        }

        return Ok(trainDto);
    }


    //redis update
    [HttpGet("trains/{trainId}", Name = nameof(GetTrainDetail))]
    public async Task<ActionResult<TrainOutputDto>> GetTrainDetail(int trainId)
    {
        var cacheKey = "Train_" + trainId;
        var redis = new RedisUtil(_distributedCache);
        TrainOutputDto trainDto;
        var redisByte = await _distributedCache.GetAsync(cacheKey);

        if (redisByte != null)
        {
            var trainList = JsonConvert.DeserializeObject<Train>(redis.RedisRead(redisByte));
            trainDto = _mapper.Map<TrainOutputDto>(trainList);
        }
        else
        {
            if (!await _ticketRepository.TrainExistsAsync(trainId))
            {
                return NotFound();
            }

            var train = await _ticketRepository.GetTrainDetailAsync(trainId);

            if (train == null)
            {
                return NotFound();
            }

            trainDto = _mapper.Map<TrainOutputDto>(train);
            redis.RedisSave("Train_" + trainId, trainDto);
        }

        return Ok(trainDto);
    }


    //redis update
    [HttpGet("lines/{lineId}/trains/{trainId}", Name = nameof(GetTrainForLine))]
    public async Task<ActionResult<TrainOutputDto>> GetTrainForLine(int lineId, int trainId)
    {
        var cacheKey = "Train_" + trainId;
        var redis = new RedisUtil(_distributedCache);
        TrainOutputDto trainDto;
        var redisByte = await _distributedCache.GetAsync(cacheKey);
        if (redisByte != null)
        {
            var trainList = JsonConvert.DeserializeObject<Train>(redis.RedisRead(redisByte));
            trainDto = _mapper.Map<TrainOutputDto>(trainList);
        }
        else
        {
            if (!await _ticketRepository.LineExistsAsync(lineId))
            {
                return NotFound();
            }

            if (!await _ticketRepository.TrainExistsAsync(trainId))
            {
                return NotFound();
            }

            var train = await _ticketRepository.GetTrainAsync(lineId, trainId);
            if (train == null)
            {
                return NotFound();
            }
            trainDto = _mapper.Map<TrainOutputDto>(train);
            redis.RedisSave("Train_" + trainId, trainDto);
        }

        return Ok(trainDto);
    }


    //redis update
    [HttpGet("getAllTrains", Name = nameof(GetTrains))]
    public async Task<ActionResult<BookerOutputDto>>
        GetTrains([FromQuery] PageDtoParameters? parameters)
    {
        IEnumerable<TrainOutputDto> trainsDtos;
        var trains = await _ticketRepository.GetTrainsAsync(parameters);
        if (trains == null)
        {
            return NotFound();
        }
        trainsDtos = _mapper.Map<IEnumerable<TrainOutputDto>>(trains);
        return Ok(trainsDtos);
    }

    //不需要升级redis
    //需要做长度验证错误处理
    [HttpPost("lines/{lineId}/trains")]
    public async Task<ActionResult<TrainOutputDto>>
        CreateTrain(int lineId, TrainAddDto train)
    {
        var entity = _mapper.Map<Train>(train);


        _ticketRepository.AddTrain(lineId, entity);
        await _ticketRepository.SaveAsync();

        var dtoToReturn = _mapper.Map<TrainOutputDto>(entity);

        return CreatedAtRoute(nameof(GetTrainDetail), new
        {
            trainId = dtoToReturn.TrainId
        }, dtoToReturn);
    }


    //redis update
    [HttpPut("trains/updateTrain")]
    public async Task<ActionResult<TrainAddDto>> UpdateTrain(int trainId, TrainUpdateDto train)
    {
        var trainEntity = await _ticketRepository.GetTrainDetailAsync(trainId);
        var redis = new RedisUtil(_distributedCache);

        if (trainEntity == null)
        {
            //没获取就用put创建资源
            var trainToAddEntity = _mapper.Map<Train>(train);

            _ticketRepository.AddTrain(trainToAddEntity);

            await _ticketRepository.SaveAsync();
            var dtoToReturn = _mapper.Map<TrainOutputDto>(trainToAddEntity);

            redis.RedisSave("Train_" + trainId, dtoToReturn);

            return CreatedAtRoute(nameof(GetTrainDetail), new
            {
                trainId = dtoToReturn.TrainId
            }, dtoToReturn);
        }

        _mapper.Map(train, trainEntity);
        _ticketRepository.UpdateTrain(trainEntity);
        var redisFlesh = _mapper.Map<TrainOutputDto>(trainEntity);

        redis.RedisSave("Train_" + trainId, redisFlesh);

        await _ticketRepository.SaveAsync();

        // 204 无需返回资源（根据实际情况决定）
        return NoContent();
    }

    //redis update
    [HttpDelete("trains/deleteTrain")]
    public async Task<IActionResult> DeleteTrain(int trainId)
    {
        var trainEntity = await _ticketRepository.GetTrainDetailAsync(trainId);

        if (trainEntity == null)
        {
            return NotFound();
        }

        await _ticketRepository.GetTrainDetailAsync(trainId);
        _ticketRepository.DeleteTrain(trainEntity);

        await _ticketRepository.SaveAsync();
        var redis = new RedisUtil(_distributedCache);
        redis.RedisRemove("Train_" + trainId);

        return NoContent();
    }
}