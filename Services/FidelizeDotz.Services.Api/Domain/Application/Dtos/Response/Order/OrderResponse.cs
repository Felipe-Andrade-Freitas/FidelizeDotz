using System;
using System.Collections.Generic;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Dotz;
using FidelizeDotz.Services.Api.Domain.Enums;

namespace FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Order
{
    public class OrderResponse
    {
        /// <summary>
        /// Code of the Order
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Status of the Order
        /// </summary>
        public EOrderStatus Status { get; set; }

        public ICollection<DeliveryResponse> Deliveries { get; set; }
        public ICollection<OrderItemResponse> OrderItems { get; set; }
        public ICollection<PaymentResponse> Payments { get; set; }
    }
}