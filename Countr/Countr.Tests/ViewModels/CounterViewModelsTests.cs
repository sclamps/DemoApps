using Countr.Core.Models;
using Countr.Core.ViewModels;
using NUnit.Framework;
using Shouldly;

namespace Countr.Tests.ViewModels
{
    [TestFixture]
    public class CounterViewModelsTests
    {
        CounterViewModel _subject;

        [SetUp]
        public void SetUp ()
        {
            _subject = new CounterViewModel ();
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