using TicketSystem.Api.Entities;

namespace TicketSystem.Api.Services
{
    public interface IBookerRepository
    {
        Task<Booker> GetBookerAsync(string phoneNum);
        void AddBooker(Booker booker);
        void DeleteBooker(Booker booker);
        void UpdateBooker(Booker booker);
        Task<bool> BookerExistsAsync(string phoneNum);
        Task<bool> BookerPwdVerify(Booker booker);
        Task<bool> SaveAsync();
    }
}
