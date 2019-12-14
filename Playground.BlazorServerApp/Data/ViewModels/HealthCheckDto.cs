using System;

namespace Playground.BlazorServerApp.Data.ViewDtos
{
    public class HealthCheckDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}