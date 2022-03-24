using Geolocation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using TicketSystem.Api.Data;
using TicketSystem.Api.DtoParameters;
using TicketSystem.Api.Entities;


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

        public async Task<Booker> GetBookerAsync(int bookerId)
        {
            if (bookerId == null)
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

        public async Task<bool> BookerExistsAsync(int bookerId)
        {
            if (bookerId == null)
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
                x => x.PhoneNum == booker.PhoneNum
                     && x.BookerPwd == booker.BookerPwd
                     && x.CardId.Contains(booker.CardId));
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

        public async Task<Station> GetStationAsync(int stationId)
        {
            if (stationId == null)
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
            // station.StationId = Guid.NewGuid();

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

        public async Task<bool> StationExistsAsync(int stationId)
        {
            if (stationId == null)
            {
                throw new ArgumentNullException(nameof(stationId));
            }
            return await _context.Stations.AnyAsync(x => x.StationId == stationId);
        }







        //Train
        public async Task<Train> GetTrainDetailAsync(int trainId)
        {
            if (trainId == null)
            {
                throw new ArgumentNullException(nameof(trainId));
            }

            return await _context.Trains
                .Where(x => x.TrainId == trainId)
                .FirstOrDefaultAsync();
        }

        public async Task<Train> GetTrainAsync(int lineId, int trainId)
        {
            if (lineId == null)
            {
                throw new ArgumentNullException(nameof(lineId));
            }

            if (trainId == null)
            {
                throw new ArgumentNullException(nameof(trainId));
            }

            return await _context.Trains
                .Where(x => x.LineId == lineId && x.TrainId == trainId)
                .FirstOrDefaultAsync();
        }

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
            // train.TrainId = Guid.NewGuid();

            _context.Trains.Add(train);
        }

        public void AddTrain(int lineId, Train train)
        {
            if (lineId == null)
            {
                throw new ArgumentNullException(nameof(lineId));
            }

            if (train == null)
            {
                throw new ArgumentNullException(nameof(train));
            }
            // train.TrainId = Guid.NewGuid();
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

        public async Task<bool> TrainExistsAsync(int trainId)
        {
            if (trainId == null)
            {
                throw new ArgumentNullException(nameof(trainId));
            }
            return await _context.Trains.AnyAsync(x => x.TrainId == trainId);
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
        public async Task<Line> GetLineAsync(int lineId)
        {
            if (lineId == null)
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

            // line.LineId = Guid.NewGuid();
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

        public async Task<bool> LineExistsAsync(int lineId)
        {
            if (lineId == null)
            {
                throw new ArgumentNullException(nameof(lineId));
            }
            return await _context.Lines.AnyAsync(x => x.LineId == lineId);
        }








        //Order
        public async Task<Order> GetOrderAsync(int bookerId, Guid orderId)
        {
            if (bookerId == null)
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

        public async Task<IEnumerable<Order>> GetOrdersAsync(int bookerId)
        {
            if (bookerId == null)
            {
                throw new ArgumentNullException(nameof(bookerId));
            }

            var items = _context.Orders as IQueryable<Order>;

            items = items.Where(x => x.BookerId == bookerId);

            return await items.OrderBy(x => x.CreatedDate).ToListAsync();
        }

        public void AddOrder(int bookerId, Order order)
        {
            if (bookerId == null)
            {
                throw new ArgumentNullException(nameof(bookerId));
            }

            if (order == null)
            {
                throw new ArgumentException(nameof(order));
            }
            order.OrderId = Guid.NewGuid();
            order.BookerId = bookerId;
            var tmp = GetStationAsync(order.StartTerminalId);
            order.StartTerminal = tmp.Result.StationName;
            tmp = GetStationAsync(order.EndTerminalId);
            order.EndTerminal = tmp.Result.StationName;
            var tmp2 = GetTrainDetailAsync(order.TrainId);
            order.TrainName = tmp2.Result.TrainName;
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


        //Distance and price
        public double GetDistance(string stopStation, string startTerminal, string endTerminal)
        {
            if (string.IsNullOrWhiteSpace(stopStation) || string.IsNullOrWhiteSpace(startTerminal) || string.IsNullOrWhiteSpace(endTerminal))
            {
                throw new ArgumentNullException(nameof(stopStation));
            }

            //获取起始和终点站区间路段
            var strArray = stopStation.Split(',');
            var i1 = strArray.FindIndex(startTerminal);
            var i2 = strArray.FindIndex(endTerminal);
            var rangeStr = strArray[i1..(i2 + 1)].Clone() as string[];

            //从数据库中获取经纬度 lon/lat
            double distance = 0;
            var originItem = _context.Stations
                .Where(x => x.StationName == rangeStr[0])
                .Select(n => new
                {
                    n.Latitude,
                    n.Longitude
                }).First();
            Coordinate origin = new Coordinate(originItem.Latitude, originItem.Longitude);

            //各站点之间距离求和
            for (int i = 0; i < rangeStr.Length - 1; i++)
            {
                var destinationItem = _context.Stations
                    .Where(x => x.StationName == rangeStr[i + 1])
                    .Select(n => new
                    {
                        n.Latitude,
                        n.Longitude
                    }).First();
                var destination = new Coordinate(destinationItem.Latitude, destinationItem.Longitude);
                double distanceTemp = GeoCalculator.GetDistance(origin, destination, 2, distanceUnit: DistanceUnit.Kilometers);
                distance += distanceTemp;
                origin = destination;
            }

            return distance;
        }

        public double GetPrice(double distance, string typeOfTrain)
        {
            double unitPrice = 0;
            switch (typeOfTrain)
            {
                case "D":
                    unitPrice = 0.4;
                    break;
                case "K":
                    unitPrice = 0.3;
                    break;
                case "G":
                    unitPrice = 0.5;
                    break;
                default:
                    unitPrice = 0.4;
                    break;
            }
            return unitPrice * distance;
        }


        //存储至数据库
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }


    }

    //拓展(Extension)
    public static class Extensions
    {
        public static int FindIndex<T>(this T[] array, T item)
        {
            return Array.IndexOf(array, item);
        }
    }
}
