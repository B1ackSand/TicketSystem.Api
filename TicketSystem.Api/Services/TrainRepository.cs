using Microsoft.EntityFrameworkCore;
using TicketSystem.Api.Data;
using TicketSystem.Api.Entities;

namespace TicketSystem.Api.Services;

public class TrainRepository: ITrainRepository
{
    private readonly TicketDbContext _context;

    public TrainRepository(TicketDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
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

    public async Task<bool> SaveAsync()
    {
        return await _context.SaveChangesAsync() >= 0;
    }
}