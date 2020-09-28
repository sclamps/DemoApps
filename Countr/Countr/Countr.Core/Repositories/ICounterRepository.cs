using System.Collections.Generic;
using System.Threading.Tasks;
using Countr.Core.Models;

namespace Countr.Core.Repositories
{
    public interface ICounterRepository
    {
        Task Save (Counter counter);
        Task<List<Counter>> GetAll ();
        Task Delete (Counter counter);
    }
}