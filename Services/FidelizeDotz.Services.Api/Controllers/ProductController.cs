using System;
using System.Threading.Tasks;
using FidelizeDotz.Services.Api.CrossCutting.Infra;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Product;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Product;
using FidelizeDotz.Services.Api.Domain.Application.Services.Interfaces;
using FidelizeDotz.Services.Api.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace FidelizeDotz.Services.Api.Controllers
{
    [ApiVersion("1.0")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("v{version:apiVersion}/products")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        ///     Get all products available for redemption
        /// </summary>
        /// <response code="200">Success created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(ReturnMessage<IPagedList<ProductResponse>>), Status200OK)]
        [ProducesResponseType(typeof(ReturnMessage), Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnMessage), Status500InternalServerError)]
        [HttpGet("available-for-redemption")]
        public Task<ReturnMessage<IPagedList<ProductResponse>>> ListProductsAvailableForRedemptionAsync() => _productService.ListProductsAvailableForRedemptionAsync();
        
        /// <summary>
        ///     Insert a new product
        /// </summary>
        /// <response code="200">Success created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(ReturnMessage), Status200OK)]
        [ProducesResponseType(typeof(ReturnMessage), Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnMessage), Status500InternalServerError)]
        [HttpPost]
        public Task<ReturnMessage> InsertProductAsync(InsertProductRequest request) => _productService.InsertProductAsync(request);

        /// <summary>
        ///     Insert a new product
        /// </summary>
        /// <response code="200">Success created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(ReturnMessage), Status200OK)]
        [ProducesResponseType(typeof(ReturnMessage), Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnMessage), Status500InternalServerError)]
        [HttpPost("{id}/rescued")]
        public Task<ReturnMessage> RescuedProductAsync(Guid id) => _productService.RescuedProductAsync(id);
    }
}