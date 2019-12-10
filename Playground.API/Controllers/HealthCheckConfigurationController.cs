using System;
using System.Net.Http;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public Task<Guid> CreateConfiguration([FromBody] CreateConfigurationCommand request) => _mediator.Send(request);
    }
}