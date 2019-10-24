using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyController.Logic.Classes
{
    public class Result
    {
        public decimal ResultValue { get; set; }

        public Result(decimal resultValue)
        {
            ResultValue = resultValue;
        }
    }
}
