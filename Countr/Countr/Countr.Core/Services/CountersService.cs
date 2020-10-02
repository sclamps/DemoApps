using System.Collections.Generic;
using System.Threading.Tasks;
using Countr.Core.Models;
using Countr.Core.Repositories;
using MvvmCross.Plugins.Messenger;

namespace Countr.Core.Services
{
    public class CountersService : ICountersService
    {
        readonly ICounterRepository _repository;
        readonly IMvxMessenger _messenger;

        public CountersService (ICounterRepository repository, IMvxMessenger messenger)
        {
            _repository = repository;
            _messenger = messenger;
        }

        public async Task<Counter> AddNewCounter (string name)
        {
            var counter = new Counter {Name = name};
            await _repository.Save (counter).ConfigureAwait (false);
            _messenger.Publish (new CountersChangedMessage (this));
            return counter;
        }

        public Task<List<Counter>> GetAllCounters ()
        {
            return  _repository.GetAll ();
        }

        public async Task DeleteCounter (Counter counter)
        {
            await _repository.Delete (counter).ConfigureAwait (false);
            _messenger.Publish (new CountersChangedMessage (this));
        }

        public Task IncrementCounter (Counter counter)
        {
            counter.Count += 1;
            return _repository.Save (counter);
        }
    }
}