using System.Collections.Generic;
using System.Threading.Tasks;
using Countr.Core.Models;
using Countr.Core.Services;
using Countr.Core.ViewModels;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace Countr.Tests.ViewModels
{
    [TestFixture]
    public class CountersViewModelTests
    {
        CountersViewModel _subject;
        ICountersService _service;

        [SetUp]
        public void SetUp ()
        {
            _service = A.Fake<ICountersService> ();
            _subject = new CountersViewModel (_service);
        }

        [Test]
        public async Task LoadCounters_GetsCounters ()
        {
            var counters = new List<Counter> {
                new Counter {Name = "one"},
                new Counter {Name = "two"},
            };
            A.CallTo (() => _service.GetAllCounters ()).Returns (counters);

            await _subject.LoadCounters ();
            
            _subject.Counters[0].Name.ShouldBe ("one");
            _subject.Counters[1].Name.ShouldBe ("two");
        }
    }
}