using Microsoft.EntityFrameworkCore;
using TicketSystem.Api.Data;
using TicketSystem.Api.Entities;

namespace TicketSystem.Api.Services
{
    //形成合约，全部拿出来，减少重复代码，方便单元测试，更关注业务逻辑
    public class BookerRepository : IBookerRepository
    {
        private readonly TicketDbContext _context;

        public BookerRepository(TicketDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Booker> GetBookerAsync(string bookerWx)
        {
            if (bookerWx == null)
            {
                throw new ArgumentNullException(nameof(bookerWx));
            }

            //搜索数据库
            return await _context.Bookers
                .Where(x => x.BookerWx == bookerWx)
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

        public async Task<bool> BookerExistsAsync(string bookerWx)
        {
            if (bookerWx == null)
            {
                throw new ArgumentNullException(nameof(bookerWx));
            }
            return await _context.Bookers.AnyAsync(x => x.BookerWx == bookerWx);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
