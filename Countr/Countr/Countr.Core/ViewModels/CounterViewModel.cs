using System.Threading.Tasks;
using Countr.Core.Models;
using Countr.Core.Services;
using MvvmCross.Core.ViewModels;

namespace Countr.Core.ViewModels
{
    public class CounterViewModel : MvxViewModel<Counter>
    {
        Counter _counter;
        ICountersService _service;

        public CounterViewModel ( ICountersService service)
        {
            _service = service;
            IncrementCommand = new MvxAsyncCommand (IncrementCounter);
            DeleteCommand = new MvxAsyncCommand (DeleteCounter);
        }
        
        public override void Prepare (Counter counter)
        {
            _counter = counter;
        }

        public string Name {
            get => _counter.Name;
            set {
                if (Name == value) return;
                _counter.Name = value;
                RaisePropertyChanged ();
            }
        }

        public int Count => _counter.Count;
        
        public IMvxAsyncCommand IncrementCommand { get; }
        async Task IncrementCounter ()
        {
            await _service.IncrementCounter (_counter);
            RaisePropertyChanged (() => Count);
        }
        
        public IMvxAsyncCommand DeleteCommand { get; }

        async Task DeleteCounter ()
        {
            await _service.DeleteCounter (_counter);
        }
    }
}