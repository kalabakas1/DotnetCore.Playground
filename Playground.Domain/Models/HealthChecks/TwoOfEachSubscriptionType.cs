using System;
using System.Linq;
using Playground.Domain.Enums;

namespace Playground.Domain.Models.HealthChecks
{
    public class TwoOfEachSubscriptionType : SubscriptionTypeAbstract
    {
        private const int MaxCountOfEach = 2;
        public TwoOfEachSubscriptionType()
        {
            Id = 2;
        }

        public override SubscriptionTypes Type => SubscriptionTypes.TwoOfEach;

        public override bool CanAddNewHealthCheck(HealthCheckConfiguration configuration, HealthCheckAbstract healthCheck)
        {
            var type = healthCheck.GetType();
            var countOfType = configuration.HealthChecks.Count(x => x.GetType() == type);
            return countOfType + 1 <= MaxCountOfEach;
        }
    }
}