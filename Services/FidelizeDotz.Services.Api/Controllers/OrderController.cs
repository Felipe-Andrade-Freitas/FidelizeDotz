using System.Threading.Tasks;
using FidelizeDotz.Services.Api.CrossCutting.Infra;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Order;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Order;
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
    [Route("v{version:apiVersion}/orders")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        ///     Get all products available for redemption
        /// </summary>
        /// <response code="200">Success created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(ReturnMessage<IPagedList<OrderResponse>>), Status200OK)]
        [ProducesResponseType(typeof(ReturnMessage), Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnMessage), Status500InternalServerError)]
        [HttpGet]
        public Task<ReturnMessage<IPagedList<OrderResponse>>> ListOrderForUser([FromQuery] ListOrderForUserRequest request) => _orderService.ListOrderForUser(request);
    }
}