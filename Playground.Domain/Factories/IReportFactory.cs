using System;
using Playground.Domain.Models;

namespace Playground.Domain.Factories
{
    public interface IReportFactory
    {
        Report Create(string name = null);
        Report Create(Type type);
    }
}