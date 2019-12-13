using Playground.Domain.Enums;

namespace Playground.Domain.Models.HealthChecks
{
    public class LimitedHealthCheckSubscriptionType : SubscriptionTypeAbstract
    {
        public LimitedHealthCheckSubscriptionType()
        {
            this.Id = 1;
        }

        private const int MaxChecks = 3;

        public override SubscriptionTypes Type => SubscriptionTypes.LimitedHealthCheck;

        public override bool CanAddNewHealthCheck(HealthCheckConfiguration configuration, HealthCheckAbstract healthCheck)
        {
            return configuration.HealthChecks.Count < MaxChecks;
        }
    }
}