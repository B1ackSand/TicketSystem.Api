using TicketSystem.Api.Entities;

namespace TicketSystem.Api.Services
{
    public interface IBookerRepository
    {
        Task<Booker> GetBookerAsync(string bookerWx);
        void AddBooker(Booker booker);
        void DeleteBooker(Booker booker);
        void UpdateBooker(Booker booker);
        Task<bool> BookerExistsAsync(string bookerWx);
        Task<bool> SaveAsync();
    }
}
