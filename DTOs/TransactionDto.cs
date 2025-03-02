using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TestApi.Enums;
using TestApi.Models;

namespace TestApi.DTOs
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [EnumDataType(typeof(TransactionType))]
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
    }
}
