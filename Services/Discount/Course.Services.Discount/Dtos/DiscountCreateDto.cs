﻿namespace Course.Services.Discount.Dtos
{
    public class DiscountCreateDto
    {
        public string UserId { get; set; }

        public string Code { get; set; }

        public int Rate { get; set; }
    }
}
