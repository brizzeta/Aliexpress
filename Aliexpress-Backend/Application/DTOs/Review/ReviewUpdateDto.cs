using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Review
{
    public class ReviewUpdateDto
    {
        [Range(1, 5)]
        public decimal? Rating { get; set; }

        public string? Comment { get; set; }
    }
}
