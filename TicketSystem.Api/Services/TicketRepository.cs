using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Api.Data;
using TicketSystem.Api.DtoParameters;
using TicketSystem.Api.Entities;

namespace TicketSystem.Api.Services
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketDbContext _context;

        public TicketRepository(TicketDbContext context)
        {
            _context = context;
        }

        //Booker
        public async Task<Booker> GetBookerAsync(string phoneNum)
        {
            if (phoneNum == null)
            {
                throw new ArgumentNullException(nameof(phoneNum));
            }

            //搜索数据库
            return await _context.Bookers
                .Where(x => x.PhoneNum == phoneNum)
                .FirstOrDefaultAsync();
        }

        public void AddBooker(Booker booker)
        {
            if (booker == null)
            {
                throw new ArgumentNullException(nameof(booker));
            }
            booker.Id = Guid.NewGuid();
            booker.TimeOfRegister = DateTime.Now;

            _context.Bookers.Add(booker);

        }

        public void DeleteBooker(Booker booker)
        {
            //
        }

        public void UpdateBooker(Booker? booker)
        {
            //_context.Entry(booker).State = EntityState.Modified;
            // ef core 会自动填充代码
        }

        public async Task<bool> BookerExistsAsync(string phoneNum)
        {
            if (phoneNum == null)
            {
                throw new ArgumentNullException(nameof(phoneNum));
            }
            return await _context.Bookers.AnyAsync(x => x.PhoneNum == phoneNum);
        }

        public async Task<bool> BookerPwdVerify(Booker booker)
        {
            if (booker == null)
            {
                throw new ArgumentNullException(nameof(booker));
            }

            return await _context.Bookers.AnyAsync(
                x => x.PhoneNum == booker.PhoneNum && x.BookerPwd == booker.BookerPwd);
        }


        //Station
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

        //Train
        public async Task<Train> GetTrainAsync(string trainName)
        {
            if (trainName == null)
            {
                throw new ArgumentNullException(nameof(trainName));
            }

            return await _context.Trains
                .Where(x => x.TrainName == trainName)
                .FirstOrDefaultAsync();
        }

        public void AddTrain(Train train)
        {
            if (train == null)
            {
                throw new ArgumentNullException(nameof(train));
            }
            train.Id = Guid.NewGuid();

            _context.Trains.Add(train);
        }

        public void UpdateTrain(Train train)
        {
            //_context.Entry(booker).State = EntityState.Modified;
            // ef core 会自动填充代码
        }

        public void DeleteTrainAsync(string trainName)
        {
            //
        }

        public async Task<bool> TrainExistsAsync(string trainName)
        {
            if (trainName == null)
            {
                throw new ArgumentNullException(nameof(trainName));
            }
            return await _context.Trains.AnyAsync(x => x.TrainName == trainName);
        }

        //Line
        public async Task<Line> GetLineAsync(Guid lineId)
        {
            if (lineId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(lineId));
            }

            return await _context.Lines
                .Where(x => x.Id == lineId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Line>> GetLinesAsync(string? firstStation, string? lastStation)
        {
            if (string.IsNullOrWhiteSpace(firstStation) || string.IsNullOrWhiteSpace(lastStation))
            {
                throw new ArgumentNullException(nameof(String));
            }

            var items = _context.Lines as IQueryable<Line>;

            if (!string.IsNullOrWhiteSpace(firstStation) && !string.IsNullOrWhiteSpace(lastStation))
            {
                firstStation = firstStation.Trim();
                lastStation = lastStation.Trim();

                items = items.Where(x =>
                    x.StopStation.Contains(firstStation)
                    && x.StopStation.Contains(lastStation));
            }

            return await items.OrderBy(x => x.Id).ToListAsync();
        }

        public void AddLine(Line line)
        {
            if (line == null)
            {
                throw new ArgumentException(nameof(line));
            }

            line.Id = Guid.NewGuid();
            _context.Lines.Add(line);
        }

        public void UpdateLine(Line line)
        {
            throw new NotImplementedException();
        }

        public void DeleteLineAsync(Guid guid)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> LineExistsAsync(Guid lineId)
        {
            if (lineId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(lineId));
            }
            return await _context.Lines.AnyAsync(x => x.Id == lineId);
        }


        //存储
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
