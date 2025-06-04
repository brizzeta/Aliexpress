using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password_hash { get; set; } = null!;
        public string? Phone { get; set; }
        public UserRole Role { get; set; } // Enum
        public string Address { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        public string? ProfileImageUrl { get; set; }
        public double Rating { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Chat> Chats { get; set; } = new List<Chat>();
        public ICollection<Notifications> Notifications { get; set; } = new List<Notifications>();
        public ICollection<Messages> Messages { get; set; } = new List<Messages>();

    }
}
