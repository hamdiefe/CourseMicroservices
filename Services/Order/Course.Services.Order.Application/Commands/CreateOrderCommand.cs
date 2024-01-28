﻿using Course.Services.Order.Application.Dtos;
using Course.Shared.Dtos;
using MediatR;
using System.Collections.Generic;

namespace Course.Services.Order.Application.Queries
{
    public class CreateOrderCommand : IRequest<Response<CreatedOrderDto>>
    {
        public string BuyerId { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }

        public AddressDto Address { get; set; }
    }
}
