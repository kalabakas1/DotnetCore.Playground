using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Playground.API.Application.Commands;

namespace Playground.API.Controllers
{
    [ApiController]
    [Route("api/Configuration")]
    public class HealthCheckConfigurationController : Controller
    {
        [HttpPost]
        public ObjectResult CreateHealthCheckConfiguration([FromBody] CreateConfigurationCommand createConfigurationCommand)
        {
            return Ok(Guid.NewGuid());
        }
    }
}