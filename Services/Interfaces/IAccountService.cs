using Bank.Dtos;

namespace Bank.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDto>> GetAllAsync();
        Task<AccountDto?> GetByIdAsync(int id);
        Task<AccountDto> CreateAsync(CreateAccountDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
