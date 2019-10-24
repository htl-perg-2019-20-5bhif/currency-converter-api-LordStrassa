using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyController.Logic.Classes
{
    public class CurrencyRate
    {
        public string Currency;
        public decimal Rate;

        public CurrencyRate(string currency, decimal rate)
        {
            Currency = currency;
            Rate = rate;
        }
    }
}
