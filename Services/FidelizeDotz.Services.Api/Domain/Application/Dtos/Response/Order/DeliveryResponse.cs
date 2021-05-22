using FidelizeDotz.Services.Api.Domain.Enums;

namespace FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Order
{
    public class DeliveryResponse
    {
        public EDeliveryStatus Status { get; set; }
        public string TrackingCode { get; set; }
        public AddressResponse Address { get; set; }
    }
}