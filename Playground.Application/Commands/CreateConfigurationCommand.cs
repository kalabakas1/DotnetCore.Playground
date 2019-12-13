using System;
using System.Collections.Generic;
using MediatR;
using Playground.Domain.Dtos;
using Playground.Domain.Enums;
using Playground.Domain.Models;

namespace Playground.Application.Commands
{
    public class CreateConfigurationCommand : IRequest<Notification<Guid>>
    {
        public string Name { get; set; }
        public int Retries { get; set; }
        public int SleepInMillsBetweenRetry { get; set; }
        public List<HealthCheckDto> HealthChecks { get; set; }
        public SubscriptionTypes SubscriptionTypeName { get; set; }
    }
}