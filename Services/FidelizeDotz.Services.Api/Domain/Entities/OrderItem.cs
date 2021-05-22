using System;

namespace FidelizeDotz.Services.Api.Domain.Entities
{
    public class OrderItem : EntityBase
    {
        public Guid Id { get; set; }
        public string SkuCode { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}