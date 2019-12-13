using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Playground.API.Extensions;
using Playground.Application.Commands;
using Playground.Domain.Models.HealthChecks;
using Playground.Domain.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace Playground.API.Controllers
{
    [ApiController]
    [Route("api/Configuration")]
    public class HealthCheckConfigurationController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IHealthCheckConfigurationRepository _healthCheckConfigurationRepository;

        public HealthCheckConfigurationController(
            IMediator mediator,
            IHealthCheckConfigurationRepository healthCheckConfigurationRepository)
        {
            _mediator = mediator;
            _healthCheckConfigurationRepository = healthCheckConfigurationRepository;
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

        //ToDo: Redo this to return a query object (not domain)
        /// <summary>
        /// Get configuration
        /// </summary>
        /// <remarks>
        /// Gets the configuration by the GUID id returned at creation
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Get existing configuration",
            OperationId = "GetConfiguration",
            Tags = new []{"Configuration"})]
        [SwaggerResponse(200, "Configuration found", typeof(HealthCheckConfiguration))]
        public IActionResult GetConfiguration(Guid id)
        {
            var response = _healthCheckConfigurationRepository.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}