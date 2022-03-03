using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Api.DtoParameters;
using TicketSystem.Api.Entities;
using TicketSystem.Api.Models;
using TicketSystem.Api.Services;

namespace TicketSystem.Api.Controllers
{
    [ApiController]
    [Route("api/lines")]
    public class LineController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public LineController(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("getLine", Name = nameof(GetLine))]
        public async Task<ActionResult<LineDto>> GetLine(Guid lineId)
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

            var lineDto = _mapper.Map<LineDto>(line);
            return Ok(lineDto);
        }

        [HttpGet("getAllLines", Name = nameof(GetLines))]
        public async Task<ActionResult<LineDto>>
            GetLines([FromQuery] PageDtoParameters? parameters)
        {
            var lines = await _ticketRepository.GetLinesAsync(parameters);
            if (lines == null)
            {
                return NotFound();
            }

            var lineDto = _mapper.Map<IEnumerable<LineDto>>(lines);

            return Ok(lineDto);
        }

        [HttpGet("searchLines", Name = nameof(SearchLines))]
        public async Task<ActionResult<LineOutputDto>> SearchLines(string firstStation, string lastStation)
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


            var linesDtos = _mapper.Map<IEnumerable<LineOutputDto>>(lines);

            return Ok(linesDtos);
        }

        [HttpPost]
        public async Task<ActionResult<LineDto>> CreateLine(LineAddDto line)
        {
            var entity = _mapper.Map<Line>(line);
            _ticketRepository.AddLine(entity);
            await _ticketRepository.SaveAsync();

            var dtoToReturn = _mapper.Map<LineDto>(entity);

            return CreatedAtRoute(nameof(GetLine), dtoToReturn);
        }

        [HttpPut("updateLine")]
        public async Task<ActionResult<LineAddDto>> UpdateLine(Guid lineId, LineAddDto line)
        {
            var lineEntity = await _ticketRepository.GetLineAsync(lineId);

            if (lineEntity == null)
            {
                //没获取就用put创建资源
                var lineToAddEntity = _mapper.Map<Line>(line);

                _ticketRepository.AddLine(lineToAddEntity);

                await _ticketRepository.SaveAsync();
                var dtoToReturn = _mapper.Map<LineOutputDto>(lineToAddEntity);

                return CreatedAtRoute(nameof(GetLine),new
                {
                    lineId
                }, dtoToReturn);
            }

            _mapper.Map(line, lineEntity);
            _ticketRepository.UpdateLine(lineEntity);

            await _ticketRepository.SaveAsync();

            // 204 无需返回资源（根据实际情况决定）
            return NoContent();
        }


        [HttpDelete("deleteLine")]
        public async Task<IActionResult> DeleteLine(Guid lineId)
        {
            var lineEntity = await _ticketRepository.GetLineAsync(lineId);

            if (lineEntity == null)
            {
                return NotFound();
            }

            await _ticketRepository.GetLineAsync(lineId);
            _ticketRepository.DeleteLine(lineEntity);

            await _ticketRepository.SaveAsync();

            return NoContent();
        }
    }
}
