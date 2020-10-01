using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Countr.Core.Models;
using Countr.Core.Repositories;
using Countr.Core.Services;
using Countr.Core.ViewModels;
using FakeItEasy;
using MvvmCross.Plugins.Messenger;
using NUnit.Framework;
using Shouldly;

namespace Countr.Tests.ViewModels
{
    [TestFixture]
    public class CountersViewModelTests
    {
        CountersViewModel _subject;
        ICountersService _service;
        IMvxMessenger _messenger;
        Action<CountersChangedMessage> _publishAction;

        [SetUp]
        public void SetUp ()
        {
            _service = A.Fake<ICountersService> ();
            _messenger = A.Fake<IMvxMessenger> ();
            _subject = new CountersViewModel (_service, _messenger );
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