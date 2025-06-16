using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class UserUpdateDto
    {
        [StringLength(100)]
        public string? Name { get; set; }

        [Phone]
        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? ProfileImageUrl { get; set; }

        public bool? IsActive { get; set; }
    }
}
