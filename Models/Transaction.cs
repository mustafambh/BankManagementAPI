namespace Bank.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public int? RelatedAccountId { get; set; } // For transfer
        public string Type { get; set; } // Deposit / Withdraw / Transfer
        public decimal Amount { get; set; } = 0;
        public DateTime DateTime { get; set; }
        public int PerformedByUserId { get; set; }
        public User User { get; set; }
        public string Description { get; set; }
    }
}
