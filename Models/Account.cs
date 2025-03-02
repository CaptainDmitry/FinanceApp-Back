using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Models
{
    public class Account : AuditableEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        private decimal _balance = 0;
        [Column("balance")]
        public decimal Balance
        {
            get => Math.Round(_balance, 2);
            set => _balance = Math.Round(value, 2);
        }

    }
}
