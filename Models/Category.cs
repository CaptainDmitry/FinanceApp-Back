using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using TestApi.Enums;

namespace TestApi.Models
{
    public class Category : AuditableEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [EnumDataType(typeof(TransactionType))]
        [Column("operation_type")]
        public TransactionType TransactionType { get; set; }

        [JsonIgnore]
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
