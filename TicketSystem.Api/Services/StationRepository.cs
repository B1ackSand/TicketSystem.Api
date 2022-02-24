using Microsoft.EntityFrameworkCore;
using TicketSystem.Api.Data;
using TicketSystem.Api.DtoParameters;
using TicketSystem.Api.Entities;

namespace TicketSystem.Api.Services
{
    public class StationRepository: IStationRepository
    {
        private readonly TicketDbContext _context;

        public StationRepository(TicketDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Station>> GetStationsAsync(StationDtoParameters? parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            //查询表达式,延迟执行，组件了一个命令，根据条件进行过滤和搜索
            var queryExpression = _context.Stations as IQueryable<Station>;

            if (!string.IsNullOrWhiteSpace(parameters.StationName))
            {
                parameters.StationName = parameters.StationName.Trim();
                queryExpression = queryExpression.Where(x => x.StationName == parameters.StationName);
            }

            //翻页
            queryExpression = queryExpression.Skip(parameters.PageSize * (parameters.PageNumber - 1))
                .Take(parameters.PageSize);

            return await queryExpression.ToListAsync();
        }

        public async Task<Station> GetStationAsync(string stationName)
        {
            if (stationName == null)
            {
                throw new ArgumentNullException(nameof(stationName));
            }

            //搜索数据库
            return await _context.Stations
                .Where(x => x.StationName == stationName)
                .FirstOrDefaultAsync();
        }

        public void AddStation(Station station)
        {
            if (station == null)
            {
                throw new ArgumentNullException(nameof(station));
            }
            station.Id = Guid.NewGuid();

            _context.Stations.Add(station);
        }

        public void DeleteStation(Station station)
        {
            //
        }

        public void UpdateStation(Station station)
        {
            //_context.Entry(booker).State = EntityState.Modified;
            // ef core 会自动填充代码
        }

        public async Task<bool> StationExistsAsync(string stationName)
        {
            if (stationName == null)
            {
                throw new ArgumentNullException(nameof(stationName));
            }
            return await _context.Stations.AnyAsync(x => x.StationName == stationName);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
