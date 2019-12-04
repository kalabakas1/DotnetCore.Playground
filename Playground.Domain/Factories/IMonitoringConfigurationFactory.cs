using System.Collections.Generic;
using Playground.Data;
using Playground.Data.Dtos;
using Playground.Domain.Models;

namespace Playground.Domain.Factories
{
    public interface IMonitoringConfigurationFactory
    {
        Notification<MonitoringConfiguration> Create(List<HealthCheckDto> healthCheckDtos);
    }
}