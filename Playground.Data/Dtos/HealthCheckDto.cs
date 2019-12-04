using System.Collections.Generic;

namespace Playground.Data.Dtos
{
    public class HealthCheckDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public int[] ValidResponses { get; set; }
        public Dictionary<string, string> Headers { get; set; }
    }
}