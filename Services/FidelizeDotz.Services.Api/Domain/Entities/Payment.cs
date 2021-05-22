using System;
using FidelizeDotz.Services.Api.Domain.Enums;

namespace FidelizeDotz.Services.Api.Domain.Entities
{
    public class Payment : EntityBase
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public ETypePayment TypePayment { get; set; }
        public EPaymentStatus Status { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}