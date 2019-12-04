using Playground.Domain.Constants;
using Playground.Domain.Extensions;
using Playground.Domain.Factories;
using Playground.Domain.Models;

namespace Playground.Domain.Services
{
    public class HealthCheckService : IHealthCheckService
    {
        private readonly IReportFactory _reportFactory;

        public HealthCheckService(IReportFactory reportFactory)
        {
            _reportFactory = reportFactory;
        }
        
        public Report Execute(MonitoringConfiguration configuration)
        {
            using(var report = _reportFactory.Create(GetType()))
            {
                if (configuration == null)
                {
                    report.Message = string.Format(ExceptionMessage.ObjectNull, nameof(configuration));
                    return report;
                }

                if (configuration.HealthChecks == null)
                {
                    report.Message = string.Format(ExceptionMessage.ObjectNull, nameof(configuration.HealthChecks));
                    return report;
                }
                
                foreach (var healthCheck in configuration?.HealthChecks)
                {
                    using (var checkReport = report.Create($"{healthCheck.Alias}:{healthCheck.Name}"))
                    {
                        var notification = healthCheck.Run();
                        checkReport.Failed = notification.HasError();
                        notification.Errors.ForEach(x => checkReport.Create().WithMessage(x.Message).WithException(x.Exception));
                    }
                }

                return report;
            }
        }
    }
}