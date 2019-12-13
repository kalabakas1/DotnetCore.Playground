namespace Playground.Domain.Models.HealthChecks
{
    public class DefaultHealthCheck : HealthCheckAbstract
    {
        public DefaultHealthCheck() : base(Enums.HealthChecks.Default)
        {
            
        }
        
        public DefaultHealthCheck(string name) : base(Enums.HealthChecks.Default, name)
        {
            
        }
        
        protected override Notification<bool> Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}