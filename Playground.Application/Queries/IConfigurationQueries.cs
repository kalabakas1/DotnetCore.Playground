using System;
using System.Collections.Generic;

namespace Playground.Application.Queries
{
    public interface IConfigurationQueries
    {
        ConfigurationView Get(Guid id);
        IEnumerable<ConfigurationView> GetPagedConfigurations(int pageNumber, int pageSize);
    }
}