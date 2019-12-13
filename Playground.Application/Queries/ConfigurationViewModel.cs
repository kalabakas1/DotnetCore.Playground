using System;

namespace Playground.Application.Queries
{
    public class ConfigurationView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Retries { get; set; }
        public int SleepInMills { get; set; }
        public int HealthCheckCount { get; set; }
    }

    public class PageParameters
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}