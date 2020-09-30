using Countr.Core.Models;
using MvvmCross.Core.ViewModels;

namespace Countr.Core.ViewModels
{
    public class CounterViewModel : MvxViewModel<Counter>
    {
        Counter _counter;
        
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
    }
}