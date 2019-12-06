namespace Playground.Domain.Models.HealthChecks
{
    public class DefaultHealthCheck : HealthCheckAbstract
    {
        public DefaultHealthCheck() : base(Constants.HealthChecks.Default)
        {
            
        }
        
        public DefaultHealthCheck(string name) : base(Constants.HealthChecks.Default, name)
        {
            
        }
        
        protected override Notification<bool> Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}