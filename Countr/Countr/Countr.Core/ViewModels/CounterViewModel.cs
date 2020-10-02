using System.Threading.Tasks;
using Countr.Core.Models;
using Countr.Core.Services;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace Countr.Core.ViewModels
{
    public class CounterViewModel : MvxViewModel<Counter>
    {
        Counter _counter;
        ICountersService _service;
        IMvxNavigationService _navigationService;

        public CounterViewModel (ICountersService service, IMvxNavigationService navigationService)
        {
            _service = service;
            _navigationService = navigationService;
            
            IncrementCommand = new MvxAsyncCommand (IncrementCounter);
            CancelCommand = new MvxAsyncCommand (Cancel);
            DeleteCommand = new MvxAsyncCommand (DeleteCounter);
            SaveComand = new MvxAsyncCommand (Save);
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
        public IMvxAsyncCommand CancelCommand { get; }
        public IMvxAsyncCommand DeleteCommand { get; }
        public IMvxAsyncCommand SaveComand { get; }
        
        async Task IncrementCounter ()
        {
            await _service.IncrementCounter (_counter);
            RaisePropertyChanged (() => Count);
        }
        

        async Task DeleteCounter ()
        {
            await _service.DeleteCounter (_counter);
        }
        
        async Task Cancel ()
        {
            await _navigationService.Close (this);
        }

        async Task Save ()
        {
            await _service.AddNewCounter (_counter.Name);
            await _navigationService.Close (this);
        }
    }
}