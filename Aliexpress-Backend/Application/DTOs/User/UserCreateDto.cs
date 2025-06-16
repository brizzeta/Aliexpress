using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs.User
{
    public class UserCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = null!;

        [Phone]
        public string? Phone { get; set; }

        public UserRole Role { get; set; } = UserRole.Buyer;

        [Required]
        public string Address { get; set; } = null!;

        public string? ProfileImageUrl { get; set; }
    }
}
