using TicketSystem.Api.DtoParameters;
using TicketSystem.Api.Entities;

namespace TicketSystem.Api.Services;

public interface IStationRepository
{
    
    Task<IEnumerable<Station>> GetStationsAsync(StationDtoParameters? parameters);
    Task<Station> GetStationAsync(string stationName);
    void AddStation(Station station);
    void DeleteStation(Station station);
    void UpdateStation(Station station);
    Task<bool> StationExistsAsync(string stationName);
    Task<bool> SaveAsync();
}