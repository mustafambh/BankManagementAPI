using System.ComponentModel.DataAnnotations;

namespace Bank.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public bool IsClosed { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
