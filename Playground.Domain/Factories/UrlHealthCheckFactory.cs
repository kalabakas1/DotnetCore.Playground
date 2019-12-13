using System;
using System.Linq;
using Playground.Domain.Constants;
using Playground.Domain.Dtos;
using Playground.Domain.Models;
using Playground.Domain.Models.HealthChecks;

namespace Playground.Domain.Factories
{
    public class UrlHealthCheckFactory : HealthCheckBaseFactory, IHealthCheckFactory
    {
        public override Enums.HealthChecks Type => Enums.HealthChecks.UrlPing;

        public new Notification<HealthCheckAbstract> Create(HealthCheckDto configuration)
        {
            var notifications = base.Create(configuration);
            if (!Uri.TryCreate(configuration.Path, UriKind.Absolute, out var validatedUri))
            {
                notifications.AddError(string.Format(ExceptionMessage.NotValidUrl, configuration.Path));
            }

            if (configuration.ValidResponses == null || !configuration.ValidResponses.Any())
            {
                notifications.AddError(string.Format(ExceptionMessage.ParameterMustBeDefined, nameof(configuration.ValidResponses)));
            }

            if (notifications.HasError())
            {
                return notifications;
            }
            
            notifications.Value = new UrlPingHealthCheck(configuration.Name, validatedUri.AbsoluteUri, configuration.ValidResponses, configuration.Headers);
            return notifications;
        }
    }
}