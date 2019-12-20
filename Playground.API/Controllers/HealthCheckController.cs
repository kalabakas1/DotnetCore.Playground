using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Playground.Application.Queries;
using Playground.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace Playground.API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class HealthCheckController : Controller
    {
        private readonly IConfigurationQueries _configurationQueries;

        public HealthCheckController(IConfigurationQueries configurationQueries)
        {
            _configurationQueries = configurationQueries;
        }

        /// <summary>
        /// Get the health checks associated with a configuration id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Configuration/{id}/Checks")]
        [SwaggerOperation(
            OperationId = "GetChecksForConfiguration",
            Tags = new[] {"Checks"})]
        [SwaggerResponse(200, "Checks found", typeof(List<HealthCheckViewModel>))]
        public IActionResult GetChecks(Guid id)
        {
            return Ok(_configurationQueries.GetChecksByConfiguration(id));
        }
    }
}