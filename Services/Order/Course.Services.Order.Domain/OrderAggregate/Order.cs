using Course.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Course.Services.Order.Domain.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public Order()
        {                
        }
        public Order(Address address, string buyerId)
        {
            CreatedOn = DateTime.Now;
            Address = address;
            BuyerId = buyerId;
            _orderItems = new List<OrderItem>();
        }
        public DateTime CreatedOn { get; private set; }

        public Address Address { get; private set; }

        public string BuyerId { get; private set; }

        private readonly List<OrderItem> _orderItems;

        public IReadOnlyCollection<OrderItem> OrderItems { get { return _orderItems; } }

        public decimal GetTotalPrice { get { return _orderItems.Sum(x => x.Price); } }

        public void AddOrderItem(string productId, string productName, string pictureUrl, decimal price)
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);

            if (!existProduct) _orderItems.Add(new OrderItem(productId, productName, pictureUrl, price));
        }
    }
}
