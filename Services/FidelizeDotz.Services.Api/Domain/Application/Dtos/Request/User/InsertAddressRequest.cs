namespace FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.User
{
    public class InsertAddressRequest
    {
        public string Street { get; set; }
        public int Number { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}