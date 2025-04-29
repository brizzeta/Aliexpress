using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public UserRole Role { get; set; }
        public string Address { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        public string? ProfileImageUrl { get; set; }
        public double Rating { get; set; }
        public bool IsActive { get; set; }
        public string? Token { get; set; }

    }
}
