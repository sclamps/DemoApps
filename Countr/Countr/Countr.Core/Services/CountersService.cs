using System.Collections.Generic;
using System.Threading.Tasks;
using Countr.Core.Models;
using Countr.Core.Repositories;

namespace Countr.Core.Services
{
    public class CountersService : ICountersService
    {
        readonly ICounterRepository _repository;

        public CountersService (ICounterRepository repository)
        {
            _repository = repository;
        }

        public async Task<Counter> AddNewCounter (string name)
        {
            var counter = new Counter {Name = name};
            await _repository.Save (counter).ConfigureAwait (false);
            return counter;
        }

        public Task<List<Counter>> GetAllCounters ()
        {
            return  _repository.GetAll ();
        }

        public Task DeleteCounter (Counter counter)
        {
            return _repository.Delete (counter);
        }

        public Task IncrementCounter (Counter counter)
        {
            counter.Count += 1;
            return _repository.Save (counter);
        }
    }
}