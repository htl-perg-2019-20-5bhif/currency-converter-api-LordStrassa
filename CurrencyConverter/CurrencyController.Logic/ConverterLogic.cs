using CurrencyController.Logic.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CurrencyController.Logic
{
    public class ConverterLogic
    {
        public Result ConvertCurrency(List<CurrencyRate> exchangeRates, List<Product> products, string targetCurrency, string productName)
        {
            targetCurrency = targetCurrency.ToUpper();
            Product product = GetProduct(productName, products);
            CurrencyRate exchangeRate = GetExchangeRate(product.Currency, exchangeRates);

            if (targetCurrency.Equals("EUR"))
            {
                return CurrencyToEuro(product, exchangeRate);  // Euro to USD
            }
            else
            {
                Result ResultValue = CurrencyToEuro(product, exchangeRate);   //  USD to Euro
                product.Currency = targetCurrency;
                product.Price = ResultValue.ResultValue;
                exchangeRate = GetExchangeRate(product.Currency, exchangeRates); // Errro
                return EuroToCurrency(product, exchangeRate);
                //Euro to CHF
            }

        }

        private Result EuroToCurrency(Product product, CurrencyRate exchangeRate)
        {
            if (product.Currency.Equals("EUR"))
            {
                return new Result(decimal.Round(product.Price, 2));
            }
            return new Result(decimal.Round(product.Price * exchangeRate.Rate, 2));
        }

        private Result CurrencyToEuro(Product product, CurrencyRate exchangeRate)
        {
            if (product.Currency.Equals("EUR"))
            {
                return new Result(decimal.Round(product.Price, 2));
            }
            return new Result(resultValue: decimal.Round(product.Price / exchangeRate.Rate, 2));

        }

        private Product GetProduct(string productName, List<Product> products)
        {
            foreach (var product in products)
            {
                if (product.Description.Equals(productName))
                {
                    return product;
                }
            }
            return products[0];
            throw new System.ArgumentException("No Products found");
        }

        private CurrencyRate GetExchangeRate(string targetCurrency, List<CurrencyRate> exchangeRates)
        {
            if (targetCurrency.Equals("EUR"))
            {
                return new CurrencyRate("EUR", (decimal)1.00);
            }
            foreach (var rate in exchangeRates)
            {
                if (rate.Currency.ToUpper().Equals(targetCurrency.ToUpper()))
                {
                    return rate;
                }
            }
            throw new System.ArgumentException("No ExchangeRates found");
        }

        public List<Product> ProductsToList(string dataString, char seperator, int ignoreLines)
        {
            var products = new List<Product>();
            string[] data = dataString
                .Replace("\r", string.Empty)
                .Split('\n');

            string[] line;
            decimal rate;
            for (int i = ignoreLines; i < data.Count(); i++)
            {
                line = data[i].Split(seperator);

                if (line.Count() == 3)
                {

                    rate = decimal.Parse(line[2], System.Globalization.CultureInfo.InvariantCulture);
                    products.Add(new Product(line[0], line[1], rate));
                }
            }

            return products;
        }

        public List<CurrencyRate> CurrencyRateToList(string dataString, char seperator, int ignoreLines)
        {
            var rates = new List<CurrencyRate>();
            string[] data = dataString
                .Replace("\r", string.Empty)
                .Split('\n');

            string[] line;
            decimal price;
            for (int i = ignoreLines; i < data.Count(); i++)
            {
                line = data[i].Split(seperator);

                if (line.Count() == 2)
                {
                    price = decimal.Parse(line[1], System.Globalization.CultureInfo.InvariantCulture);
                    rates.Add(new CurrencyRate(line[0], price));
                }
            }

            return rates;

        }
    }
}
