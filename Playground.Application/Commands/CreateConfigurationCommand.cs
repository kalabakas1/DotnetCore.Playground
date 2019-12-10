using System;
using System.Collections.Generic;
using MediatR;
using Playground.Domain.Dtos;

namespace Playground.Application.Commands
{
    public class CreateConfigurationCommand : IRequest<Guid>
    {
        public int Retries { get; set; }
        public int SleepInMillsBetweenRetry { get; set; }
        public List<HealthCheckDto> HealthChecks { get; set; }
        public string SubscriptionTypeName { get; set; }
    }
}