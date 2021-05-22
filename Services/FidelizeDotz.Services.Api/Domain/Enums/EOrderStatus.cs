using System.ComponentModel;

namespace FidelizeDotz.Services.Api.Domain.Enums
{
    public enum EOrderStatus
    {
        [Description("CREATED")] Created = 1,

        [Description("AWAITINGPAYMENT")] AwaitingPayment = 2,

        [Description("PAID")] Paid = 3,

        [Description("CANCELLED")] Cancelled = 4
    }
}