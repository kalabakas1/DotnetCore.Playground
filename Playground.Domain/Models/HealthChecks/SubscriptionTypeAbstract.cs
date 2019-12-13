using Playground.Domain.Enums;

namespace Playground.Domain.Models.HealthChecks
{
    public abstract class SubscriptionTypeAbstract : AggregateBase<int>
    {
        public abstract SubscriptionTypes Type { get; }
        protected SubscriptionTypeAbstract()
        {
            
        }
        
        public abstract bool CanAddNewHealthCheck(HealthCheckConfiguration configuration, HealthCheckAbstract healthCheck);
    }
}