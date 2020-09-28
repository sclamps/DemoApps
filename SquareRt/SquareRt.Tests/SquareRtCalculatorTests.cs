using System;
using NUnit.Framework;
using Shouldly;
using SquareRt.Core;

namespace SquareRt.Tests
{
    [TestFixture]
    public class SquareRtCalculatorTests
    {
        ISquareRtCalculator _calc;

        [SetUp]
        public void SetUp()
        {
            _calc = new SquareRtCalculator();
        }

        [TestCase(0)]
        [TestCase(2)]
        [TestCase(-1)]
        public void Calculate(int num)
        {
            _calc.Calculate(num).ShouldBe(Math.Sqrt(num));
        }

    }
}