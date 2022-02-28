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

        public async Task<bool> BookerExistsAsync(string phoneNum)
        {
            if (phoneNum == null)
            {
                throw new ArgumentNullException(nameof(phoneNum));
            }
            return await _context.Bookers.AnyAsync(x => x.PhoneNum == phoneNum);
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

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
