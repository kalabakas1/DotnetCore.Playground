using System;
using System.Collections.Generic;
using Playground.Domain.Dtos;
using Playground.Domain.Models;
using Playground.Domain.Models.HealthChecks;

namespace Playground.Domain.Factories
{
    public interface IHealthCheckConfigurationFactory
    {
        Notification<HealthCheckConfiguration> Create(Guid? configurationId = null);
    }
}