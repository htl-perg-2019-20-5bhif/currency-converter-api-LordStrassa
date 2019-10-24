using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyController.Logic.Classes
{
   public  class Product
    {
        public string Description;
        public string Currency;
        public decimal Price;

        public Product(string description, string currency, decimal price)
        {
            Description = description;
            Currency = currency.ToUpper();
            Price = price;
        }
    }
}
