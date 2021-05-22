using System;
using System.Threading.Tasks;
using FidelizeDotz.Services.Api.CrossCutting.Infra;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Request.Dotz;
using FidelizeDotz.Services.Api.Domain.Application.Dtos.Response.Dotz;
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
    [Route("v{version:apiVersion}/dots")]
    [Authorize]
    public class DotController : ControllerBase
    {
        private readonly IDotzService _dotzService;

        public DotController(IDotzService dotzService)
        {
            _dotzService = dotzService;
        }

        /// <summary>
        ///     Get balance of the dot
        /// </summary>
        /// <response code="200">Success created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(ReturnMessage<DotBalanceResponse>), Status200OK)]
        [ProducesResponseType(typeof(ReturnMessage), Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnMessage), Status500InternalServerError)]
        [HttpGet("balance")]
        public Task<ReturnMessage<DotBalanceResponse>> GetBalanceDotsAsync() => _dotzService.GetBalanceDotsAsync();

        /// <summary>
        ///     List extract of the filter and paged list
        /// </summary>
        /// <response code="200">list of the dots</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(ReturnMessage<IPagedList<DotResponse>>), Status200OK)]
        [ProducesResponseType(typeof(ReturnMessage), Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnMessage), Status500InternalServerError)]
        [HttpGet]
        public Task<ReturnMessage<IPagedList<DotResponse>>> GetAllDotsAsync([FromQuery] GetAllDotRequest request) => _dotzService.GetAllDotsAsync(request);

        /// <summary>
        ///     Insert a new dot
        /// </summary>
        /// <response code="200">Success created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(ReturnMessage), Status200OK)]
        [ProducesResponseType(typeof(ReturnMessage), Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnMessage), Status500InternalServerError)]
        [HttpPost]
        public Task<ReturnMessage> InsertDotAsync(InsertDotRequest request) => _dotzService.InsertDotAsync(request);

        /// <summary>
        ///     Rescued of the dot
        /// </summary>
        /// <response code="200">Success created</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(ReturnMessage<DotResponse>), Status200OK)]
        [ProducesResponseType(typeof(ReturnMessage), Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnMessage), Status500InternalServerError)]
        [HttpPost("rescued")]
        public Task<ReturnMessage<DotResponse>> RescuedDot(RescuedDotRequest request) => _dotzService.RescuedDot(request);
    }
}