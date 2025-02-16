﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        private string passwordHash;

        [Column("password")]
        [Required]
        public string PasswordHash {
            get
            {
                return passwordHash;
            }
            set
            {
                passwordHash = BCrypt.Net.BCrypt.HashPassword(value);
            }
        }

        [Column("registration_date")]
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        public bool VerifHashPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
