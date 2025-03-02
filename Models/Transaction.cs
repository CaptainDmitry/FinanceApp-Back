using Microsoft.OpenApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestApi.Enums;

namespace TestApi.Models
{
    public class Transaction
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("date_transaction")]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        [EnumDataType(typeof(TransactionType))]
        [Column("transaction_type")]
        public TransactionType TransactionType { get; set; }

        [Required]
        [Column("category_id")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required]
        [Column("account_id")]
        public int AccountId { get; set; }
        public Account? Account {  get; set; }

        [Required]
        [Column("amount")]
        public decimal Amount { get; set; }

    }
}
