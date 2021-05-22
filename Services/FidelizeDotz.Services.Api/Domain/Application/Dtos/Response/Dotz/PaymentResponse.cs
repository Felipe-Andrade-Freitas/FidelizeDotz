using FidelizeDotz.Services.Api.Domain.Enums;

namespace FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Dotz
{
    public class PaymentResponse
    {
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public ETypePayment TypePayment { get; set; }
        public EPaymentStatus Status { get; set; }
    }
}