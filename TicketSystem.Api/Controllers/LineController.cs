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

namespace TicketSystem.Api.Controllers
{
    [ApiController]
    [Route("api/lines")]
    public class LineController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;

        public LineController(ITicketRepository ticketRepository, IMapper mapper, IDistributedCache distributedCache)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        }


        //update redis
        [HttpGet("getLine", Name = nameof(GetLine))]
        public async Task<ActionResult<LineDto>> GetLine(int lineId)
        {
            var cacheKey = "Line_" + lineId;
            var redis = new RedisUtil(_distributedCache);
            LineDto lineDto;
            var redisByte = await _distributedCache.GetAsync(cacheKey);
            if (redisByte != null)
            {

                var lineList = JsonConvert.DeserializeObject<Line>(redis.RedisRead(redisByte));
                lineDto = _mapper.Map<LineDto>(lineList);
            }
            else
            {
                if (!await _ticketRepository.LineExistsAsync(lineId))
                {
                    return NotFound();
                }

                var line = await _ticketRepository.GetLineAsync(lineId);
                if (line == null)
                {
                    return NotFound();
                }

                lineDto = _mapper.Map<LineDto>(line);
                redis.RedisSave("Line_" + lineId, lineDto);
            }
            return Ok(lineDto);
        }



        //redis update
        [HttpGet("getAllLines", Name = nameof(GetLines))]
        public async Task<ActionResult<LineDto>>
            GetLines([FromQuery] PageDtoParameters? parameters)
        {
            var cacheKey = "LineList";
            IEnumerable<LineDto> lineDto;
            var redis = new RedisUtil(_distributedCache);
            var redisLineByte = await _distributedCache.GetAsync(cacheKey);
            if (redisLineByte != null)
            {
                var lineList = JsonConvert.DeserializeObject<List<Line>>(redis.RedisRead(redisLineByte));
                lineDto = _mapper.Map<IEnumerable<LineDto>>(lineList);
            }
            else
            {
                var lines = await _ticketRepository.GetLinesAsync(parameters);
                if (lines == null)
                {
                    return NotFound();
                }
                lineDto = _mapper.Map<IEnumerable<LineDto>>(lines);
                redis.RedisSave("LineList", lineDto);

            }


            return Ok(lineDto);
        }


        //redis update
        [HttpGet("searchLines", Name = nameof(SearchLines))]
        public async Task<ActionResult<LineOutputDto>> SearchLines(string firstStation, string lastStation)
        {
            var cacheKey = "Line_" + firstStation + "_" + lastStation;
            var redis = new RedisUtil(_distributedCache);
            IEnumerable<LineOutputDto> linesDtos;
            var redisLineByte = await _distributedCache.GetAsync(cacheKey);
            if (redisLineByte != null)
            {
                var lineList = JsonConvert.DeserializeObject<List<Line>>(redis.RedisRead(redisLineByte));
                linesDtos = _mapper.Map<IEnumerable<LineOutputDto>>(lineList);
            }
            else
            {
                if (!await _ticketRepository.StationExistsAsync(firstStation))
                {
                    return NotFound();
                }

                if (!await _ticketRepository.StationExistsAsync(lastStation))
                {
                    return NotFound();
                }

                var lines = await _ticketRepository.GetLinesAsync(firstStation, lastStation);


                linesDtos = _mapper.Map<IEnumerable<LineOutputDto>>(lines);
                redis.RedisSave("Line_" + firstStation + "_" + lastStation, linesDtos);
            }
            return Ok(linesDtos);
        }

        //redis update
        [HttpPost]
        public async Task<ActionResult<LineDto>> CreateLine(LineAddDto line)
        {
            var entity = _mapper.Map<Line>(line);
            _ticketRepository.AddLine(entity);
            await _ticketRepository.SaveAsync();

            var dtoToReturn = _mapper.Map<LineDto>(entity);

            var redis = new RedisUtil(_distributedCache);
            redis.RedisSave("Line_" + dtoToReturn.LineId, dtoToReturn);
            redis.RedisRemove("LineList");

            return CreatedAtRoute(nameof(GetLine), dtoToReturn);
        }


        //redis update
        [HttpPut("updateLine")]
        public async Task<ActionResult<LineAddDto>> UpdateLine(int lineId, LineAddDto line)
        {
            var lineEntity = await _ticketRepository.GetLineAsync(lineId);
            var redis = new RedisUtil(_distributedCache);

            if (lineEntity == null)
            {
                //没获取就用put创建资源
                var lineToAddEntity = _mapper.Map<Line>(line);

                _ticketRepository.AddLine(lineToAddEntity);

                await _ticketRepository.SaveAsync();
                var dtoToReturn = _mapper.Map<LineOutputDto>(lineToAddEntity);

                redis.RedisSave("Line_" + lineId, dtoToReturn);

                return CreatedAtRoute(nameof(GetLine), new
                {
                    lineId
                }, dtoToReturn);
            }

            _mapper.Map(line, lineEntity);
            _ticketRepository.UpdateLine(lineEntity);
            var redisFlesh = _mapper.Map<LineOutputDto>(lineEntity);

            redis.RedisSave("Line_" + lineId, redisFlesh);

            await _ticketRepository.SaveAsync();

            // 204 无需返回资源（根据实际情况决定）
            return NoContent();
        }

        //redis update
        [HttpDelete("deleteLine")]
        public async Task<IActionResult> DeleteLine(int lineId)
        {
            var lineEntity = await _ticketRepository.GetLineAsync(lineId);

            if (lineEntity == null)
            {
                return NotFound();
            }

            await _ticketRepository.GetLineAsync(lineId);
            _ticketRepository.DeleteLine(lineEntity);

            await _ticketRepository.SaveAsync();
            var redis = new RedisUtil(_distributedCache);
            redis.RedisRemove("LineList");
            redis.RedisRemove("Line_" + lineId);

            return NoContent();
        }
    }
}
