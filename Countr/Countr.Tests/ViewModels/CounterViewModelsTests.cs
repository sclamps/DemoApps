using System.Collections.Generic;
using System.Threading.Tasks;
using Countr.Core.Models;
using Countr.Core.Services;
using Countr.Core.ViewModels;
using FakeItEasy;
using MvvmCross.Core.Navigation;
using NUnit.Framework;
using Shouldly;

namespace Countr.Tests.ViewModels
{
    [TestFixture]
    public class CounterViewModelsTests
    {
        CounterViewModel _subject;
        ICountersService _service;
        IMvxNavigationService _navigationService;

        [SetUp]
        public void SetUp ()
        {
            _service = A.Fake<ICountersService> ();
            _navigationService = A.Fake<IMvxNavigationService> ();
            _subject = new CounterViewModel (_service, _navigationService);
        }

        [Test]
        public async Task IncrementCounter ()
        {
            await _subject.IncrementCommand.ExecuteAsync ();

            A.CallTo (() => _service.IncrementCounter (A<Counter>.Ignored)).MustHaveHappenedOnceExactly ();
        }

        [Test]
        public void Name_ComesFromCounter ()
        {
            var counter = new Counter { Name = "test-counter" };
            
            _subject.Prepare (counter);
            
            _subject.Name.ShouldBe (counter.Name);
        }
        
        [Test]
        public void Count_ComesFromCounter ()
        {
            var counter = new Counter { Count = 3 };
            
            _subject.Prepare (counter);
            
            _subject.Count.ShouldBe (counter.Count);
        } 
    }
}