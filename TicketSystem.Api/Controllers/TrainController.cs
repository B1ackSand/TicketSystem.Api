using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Api.DtoParameters;
using TicketSystem.Api.Entities;
using TicketSystem.Api.Models;
using TicketSystem.Api.Services;

namespace TicketSystem.Api.Controllers;

[ApiController]
[Route("api")]
public class TrainController : ControllerBase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;

    public TrainController(ITicketRepository trainRepository, IMapper mapper)
    {
        _ticketRepository = trainRepository ?? throw new ArgumentNullException(nameof(trainRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet("train")]
    public async Task<ActionResult<TrainOutputDto>> GetTrain(string trainName)
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

        var trainDto = _mapper.Map<TrainOutputDto>(train);

        return Ok(trainDto);
    }

    [HttpGet("trains/{trainId}", Name = nameof(GetTrainDetail))]
    public async Task<ActionResult<TrainOutputDto>> GetTrainDetail(int trainId)
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

        var trainDto = _mapper.Map<TrainOutputDto>(train);

        return Ok(trainDto);
    }


    [HttpGet("lines/{lineId}/trains/{trainId}", Name = nameof(GetTrainForLine))]
    public async Task<ActionResult<TrainOutputDto>> GetTrainForLine(int lineId, int trainId)
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
        var trainDto = _mapper.Map<TrainOutputDto>(train);
        return Ok(trainDto);
    }

    [HttpGet("getAllTrains", Name = nameof(GetTrains))]
    public async Task<ActionResult<BookerOutputDto>>
        GetTrains([FromQuery] PageDtoParameters? parameters)
    {
        var trains = await _ticketRepository.GetTrainsAsync(parameters);
        if (trains == null)
        {
            return NotFound();
        }

        var trainsDto = _mapper.Map<IEnumerable<TrainOutputDto>>(trains);

        return Ok(trainsDto);
    }

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


    [HttpPut("trains/updateTrain")]
    public async Task<ActionResult<TrainAddDto>> UpdateTrain(int trainId, TrainUpdateDto train)
    {
        var trainEntity = await _ticketRepository.GetTrainDetailAsync(trainId);

        if (trainEntity == null)
        {
            //没获取就用put创建资源
            var trainToAddEntity = _mapper.Map<Train>(train);

            _ticketRepository.AddTrain(trainToAddEntity);

            await _ticketRepository.SaveAsync();
            var dtoToReturn = _mapper.Map<TrainOutputDto>(trainToAddEntity);

            return CreatedAtRoute(nameof(GetTrainDetail), new
            {
                trainId = dtoToReturn.TrainId
            }, dtoToReturn);
        }

        _mapper.Map(train, trainEntity);
        _ticketRepository.UpdateTrain(trainEntity);

        await _ticketRepository.SaveAsync();

        // 204 无需返回资源（根据实际情况决定）
        return NoContent();
    }


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

        return NoContent();
    }
}