using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Countr.Core.Models;
using Countr.Core.Services;
using MvvmCross.Core.ViewModels;

namespace Countr.Core.ViewModels
{
    public class CountersViewModel : MvxViewModel
    {
        ICountersService _service;

        public CountersViewModel (ICountersService service)
        {
            _service = service;
            
            Counters = new ObservableCollection<CounterViewModel> ();
        }

        public override async Task Initialize ()
        {
            await LoadCounters ();
        }

        public ObservableCollection<CounterViewModel> Counters { get; }

        public async Task LoadCounters ()
        {
            var counters = await _service.GetAllCounters ();
            foreach (var counter in counters) {
                var viewModel = new CounterViewModel ();
                viewModel.Prepare(counter);
                Counters.Add (viewModel);
            }
        }
    }
}