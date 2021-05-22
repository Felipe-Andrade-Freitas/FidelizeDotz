using System.ComponentModel;

namespace FidelizeDotz.Services.Api.Domain.Enums
{
    public enum ETypePayment
    {
        [Description("DOTS")] Dots = 1,
        [Description("MONEY")] Mondey = 2,
        [Description("CREDCARD")] CredCard = 3,
        [Description("PIX")] Pix = 3,
    }
}