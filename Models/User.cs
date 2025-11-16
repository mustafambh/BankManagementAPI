using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Bank.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PassWordHash { get; set; }
        [Required]
        public string Role { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Account> Accounts { get; set; }
    }
}
