using System.Collections.Generic;
using Playground.Domain.Models.HealthChecks;

namespace Playground.Domain.Models
{
    public class MonitoringConfiguration
    {
        public List<HealthCheckAbstract> HealthChecks { get; }

        public MonitoringConfiguration()
        {
            HealthChecks = new List<HealthCheckAbstract>();
        }
        
        public void AddHealthCheck(HealthCheckAbstract healthCheck)
        {
            HealthChecks.Add(healthCheck);
        }
    }    
}