using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TestApi.Models
{
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("email")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }
        private string _passwordHash;

        [Column("password")]
        [Required]
        public string PasswordHash {
            get
            {
                return _passwordHash;
            }
            set
            {
                _passwordHash = BCrypt.Net.BCrypt.HashPassword(value);
            }
        }

        [Column("registration_date")]
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        public bool VerifHashPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, _passwordHash);
        }

        [JsonIgnore]
        public List<Account> Accounts { get; set; } = new List<Account>();
        [JsonIgnore]
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
