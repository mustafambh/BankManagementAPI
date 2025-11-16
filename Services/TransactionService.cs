using Bank.Data;
using Bank.Dtos;
using Bank.Models;
using Bank.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bank.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _db;

        public TransactionService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<TransactionDto>> GetAllAsync()
        {
            return await _db.Transactions.Select(t => new TransactionDto
            {
                Id = t.Id,
                AccountId = t.AccountId,
                RelatedAccountId = t.RelatedAccountId,
                Type = t.Type,
                Amount = t.Amount,
                DateTime = t.DateTime,
                PerformedByUserId = t.PerformedByUserId,
                Description = t.Description
            }).ToListAsync();
        }

        public async Task<TransactionDto?> GetByIdAsync(int id)
        {
            var t = await _db.Transactions.FindAsync(id);
            if (t == null) return null;

            return new TransactionDto
            {
                Id = t.Id,
                AccountId = t.AccountId,
                RelatedAccountId = t.RelatedAccountId,
                Type = t.Type,
                Amount = t.Amount,
                DateTime = t.DateTime,
                PerformedByUserId = t.PerformedByUserId,
                Description = t.Description
            };
        }

        public async Task<TransactionDto> CreateAsync(CreateTransactionDto dto)
        {
            var transaction = new Transaction
            {
                AccountId = dto.AccountId,
                RelatedAccountId = dto.RelatedAccountId,
                Type = dto.Type,
                Amount = dto.Amount,
                PerformedByUserId = dto.PerformedByUserId,
                Description = dto.Description,
                DateTime = DateTime.Now
            };

            // Update account balance
            var account = await _db.Accounts.FindAsync(dto.AccountId);
            if (account == null) throw new Exception("Account not found");

            if (dto.Type == "Deposit")
                account.Balance += dto.Amount;
            else if (dto.Type == "Withdraw")
            {
                if (account.Balance < dto.Amount) throw new Exception("Insufficient balance");
                account.Balance -= dto.Amount;
            }
            else if (dto.Type == "Transfer")
            {
                if (dto.RelatedAccountId == null) throw new Exception("Related account required for transfer");
                var relatedAcc = await _db.Accounts.FindAsync(dto.RelatedAccountId);
                if (relatedAcc == null) throw new Exception("Related account not found");
                if (account.Balance < dto.Amount) throw new Exception("Insufficient balance");
                account.Balance -= dto.Amount;
                relatedAcc.Balance += dto.Amount;
            }
            else
            {
                throw new Exception("Invalid transaction type");
            }

            _db.Transactions.Add(transaction);
            await _db.SaveChangesAsync();

            return new TransactionDto
            {
                Id = transaction.Id,
                AccountId = transaction.AccountId,
                RelatedAccountId = transaction.RelatedAccountId,
                Type = transaction.Type,
                Amount = transaction.Amount,
                DateTime = transaction.DateTime,
                PerformedByUserId = transaction.PerformedByUserId,
                Description = transaction.Description
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var transaction = await _db.Transactions.FindAsync(id);
            if (transaction == null) return false;

            _db.Transactions.Remove(transaction);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
