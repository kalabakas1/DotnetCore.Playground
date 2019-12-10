using System;
using System.Collections.Generic;

namespace Playground.Domain.Dtos
{
    public class HealthCheckDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public int[] ValidResponses { get; set; }
        public Dictionary<string, string> Headers { get; set; }
    }
}