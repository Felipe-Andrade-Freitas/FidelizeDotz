using FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Product;

namespace FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Order
{
    public class OrderItemResponse
    {
        public string SkuCode { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public ProductResponse Product { get; set; }
    }
}