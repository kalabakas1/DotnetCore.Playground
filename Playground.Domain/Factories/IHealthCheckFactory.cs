using Playground.Domain.Dtos;
using Playground.Domain.Models;
using Playground.Domain.Models.HealthChecks;

namespace Playground.Domain.Factories
{
    public interface IHealthCheckFactory
    {
        string Type { get; }
        Notification<HealthCheckAbstract> Create(HealthCheckDto configuration);
    }
}