using System;
using Playground.Domain.Models;

namespace Playground.Domain.Factories
{
    public class ReportFactory : IReportFactory
    {
        public Report Create(string name = null)
        {
            return new Report(name);
        }

        public Report Create(Type type)
        {
            return new Report(type.Name);
        }
    }
}