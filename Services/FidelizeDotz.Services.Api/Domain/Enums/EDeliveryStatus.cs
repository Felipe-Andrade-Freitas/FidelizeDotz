using System.ComponentModel;

namespace FidelizeDotz.Services.Api.Domain.Enums
{
    public enum EDeliveryStatus
    {
        [Description("AWAITINGDISPATCH")] AwaitingDispatch = 1,

        [Description("SENT")] Sent = 2,

        [Description("DELIVERED")] Delivered = 3,

        [Description("RETURNED")] Returned = 3
    }
}