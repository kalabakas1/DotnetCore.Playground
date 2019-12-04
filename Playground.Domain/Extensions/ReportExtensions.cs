using System;
using Playground.Domain.Models;

namespace Playground.Domain.Extensions
{
    public static class ReportExtensions
    {
        public static Report WithName(this Report report, string name)
        {
            report.Name = name;
            return report;
        }

        public static Report WithMessage(this Report report, string message)
        {
            report.Message = message;
            return report;
        }

        public static Report WithException(this Report report, Exception exception)
        {
            report.Exception = exception;
            return report;
        }
    }
}