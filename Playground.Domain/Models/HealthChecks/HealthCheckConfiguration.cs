using System;
using System.Collections.Generic;

namespace Playground.Domain.Models.HealthChecks
{
    public class HealthCheckConfiguration : AggregateBase<Guid>
    {
        public SubscriptionTypeAbstract SubscriptionType { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<HealthCheckAbstract> HealthChecks { get; set; }
        public int Retries { get; set; }
        public int SleepInMillsBetweenRetry { get; set; }

        public HealthCheckConfiguration()
        {
            HealthChecks = new List<HealthCheckAbstract>();
        }

        public HealthCheckConfiguration(int retries, int sleepInMillsBetweenRetry,
            List<HealthCheckAbstract> healthChecks, SubscriptionTypeAbstract subscriptionType)
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;
            Retries = retries;
            SleepInMillsBetweenRetry = sleepInMillsBetweenRetry;
            HealthChecks = healthChecks;
            SubscriptionType = subscriptionType;
        }
        
        public void AddHealthCheck(HealthCheckAbstract healthCheck)
        {
            if (SubscriptionType == null || SubscriptionType.CanAddNewHealthCheck(this, healthCheck))
            {
                HealthChecks.Add(healthCheck);
            }
        }
    }    
}