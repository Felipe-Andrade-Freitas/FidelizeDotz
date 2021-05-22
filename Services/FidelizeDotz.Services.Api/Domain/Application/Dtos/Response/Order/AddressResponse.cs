namespace FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Order
{
    public class AddressResponse
    {
        public string Street { get; set; }
        public int Number { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}