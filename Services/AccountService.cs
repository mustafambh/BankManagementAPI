using Bank.Data;
using Bank.Dtos;
using Bank.Models;
using Bank.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bank.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _db;

        public AccountService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<AccountDto>> GetAllAsync()
        {
            return await _db.Accounts.Select(a => new AccountDto
            {
                Id = a.Id,
                AccountNumber = a.AccountNumber,
                Balance = a.Balance,
                Currency = a.Currency,
                IsClosed = a.IsClosed,
                UserId = a.UserId
            }).ToListAsync();
        }

        public async Task<AccountDto?> GetByIdAsync(int id)
        {
            var acc = await _db.Accounts.FindAsync(id);
            if (acc == null) return null;

            return new AccountDto
            {
                Id = acc.Id,
                AccountNumber = acc.AccountNumber,
                Balance = acc.Balance,
                Currency = acc.Currency,
                IsClosed = acc.IsClosed,
                UserId = acc.UserId
            };
        }

        public async Task<AccountDto> CreateAsync(CreateAccountDto dto)
        {
            var acc = new Account
            {
                AccountNumber = dto.AccountNumber,
                Currency = dto.Currency,
                UserId = dto.UserId,
                Balance = 0,
                IsClosed = false
            };

            _db.Accounts.Add(acc);
            await _db.SaveChangesAsync();

            return new AccountDto
            {
                Id = acc.Id,
                AccountNumber = acc.AccountNumber,
                Balance = acc.Balance,
                Currency = acc.Currency,
                IsClosed = acc.IsClosed,
                UserId = acc.UserId
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var acc = await _db.Accounts.FindAsync(id);
            if (acc == null) return false;

            _db.Accounts.Remove(acc);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
