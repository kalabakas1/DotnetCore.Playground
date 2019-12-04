using Playground.Domain.Models;

namespace Playground.Domain.Services
{
    public interface IHealthCheckService
    {
        Report Execute(MonitoringConfiguration configuration);
    }
}