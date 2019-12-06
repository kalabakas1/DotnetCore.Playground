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

        protected HealthCheckAbstract(string alias)
        {
            Alias = alias;
        }

        protected HealthCheckAbstract(string alias, string name)
        {
            Alias = alias;
            Name = name;
        }
        
        public string Alias { get; }
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