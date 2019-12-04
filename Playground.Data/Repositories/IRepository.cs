using System.Collections.Generic;

namespace Playground.Data.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();
    }
}