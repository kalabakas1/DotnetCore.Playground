using System;
using System.Linq;

namespace Playground.Domain.Models.HealthChecks
{
    public class TwoOfEachSubscriptionType : SubscriptionTypeAbstract
    {
        private const int MaxCountOfEach = 2;
        public TwoOfEachSubscriptionType()
        {
            Id = 2;
        }

        public override string Type => "TwoOfEach";

        public override bool CanAddNewHealthCheck(HealthCheckConfiguration configuration, HealthCheckAbstract healthCheck)
        {
            var type = healthCheck.GetType();
            var countOfType = configuration.HealthChecks.Count(x => x.GetType() == type);
            return countOfType + 1 <= MaxCountOfEach;
        }
    }
}