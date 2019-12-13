using System;
using Playground.Domain.EventArgs;

namespace Playground.Domain.Models.HealthChecks
{
    public abstract class HealthCheckAbstract
    {
        public static event EventHandler<HealthCheckEventArgs> OnHealthCheckEnd;

        protected HealthCheckAbstract()
        {
            
        }

        protected HealthCheckAbstract(Enums.HealthChecks alias)
        {
            Alias = alias;
        }

        protected HealthCheckAbstract(Enums.HealthChecks alias, string name)
        {
            Alias = alias;
            Name = name;
        }
        
        public Enums.HealthChecks Alias { get; }
        public string Name { get; }
        public Guid Id { get; set; }

        public Notification Run()
        {
            var notification = Execute();
            OnHealthCheckEnd?.Invoke(this, new HealthCheckEventArgs(this, notification));

            return notification;
        }

        protected abstract Notification<bool> Execute();
    }
}