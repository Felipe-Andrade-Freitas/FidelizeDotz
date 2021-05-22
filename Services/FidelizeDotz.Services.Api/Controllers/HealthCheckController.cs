using System.Threading.Tasks;
using FidelizeDotz.Services.Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using static FidelizeDotz.Services.Api.Domain.Enums.EReturnMessageType;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace FidelizeDotz.Services.Api.Controllers
{
    [ApiVersion("1.0")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("v{version:apiVersion}/health-check")]
    public class HealthCheckController : ControllerBase
    {
        /// <summary>
        ///     Validação da conexão com os servidores
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(typeof(ReturnMessage), Status200OK)]
        [ProducesResponseType(typeof(ReturnMessage), Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnMessage), Status500InternalServerError)]
        [HttpGet]
        public async Task<ReturnMessage> HealthCheck()
        {
            return new ReturnMessage(true, SuccessOk);
        }
    }
}