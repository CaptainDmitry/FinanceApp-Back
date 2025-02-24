using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Models
{
    public class AuditableEntity
    {
        [Column("created_by")]
        public int UserId { get; set; }
        public User? CreatedBy { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
