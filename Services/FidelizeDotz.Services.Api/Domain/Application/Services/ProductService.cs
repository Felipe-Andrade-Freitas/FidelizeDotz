using System;
using System.Threading.Tasks;
using FidelizeDotz.Services.Api.CrossCutting.Bases;
using FidelizeDotz.Services.Api.CrossCutting.Constants;
using FidelizeDotz.Services.Api.CrossCutting.Infra;
using FidelizeDotz.Services.Api.Domain.Application.Dtos;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Dotz;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Product;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Product;
using FidelizeDotz.Services.Api.Domain.Application.Services.Interfaces;
using FidelizeDotz.Services.Api.Domain.Entities;
using FidelizeDotz.Services.Api.Domain.Enums;
using FidelizeDotz.Services.Api.Domain.Infra.Interfaces;

namespace FidelizeDotz.Services.Api.Domain.Application.Services
{
    public class ProductService : ServiceBase, IProductService
    {
        private readonly IDotzService _dotzService;
        public ProductService(IUnitOfWork unitOfWork, IAdapter adapter, UserLogged userLogged, IDotzService dotzService) : base(unitOfWork, adapter, userLogged: userLogged)
        {
            _dotzService = dotzService;
        }

        public async Task<ReturnMessage> InsertProductAsync(InsertProductRequest request)
        {
            var product = Adapter.ConvertTo<InsertProductRequest, Product>(request);
            await UnitOfWork.GetRepository<Product>().InsertAsync(product);
            await UnitOfWork.SaveChangesAsync();

            return new ReturnMessage(true, EReturnMessageType.SuccessCreated);
        }

        public async Task<ReturnMessage<IPagedList<ProductResponse>>> ListProductsAvailableForRedemptionAsync()
        {
            var balance = await _dotzService.GetBalanceDotsAsync();
            var listProducts = await UnitOfWork.GetRepository<Product>()
                .GetPagedListAsync(predicate: _ => _.PriceDots <= balance.Data.Balance);
            return new ReturnMessage<IPagedList<ProductResponse>>(
                Adapter.ConvertTo<PagedList<Product>, PagedList<ProductResponse>>((PagedList<Product>)listProducts));
        }

        public async Task<ReturnMessage> RescuedProductAsync(Guid id)
        {
            var product = await UnitOfWork.GetRepository<Product>().GetFirstOrDefaultAsync(predicate: _ => _.Id == id);
            if (product is null)
                return new ReturnMessage(ErrorsConstants.ProductNotFound, EReturnMessageType.ClientErrorBadRequest);

            var rescued = await _dotzService.RescuedDot(new RescuedDotRequest { Quantity = product.PriceDots });
            if (!rescued.Success)
                return rescued;
            
            var cashback = await _dotzService.InsertDotAsync(new InsertDotRequest { Quantity = product.Cashback });
            if (!cashback.Success)
                return cashback;

            InsertOrder(id, product.SkuCode, 1, product.Price);

            await UnitOfWork.SaveChangesAsync();

            return new ReturnMessage(true, EReturnMessageType.SuccessCreated);
        }

        #region [ Private ]

        private async void InsertOrder(Guid productId, string skuCode, int quantity, decimal price)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                Code = $"O{DateTime.Now:YYYYMMDDhhmm}",
                Status = EOrderStatus.Paid,
                UserId = UserLogged.Id
            };

            await UnitOfWork.GetRepository<Order>().InsertAsync(order);

            var orderId = order.Id;
            InsertOrderItem(productId, orderId, skuCode, quantity, price);
            InsertPayment(orderId, price);
            InsertDelivery(orderId);
        }

        private async void InsertOrderItem(Guid productId, Guid orderId, string skuCode, int quantity, decimal price)
        {
            var orderItem = new OrderItem
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity,
                SkuCode = skuCode,
                UnitPrice = price,
                TotalPrice = quantity * price
            };

            await UnitOfWork.GetRepository<OrderItem>().InsertAsync(orderItem);
        }

        private async void InsertPayment(Guid orderId, decimal price)
        {
            var payment = new Payment
            {
                Code = $"P{DateTime.Now:YYYYMMDDhhmm}",
                OrderId = orderId,
                Status = EPaymentStatus.Paid,
                TypePayment = ETypePayment.Dots,
                Amount = price
            };

            await UnitOfWork.GetRepository<Payment>().InsertAsync(payment);
        }

        private async void InsertDelivery(Guid orderId)
        {
            var addressId = await UnitOfWork.GetRepository<Address>().GetFirstOrDefaultAsync(_ => _.Id, _ => _.UserId == UserLogged.Id);

            var delivery = new Delivery
            {
                Status = EDeliveryStatus.AwaitingDispatch,
                TrackingCode = $"BR-{DateTime.Now:YYYYMMDDhhmm}",
                OrderId = orderId,
                AddressId = addressId
            };

            await UnitOfWork.GetRepository<Delivery>().InsertAsync(delivery);
        }

        #endregion
    }
}