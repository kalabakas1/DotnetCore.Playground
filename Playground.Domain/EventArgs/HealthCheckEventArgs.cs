using Playground.Domain.Models;
using Playground.Domain.Models.HealthChecks;

namespace Playground.Domain.EventArgs
{
    public class HealthCheckEventArgs : System.EventArgs
    {
        public HealthCheckEventArgs(HealthCheckAbstract healthCheck)
        {
            HealthCheck = healthCheck;
        }

        public HealthCheckEventArgs(HealthCheckAbstract healthCheck, Notification notification)
        {
            HealthCheck = healthCheck;
            Notification = notification;
        }
        
        public HealthCheckAbstract HealthCheck { get; set; }
        public Notification Notification { get; set; }
    }
}