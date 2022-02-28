using TicketSystem.Api.DtoParameters;
using TicketSystem.Api.Entities;

namespace TicketSystem.Api.Services
{
    public interface ITicketRepository
    {
        //Booker
        Task<Booker> GetBookerAsync(string phoneNum);
        void AddBooker(Booker booker);
        void DeleteBooker(Booker booker);
        void UpdateBooker(Booker booker);
        Task<bool> BookerExistsAsync(string phoneNum);
        Task<bool> BookerPwdVerify(Booker booker);


        //Station
        Task<IEnumerable<Station>> GetStationsAsync(StationDtoParameters? parameters);
        Task<Station> GetStationAsync(string stationName);
        void AddStation(Station station);
        void DeleteStation(Station station);
        void UpdateStation(Station station);
        Task<bool> StationExistsAsync(string stationName);


        //Train
        Task<Train> GetTrainAsync(string trainName);
        void AddTrain(Train train);
        void UpdateTrain(Train train);
        void DeleteTrainAsync(string trainName);
        Task<bool> TrainExistsAsync(string trainName);


        //Line
        Task<Line> GetLineAsync(Guid lineId);
        Task<IEnumerable<Line>> GetLinesAsync(string firstStation,string lastStation);
        void AddLine(Line line);
        void UpdateLine(Line line);
        void DeleteLineAsync(Guid guid);
        Task<bool> LineExistsAsync(Guid lineId);

        //存储
        Task<bool> SaveAsync();
    }
}
