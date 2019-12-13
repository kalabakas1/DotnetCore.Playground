using System;

namespace Playground.BlazorServerApp.Data.ViewDtos
{
    public class ConfigurationDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Name { get; set; }
        public int HealthCheckCount { get; set; }
    }
}