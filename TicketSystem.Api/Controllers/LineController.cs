using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Api.Entities;
using TicketSystem.Api.Models;
using TicketSystem.Api.Services;

namespace TicketSystem.Api.Controllers
{
    [ApiController]
    [Route("api/Lines")]
    public class LineController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public LineController(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("getLine",Name = nameof(GetLine))]
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

        [HttpGet("searchLines",Name = nameof(SearchLines))]
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
    }
}
