namespace FidelizeDotz.Services.Api.Domain.Application.Dtos.Settings
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public int TimeExpiration { get; set; }

        public string Issuer { get; set; }

        public string Valid { get; set; }
    }
}