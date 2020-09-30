using System;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using SquareRt.Core;
using SquareRt.Core.ViewModels;

namespace SquareRt.Tests.ViewModels
{
    [TestFixture]
    public class SquareRtViewModelTests
    {
        SquareRtViewModel _subject;
        ISquareRtCalculator _calculator;

        [SetUp]
        public void SetUp()
        {
            _calculator = A.Fake<ISquareRtCalculator> ();
            _subject = new SquareRtViewModel(_calculator);
        }

        [TestCase (4)]
        [TestCase (0)]
        [TestCase (-1)]
        [TestCase (16)]
        public void SettingNumber_CalculatesResult (double number)
        {
            A.CallTo (() => _calculator.Calculate (number)).Returns (Math.Sqrt (number));
            
            _subject.Number = number;
            
            _subject.Result.ShouldBe (Math.Sqrt (number));
        }
        
    }
    
}