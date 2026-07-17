using System;
using System.Collections.Generic;
using System.Text;

namespace CarePride.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty; // Stores the encrypted password
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
        public int RoleId { get; set; } // Foreign key to Role
    }
}
