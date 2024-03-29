﻿using TicketSystem.Api.DtoParameters;
using TicketSystem.Api.Entities;

namespace TicketSystem.Api.Services
{
    public interface ITicketRepository
    {
        //Booker
        Task<Booker> GetBookerAsync(string phoneNum);
        Task<Booker> GetBookerAsync(int bookerId);
        Task<IEnumerable<Booker>> GetBookersAsync(PageDtoParameters parameters);
        void AddBooker(Booker booker);
        void DeleteBooker(Booker booker);
        void UpdateBooker(Booker booker);
        Task<bool> BookerExistsAsync(string phoneNum);
        Task<bool> BookerExistsAsync(int bookerId);
        Task<bool> BookerPwdVerify(Booker booker);


        //Station
        Task<IEnumerable<Station>> GetStationsAsync(StationDtoParameters? parameters);
        Task<Station> GetStationAsync(int stationId);
        Task<Station> GetStationAsync(string stationName);
        void AddStation(Station station);
        void DeleteStation(Station station);
        void UpdateStation(Station station);
        Task<bool> StationExistsAsync(string stationName);
        Task<bool> StationExistsAsync(int stationId);


        //Train
        Task<Train> GetTrainDetailAsync(int trainId);
        Task<Train> GetTrainAsync(int lineId,int trainId);
        Task<Train> GetTrainAsync(string trainName);
        Task<IEnumerable<Train>> GetTrainsAsync(PageDtoParameters parameters);
        void AddTrain(int lineId,Train train);
        void AddTrain(Train train);
        void UpdateTrain(Train train);
        void DeleteTrain(Train train);
        Task<bool> TrainExistsAsync(int trainId);
        Task<bool> TrainExistsAsync(string trainName);


        //Line
        Task<Line> GetLineAsync(int lineId);
        Task<IEnumerable<Line>> GetLinesAsync(string firstStation,string lastStation);
        Task<IEnumerable<Line>> GetLinesAsync(PageDtoParameters parameters);
        void AddLine(Line line);
        void UpdateLine(Line line);
        void DeleteLine(Line line);
        Task<bool> LineExistsAsync(int lineId);

        //Order
        Task<IEnumerable<Order>> GetOrdersAsync(int bookerId);
        Task<Order> GetOrderAsync(int bookerId, Guid orderId);
        Task<IEnumerable<Order>> GetOrdersAsync(PageDtoParameters parameters);
        void AddOrder(int bookerId,Order order);
        Task<bool> OrderExistsAsync(Guid orderId);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);

        //distance
        public double GetDistance(string stopStation, string startTerminal, string endTerminal);
        public double GetPrice(double distance, string typeOfTrain);
        public string GetDeparture(string stopStation, string startTerminal, string time);

        //存储
        Task<bool> SaveAsync();
    }
}
