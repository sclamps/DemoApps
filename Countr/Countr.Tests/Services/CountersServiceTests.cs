using System.Collections.Generic;
using System.Threading.Tasks;
using Countr.Core.Models;
using Countr.Core.Repositories;
using Countr.Core.Services;
using FakeItEasy;
using MvvmCross.Plugins.Messenger;
using NUnit.Framework;
using Shouldly;

namespace Countr.Tests.Services
{
    [TestFixture]
    public class CountersServiceTests
    {
        ICountersService _subject;
        ICounterRepository _repo;
        IMvxMessenger _messenger;

        [SetUp]
        public void SetUp ()
        {
            _repo = A.Fake<ICounterRepository> ();
            _messenger = A.Fake<IMvxMessenger> ();
            _subject = new CountersService (_repo, _messenger);
        }

        [Test]
        public async Task IncrementCounter ()
        {
            var counter = new Counter { Count = 0 };
            await _subject.IncrementCounter (counter);
            
            counter.Count.ShouldBe (1);
            A.CallTo (() => _repo.Save (counter)).MustHaveHappenedOnceExactly ();
        }

        [Test]
        public async Task GetAllCounters ()
        {
            var counters = new List<Counter> {
                new Counter { Name = "counter1" },
                new Counter { Name = "counter2" },
            };
            A.CallTo (() => _repo.GetAll ()).Returns (counters);

            var result = await _subject.GetAllCounters ();
            result.ShouldBeSameAs (counters);
        }

        [Test]
        public async Task DeleteCounter ()
        {
            var counter = new Counter { Count = 0 };
            await _subject.IncrementCounter (counter);

            A.CallTo (() => _repo.Save (counter)).MustHaveHappenedOnceExactly ();

            await _subject.DeleteCounter (counter);

            A.CallTo (() => _repo.Delete (counter)).MustHaveHappenedOnceExactly ();
        }
        
        [Test]
        public async Task DeleteCounter_PublishedMessage ()
        {
            await _subject.DeleteCounter (new Counter ());

            A.CallTo (() => _messenger.Publish (A<CountersChangedMessage>.Ignored)).MustHaveHappenedOnceExactly ();
        }
    }
}