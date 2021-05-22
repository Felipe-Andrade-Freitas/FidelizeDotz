using System.Threading.Tasks;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.User;
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
    [Route("v{version:apiVersion}/users")]
    [Authorize]
    public class UserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        ///     Get all products available for redemption
        /// </summary>
        /// <response code="200">Success created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(ReturnMessage), Status200OK)]
        [ProducesResponseType(typeof(ReturnMessage), Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnMessage), Status500InternalServerError)]
        [HttpPost("addresses")]
        public Task<ReturnMessage> InsertAddressAsync(InsertAddressRequest request) => _userService.InsertAddressAsync(request);
    }
}