using System;
using System.Net.Http;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Playground.API.Extensions;
using Playground.Application.Commands;

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

        [HttpPost]
        public IActionResult CreateConfiguration([FromBody] CreateConfigurationCommand request)
        {
            var response = _mediator.Send(request).Result;
            if (response.HasError())
            {
                return BadRequest(response.ConvertToHttpError(400));
            }

            return Ok(response.Value);
        }
    }
}