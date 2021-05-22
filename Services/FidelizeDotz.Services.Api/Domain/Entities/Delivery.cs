using System;
using FidelizeDotz.Services.Api.Domain.Enums;

namespace FidelizeDotz.Services.Api.Domain.Entities
{
    public class Delivery : EntityBase
    {
        public Guid Id { get; set; }
        public EDeliveryStatus Status { get; set; }
        public string TrackingCode { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid AddressId { get; set; }
        public Address Address { get; set; }
    }
}