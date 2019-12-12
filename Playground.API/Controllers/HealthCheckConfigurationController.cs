using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Playground.API.Extensions;
using Playground.Application.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace Playground.API.Controllers
{
    [ApiController]
    [Route("api/Configuration")]
    public class HealthCheckConfigurationController : Controller
    {
        private readonly IMediator _mediator;

        public HealthCheckConfigurationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create new configuration
        /// </summary>
        /// <remarks>
        /// Used to create an entirely new configuration item:
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a new configuration",
            OperationId = "CreateConfiguration",
            Tags = new []{"Configuration"})]
        [SwaggerResponse(200, "Configuration created", typeof(Guid))]
        public IActionResult CreateConfiguration([FromBody] CreateConfigurationCommand request)
        {
            var response = _mediator.Send(request).Result;
            if (response.HasError())
            {
                return BadRequest(response.ToHttpError(HttpStatusCode.BadRequest));
            }

            return Ok(response.Value);
        }
    }
}