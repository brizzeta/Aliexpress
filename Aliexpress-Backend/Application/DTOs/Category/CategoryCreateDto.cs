using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Category
{
    public class CategoryCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int? ParentCategoryId { get; set; }
    }
}
