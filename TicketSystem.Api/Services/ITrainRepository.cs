using TicketSystem.Api.Entities;

namespace TicketSystem.Api.Services
{
    public interface ITrainRepository
    {
        Task<Train> GetTrainAsync(string trainName);
        void AddTrain(Train train);
        void UpdateTrain(Train train);
        void DeleteTrainAsync(string trainName);
        Task<bool> TrainExistsAsync(string trainName);
        Task<bool> SaveAsync();
    }
}
