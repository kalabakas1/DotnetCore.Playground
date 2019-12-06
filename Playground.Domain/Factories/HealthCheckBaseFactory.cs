using Playground.Domain.Constants;
using Playground.Domain.Dtos;
using Playground.Domain.Models;
using Playground.Domain.Models.HealthChecks;

namespace Playground.Domain.Factories
{
    public abstract class HealthCheckBaseFactory : IHealthCheckFactory
    {
        public abstract string Type { get; }

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