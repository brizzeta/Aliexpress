using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Review
{
    public class ReviewCreateDto
    {
        [Required]
        public int ProductID { get; set; }

        [Required]
        [Range(1, 5)]
        public decimal Rating { get; set; }

        public string? Comment { get; set; }
    }
}
