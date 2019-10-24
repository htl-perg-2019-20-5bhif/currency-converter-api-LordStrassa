using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyConverter.Services
{
    public class DataParser
    {
        public async Task<string> GetFile(string url, HttpClient client)
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();

            /* First try instead of the EnsureSuccessStatusCode Methode
            if (response.IsSuccessStatusCode.Equals(false))
            {
                throw new ArgumentException("Failed to get the "File!);
            }
            */
        }
        public async Task<string> FileToString(string file)
        {
            string data;
            try
            {
                data = await System.IO.File.ReadAllTextAsync(file);
            }
            catch (FileNotFoundException ex)
            {
                throw new System.ArgumentException($"File [{ file }] does not exist! Error Message: " + ex.Message);
            }
            return "";
        }
    }
}
