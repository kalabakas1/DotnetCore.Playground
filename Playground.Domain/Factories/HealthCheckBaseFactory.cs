using Playground.Domain.Constants;
using Playground.Domain.Dtos;
using Playground.Domain.Models;
using Playground.Domain.Models.HealthChecks;
using HealthChecks = Playground.Domain.Enums.HealthChecks;

namespace Playground.Domain.Factories
{
    public abstract class HealthCheckBaseFactory : IHealthCheckFactory
    {
        public abstract Enums.HealthChecks Type { get; }

        public Notification<HealthCheckAbstract> Create(HealthCheckDto configuration)
        {
            var notification = new Notification<HealthCheckAbstract>();
            if (string.IsNullOrEmpty(configuration.Name))
            {
                notification.AddError(string.Format(ExceptionMessage.ParameterMustBeDefined, nameof(configuration.Name)));
            }

            return notification;
        }
    }
}