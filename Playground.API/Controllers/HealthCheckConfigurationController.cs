using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Playground.API.Extensions;
using Playground.Application.Commands;
using Playground.Application.Queries;
using Playground.Domain.Models.HealthChecks;
using Playground.Domain.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using Route = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Playground.API.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/Configuration")]
    public class HealthCheckConfigurationController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IConfigurationQueries _configurationQueries;

        public HealthCheckConfigurationController(
            IMediator mediator,
            IConfigurationQueries configurationQueries)
        {
            _mediator = mediator;
            _configurationQueries = configurationQueries;
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
            OperationId = "CreateConfiguration",
            Tags = new[] {"Configuration"})]
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
        [Route("{id}")]
        [HttpGet]
        [SwaggerOperation(
            OperationId = "GetConfiguration",
            Tags = new[] {"Configuration"})]
        [SwaggerResponse(200, "Configuration found", typeof(ConfigurationView))]
        public IActionResult GetConfiguration(Guid id)
        {
            var response = _configurationQueries.Get(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        /// <summary>
        /// Get a paged result of configurations
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetPagedConfigurations")]
        [SwaggerOperation(
            OperationId = "GetPagedConfigurations",
            Tags = new[] {"Configuration"})]
        [SwaggerResponse(200, "Configurations found", typeof(List<ConfigurationView>))]
        public IActionResult GetPagedConfigurations([FromQuery] PageParameters parameters)
        {
            return Ok(_configurationQueries.GetPagedConfigurations(parameters.PageNumber, parameters.PageSize));
        }

        [HttpGet]
        [Route("{id}/checks")]
        [SwaggerOperation(
            OperationId = "GetChecks",
            Tags =  new []{"Checks"})]
        [SwaggerResponse(200, "Checks found", typeof(List<HealthCheckViewModel>))]
        public IActionResult GetChecks(Guid id)
        {
            return Ok(_configurationQueries.GetChecksByConfiguration(id));
        }
    }
}