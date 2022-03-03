using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Api.Data;
using TicketSystem.Api.DtoParameters;
using TicketSystem.Api.Entities;
using TicketSystem.Api.Models;

namespace TicketSystem.Api.Services
{
    public class TicketRepository : ITicketRepository
    {
        private readonly TicketDbContext _context;
        private ITicketRepository _ticketRepositoryImplementation;

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

        public async Task<Booker> GetBookerAsync(Guid bookerId)
        {
            if (bookerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bookerId));
            }

            //搜索数据库
            return await _context.Bookers
                .Where(x => x.BookerId == bookerId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Booker>> GetBookersAsync(PageDtoParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var queryExpression = _context.Bookers
                .Skip(parameters.PageSize * (parameters.PageNumber - 1)).Take(parameters.PageSize);

            return await queryExpression.ToListAsync();
        }

        public void AddBooker(Booker booker)
        {
            if (booker == null)
            {
                throw new ArgumentNullException(nameof(booker));
            }
            booker.BookerId = Guid.NewGuid();
            booker.TimeOfRegister = DateTime.Now;

            _context.Bookers.Add(booker);

        }

        public void DeleteBooker(Booker booker)
        {
            if (booker == null)
            {
                throw new ArgumentNullException(nameof(booker));
            }

            _context.Bookers.Remove(booker);
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

        public async Task<bool> BookerExistsAsync(Guid bookerId)
        {
            if (bookerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bookerId));
            }
            return await _context.Bookers.AnyAsync(x => x.BookerId == bookerId);
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

        public async Task<Station> GetStationAsync(Guid stationId)
        {
            if (stationId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(stationId));
            }

            return await _context.Stations
                .Where(x => x.StationId == stationId)
                .FirstOrDefaultAsync();
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
            station.StationId = Guid.NewGuid();

            _context.Stations.Add(station);
        }

        public void DeleteStation(Station station)
        {
            if (station == null)
            {
                throw new ArgumentNullException(nameof(station));
            }

            _context.Stations.Remove(station);
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
        public async Task<Train> GetTrainDetailAsync(Guid trainId)
        {
            if (trainId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(trainId));
            }

            return await _context.Trains
                .Where(x => x.TrainId == trainId)
                .FirstOrDefaultAsync();
        }

        public async Task<Train> GetTrainAsync(Guid lineId, Guid trainId)
        {
            if (lineId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(lineId));
            }

            if (trainId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(trainId));
            }

            return await _context.Trains
                .Where(x => x.LineId == lineId && x.TrainId == trainId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Train>> GetTrainsAsync(PageDtoParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var queryExpression = _context.Trains
                .Skip(parameters.PageSize * (parameters.PageNumber - 1)).Take(parameters.PageSize);

            return await queryExpression.ToListAsync();
        }

        public void AddTrain(Train train)
        {
            if (train == null)
            {
                throw new ArgumentNullException(nameof(train));
            }
            train.TrainId = Guid.NewGuid();

            _context.Trains.Add(train);
        }

        public void AddTrain(Guid lineId,Train train)
        {
            if (lineId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(lineId));
            }

            if (train == null)
            {
                throw new ArgumentNullException(nameof(train));
            }
            train.TrainId = Guid.NewGuid();
            train.LineId = lineId;

            _context.Trains.Add(train);
        }

        public void UpdateTrain(Train train)
        {
            //_context.Entry(booker).State = EntityState.Modified;
            // ef core 会自动填充代码
        }

        public void DeleteTrain(Train train)
        {
            if (train == null)
            {
                throw new ArgumentNullException(nameof(train));
            }

            _context.Trains.Remove(train);
        }

        public async Task<bool> TrainExistsAsync(Guid trainId)
        {
            if (trainId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(trainId));
            }
            return await _context.Trains.AnyAsync(x => x.TrainId == trainId);
        }

        //Line
        public async Task<Line> GetLineAsync(Guid lineId)
        {
            if (lineId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(lineId));
            }

            return await _context.Lines
                .Where(x => x.LineId == lineId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Line>> GetLinesAsync(string? firstStation, string? lastStation)
        {
            if (string.IsNullOrWhiteSpace(firstStation) || string.IsNullOrWhiteSpace(lastStation))
            {
                throw new ArgumentNullException(nameof(String));
            }

            var lines = _context.Lines as IQueryable<Line>;
            var trains = _context.Trains;

            if (!string.IsNullOrWhiteSpace(firstStation) && !string.IsNullOrWhiteSpace(lastStation))
            {
                firstStation = firstStation.Trim();
                lastStation = lastStation.Trim();

                lines = lines
                    .Join(trains, p => p.LineId, d => d.LineId,
                        (p, d) => new Line
                        {
                            LineId = p.LineId,
                            StartTerminal = p.StartTerminal,
                            EndTerminal = p.EndTerminal,
                            StopStation = p.StopStation,
                            TrainName = d.TrainName
                        }).Where(x => x.StopStation.Contains(firstStation) && x.StopStation.Contains(lastStation));
            }
            return await lines.OrderBy(x => x.StopStation.Length).ToListAsync();
        }

        public async Task<IEnumerable<Line>> GetLinesAsync(PageDtoParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var queryExpression = _context.Lines
                .Skip(parameters.PageSize * (parameters.PageNumber - 1)).Take(parameters.PageSize);

            return await queryExpression.ToListAsync();
        }

        public void AddLine(Line line)
        {
            if (line == null)
            {
                throw new ArgumentException(nameof(line));
            }

            line.LineId = Guid.NewGuid();
            _context.Lines.Add(line);
        }

        public void UpdateLine(Line? line)
        {
            //_context.Entry(booker).State = EntityState.Modified;
            // ef core 会自动填充代码
        }

        public void DeleteLine(Line line)
        {
            if (line == null)
            {
                throw new ArgumentNullException(nameof(line));
            }

            _context.Lines.Remove(line);
        }

        public async Task<bool> LineExistsAsync(Guid lineId)
        {
            if (lineId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(lineId));
            }
            return await _context.Lines.AnyAsync(x => x.LineId == lineId);
        }


        //Order
        public async Task<Order> GetOrderAsync(Guid bookerId, Guid orderId)
        {
            if (bookerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bookerId));
            }

            if (orderId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(orderId));
            }

            //搜索数据库
            return await _context.Orders
                .Where(x => x.OrderId == orderId && x.BookerId == bookerId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(PageDtoParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var queryExpression = _context.Orders
                .Skip(parameters.PageSize * (parameters.PageNumber - 1)).Take(parameters.PageSize);

            return await queryExpression.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(Guid bookerId)
        {
            if (bookerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bookerId));
            }

            var items = _context.Orders as IQueryable<Order>;

            items = items.Where(x => x.BookerId == bookerId);

            return await items.OrderBy(x => x.CreatedDate).ToListAsync();
        }

        public void AddOrder(Guid bookerId, Order order)
        {
            if (bookerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bookerId));
            }

            if (order == null)
            {
                throw new ArgumentException(nameof(order));
            }
            order.OrderId = Guid.NewGuid();
            order.BookerId = bookerId;
            order.CreatedDate = DateTime.Now;
            _context.Add(order);
        }

        public async Task<bool> OrderExistsAsync(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(orderId));
            }
            return await _context.Orders.AnyAsync(x => x.OrderId == orderId);
        }

        public void UpdateOrder(Order order)
        {
           
        }

        public void DeleteOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            _context.Orders.Remove(order);
        }


        //存储
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
