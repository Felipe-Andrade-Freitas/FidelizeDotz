using System;
using System.Collections.Generic;

namespace FidelizeDotz.Services.Api.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string SkuCode { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int PriceDots { get; set; }
        public int Cashback { get; set; }
        public string Image { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}