﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Order
{
    public class OrderDetailDto : OrderDto
    {
        public int Id { get; set; }       
        public int BuyerId { get; set; }
        public string BuyerEmail { get; set; } = null!;
        public string? BuyerPhone { get; set; }
        public ProductSimpleDto Product { get; set; } = null!;
    }
}
