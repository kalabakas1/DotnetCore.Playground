using System;

namespace Playground.Domain.Dtos
{
    public class HealthCheckConfigurationDto
    {
        public Guid Id { get; set; }
        public int Retries { get; set; }
        public int MillsBetweenRetries { get; set; }
    }
}