using System;
using System.Collections.Generic;
using FidelizeDotz.Services.Api.Domain.Enums;

namespace FidelizeDotz.Services.Api.Domain.Entities
{
    public class Order : EntityBase
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public EOrderStatus Status { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Delivery> Deliveries { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}