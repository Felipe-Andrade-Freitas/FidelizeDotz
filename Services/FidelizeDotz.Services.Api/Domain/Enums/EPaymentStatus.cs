using System.ComponentModel;

namespace FidelizeDotz.Services.Api.Domain.Enums
{
    public enum EPaymentStatus
    {
        [Description("AWAITINGPAYMENT")] AwaitingPayment = 1,

        [Description("PAID")] Paid = 2,

        [Description("CANCELLED")] Cancelled = 3
    }
}