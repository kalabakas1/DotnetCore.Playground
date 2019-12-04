using System;
using System.Collections.Generic;

namespace Playground.Domain.Models
{
    public class Report : IDisposable
    {
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public bool? Failed { get; set; }
        public Exception Exception { get; set; }

        public List<Report> Items { get; set; }

        public Report()
        {
        }

        public Report(string name)
        {
            Name = name;
            Start = DateTime.Now;
        }

        public Report Create(string name)
        {
            var report = new Report(name);
            Add(report);
            return report;
        }
        
        public Report Create()
        {
            var report = new Report();
            Add(report);
            return report;
        }
        
        private void Add(Report report)
        {
            if (Items == null)
            {
                Items = new List<Report>();
            }

            Items.Add(report);
        }

        public void Dispose()
        {
            End = DateTime.Now;
        }
    }
}