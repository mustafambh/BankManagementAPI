using System.ComponentModel.DataAnnotations;

namespace Bank.Dtos
{
    public class AccountDto
    {
        public int Id { get; set; }
        public int AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public bool IsClosed { get; set; }
        public int UserId { get; set; }
    }

    public class CreateAccountDto
    {
        [Required]
        public int AccountNumber { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
