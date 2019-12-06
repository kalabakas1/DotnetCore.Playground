using System;
using System.Collections.Generic;
using Playground.Domain.Models.HealthChecks;

namespace Playground.Domain.Repositories
{
    public interface IHealthCheckConfigurationRepository
    {
        HealthCheckConfiguration GetById(Guid id);
        void Add(HealthCheckConfiguration configuration);
        void Update(HealthCheckConfiguration configuration);
        List<HealthCheckConfiguration> GetAll();
    }
}