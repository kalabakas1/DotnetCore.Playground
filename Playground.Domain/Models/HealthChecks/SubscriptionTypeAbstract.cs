namespace Playground.Domain.Models.HealthChecks
{
    public abstract class SubscriptionTypeAbstract : AggregateBase<int>
    {
        public abstract string Type { get; }
        protected SubscriptionTypeAbstract()
        {
            
        }
        
        public abstract bool CanAddNewHealthCheck(HealthCheckConfiguration configuration, HealthCheckAbstract healthCheck);
    }
}