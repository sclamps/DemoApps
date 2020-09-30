using System;
using MvvmCross.Core.ViewModels;

namespace SquareRt.Core.ViewModels
{
    public class SquareRtViewModel: MvxViewModel
    {
        readonly ISquareRtCalculator _calculator;

        public SquareRtViewModel (ISquareRtCalculator calculator)
        {
            _calculator = calculator;
        }

        double _result;

        public double Result {
            get => _result;
            set => SetProperty (ref _result, value);
        }

        double _number;
        public double Number {
            get => _number;
            set {
                if (SetProperty (ref _number, value)) {
                    Result = _calculator.Calculate (_number);
                }
            }
        }
    }
}