using System.ComponentModel.DataAnnotations;

namespace Bank.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int? RelatedAccountId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        public int PerformedByUserId { get; set; }
        public string Description { get; set; }
    }

    public class CreateTransactionDto
    {
        [Required]
        public int AccountId { get; set; }
        public int? RelatedAccountId { get; set; } // for transfers
        [Required]
        public string Type { get; set; } // Deposit / Withdraw / Transfer
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public int PerformedByUserId { get; set; }
        public string Description { get; set; }
    }
}
