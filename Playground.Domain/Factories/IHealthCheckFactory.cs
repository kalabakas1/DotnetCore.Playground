using Playground.Domain.Dtos;
using Playground.Domain.Enums;
using Playground.Domain.Models;
using Playground.Domain.Models.HealthChecks;
using HealthChecks = Playground.Domain.Constants.HealthChecks;

namespace Playground.Domain.Factories
{
    public interface IHealthCheckFactory
    {
        Enums.HealthChecks Type { get; }
        Notification<HealthCheckAbstract> Create(HealthCheckDto configuration);
    }
}