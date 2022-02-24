using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Api.Entities;
using TicketSystem.Api.Models;
using TicketSystem.Api.Services;

namespace TicketSystem.Api.Controllers;

[ApiController]
[Route("api/trains")]
public class TrainController : ControllerBase
{
    private readonly ITrainRepository _trainRepository;
    private readonly IMapper _mapper;

    public TrainController(ITrainRepository trainRepository, IMapper mapper)
    {
        _trainRepository = trainRepository ?? throw new ArgumentNullException(nameof(trainRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet(Name = nameof(GetTrainDetail))]
    public async Task<ActionResult<TrainOutputDto>> GetTrainDetail(string trainName)
    {
        if (!await _trainRepository.TrainExistsAsync(trainName))
        {
            return NotFound();
        }

        var train = await _trainRepository.GetTrainAsync(trainName);
        if (train == null)
        {
            return NotFound();
        }

        var trainDto = _mapper.Map<TrainOutputDto>(train);

        return Ok(trainDto);
    }

    //需要做长度验证错误处理
    [HttpPost]
    public async Task<ActionResult<BookerOutputDto>>
        CreateTrain(TrainAddDto train)
    {
        var entity = _mapper.Map<Train>(train);
        _trainRepository.AddTrain(entity);
        await _trainRepository.SaveAsync();

        var dtoToReturn = _mapper.Map<TrainOutputDto>(entity);

        return CreatedAtRoute(nameof(GetTrainDetail), dtoToReturn);
    }
}