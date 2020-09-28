using System.Collections.Generic;
using System.Threading.Tasks;
using Countr.Core.Models;
using Countr.Core.Repositories;
using Countr.Core.Services;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace Countr.Tests.Services
{
    [TestFixture]
    public class CountersServiceTests
    {
        ICountersService subject;
        ICounterRepository repo;

        [SetUp]
        public void SetUp ()
        {
            repo = A.Fake<ICounterRepository> ();
            subject = new CountersService (repo);
        }

        [Test]
        public async Task IncrementCounter ()
        {
            var counter = new Counter { Count = 0 };
            await subject.IncrementCounter (counter);
            
            counter.Count.ShouldBe (1);
            A.CallTo (() => repo.Save (counter)).MustHaveHappenedOnceExactly ();
        }

        [Test]
        public async Task GetAllCounters ()
        {
            var counters = new List<Counter> {
                new Counter { Name = "counter1" },
                new Counter { Name = "counter2" },
            };
            A.CallTo (() => repo.GetAll ()).Returns (counters);

            var result = await subject.GetAllCounters ();
            result.ShouldBeSameAs (counters);
        }

        [Test]
        public async Task DeleteCounter ()
        {
            var counter = new Counter { Count = 0 };
            await subject.IncrementCounter (counter);

            A.CallTo (() => repo.Save (counter)).MustHaveHappenedOnceExactly ();

            await subject.DeleteCounter (counter);

            A.CallTo (() => repo.Delete (counter)).MustHaveHappenedOnceExactly ();
        }
    }
}