using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CurrencyConverter.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// Verweise auf die 2.1 Bibliothek mit der Logik
// Unterordner (hier 'Classes') müssen extra angegeben werden!!!!!!!
using CurrencyController.Logic;
using CurrencyController.Logic.Classes;


namespace CurrencyConverter.Controllers
{
    [ApiController]
    [Route("/api")]
    
    public class CurrencyController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();
        readonly string RateUrl = "https://cddataexchange.blob.core.windows.net/data-exchange/htl-homework/ExchangeRates.csv";
        readonly string ProductUrl = "https://cddataexchange.blob.core.windows.net/data-exchange/htl-homework/Prices.csv";


        [HttpGet]
        [Route("products/{requestedProduct}/price")]
        public async Task<IActionResult> GetAllProducts([FromQuery] string requestedCurrency, string requestedProduct)
        {
            var parser = new DataParser();
            ConverterLogic logic = new ConverterLogic();

            string currencyData = await parser.GetFile(RateUrl, client);
            string productData = await parser.GetFile(ProductUrl, client);
            List<CurrencyRate> RateList = logic.CurrencyRateToList(currencyData, ',', 1);
            List<Product> ProductList = logic.ProductsToList(productData, ',', 1);

            Result price = logic.ConvertCurrency(RateList, ProductList, requestedCurrency, requestedProduct);

            var output = JsonConvert.SerializeObject(price);

            return Ok(output);
        }
    }
}
