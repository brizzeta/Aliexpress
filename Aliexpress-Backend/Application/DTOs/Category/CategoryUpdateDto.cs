using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Category
{
    public class CategoryUpdateDto
    {
        [StringLength(100)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? ParentCategoryId { get; set; }
    }
}
