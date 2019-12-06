using Playground.Domain.Models;
using Playground.Domain.Models.HealthChecks;

namespace Playground.Domain.Services
{
    public interface IHealthCheckService
    {
        Report Execute(HealthCheckConfiguration configuration);
    }
}