using System;
using Playground.Domain.Enums;

namespace Playground.Application.Queries
{
    public class HealthCheckViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public HealthChecks Alias { get; set; }
    }
}