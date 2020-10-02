using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Countr.Core.Models;
using Countr.Core.Services;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace Countr.Core.ViewModels
{
    public class CountersViewModel : MvxViewModel
    {
        ICountersService _service;
        readonly MvxSubscriptionToken _token;
        readonly IMvxNavigationService _navigationService;

        public CountersViewModel (
            ICountersService service, 
            IMvxMessenger messenger,
            IMvxNavigationService navigationService)
        {
            _service = service;
            _token = messenger.SubscribeOnMainThread<CountersChangedMessage> (async m => await LoadCounters ());
            _navigationService = navigationService;
            
            Counters = new ObservableCollection<CounterViewModel> ();
            ShowAddNewCounterCommand = new MvxAsyncCommand (ShowAddNewCounter);
        }

        public override async Task Initialize ()
        {
            await LoadCounters ();
        }

        public ObservableCollection<CounterViewModel> Counters { get; }

        public async Task LoadCounters ()
        {
            Counters.Clear ();
            
            var counters = await _service.GetAllCounters ();
            foreach (var counter in counters) {
                var viewModel = new CounterViewModel (_service, _navigationService);
                viewModel.Prepare(counter);
                Counters.Add (viewModel);
            }
        }
        
        public IMvxAsyncCommand ShowAddNewCounterCommand { get; }

        async Task ShowAddNewCounter ()
        {
            await _navigationService.Navigate (typeof (CounterViewModel), new Counter ());
        }
    }
}