using System;
using System.Collections.Generic;

namespace Playground.Domain.Models.HealthChecks
{
    public class HealthCheckConfiguration : AggregateBase<Guid>
    {
        public List<HealthCheckAbstract> HealthChecks { get; set; }
        public int Retries { get; set; }
        public int SleepInMillsBetweenRetry { get; set; }

        public HealthCheckConfiguration()
        {
            HealthChecks = new List<HealthCheckAbstract>();
        }
        
        public void AddHealthCheck(HealthCheckAbstract healthCheck)
        {
            HealthChecks.Add(healthCheck);
        }
    }    
}