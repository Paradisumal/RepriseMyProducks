using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Producks.UnderCuttersFacade.Models;

namespace Producks.UnderCuttersFacade
{
    class Brand : IBrand
    {
        private HttpClient InitialiseClient()
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri("http://undercutters.azurewebsites.net"),
                Timeout = TimeSpan.FromSeconds(5)
            };
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");

            return client;
        }

        public async Task<UCBrandDto> GetBrand(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UCBrandDto>> GetBrands()
        {
            var client = InitialiseClient();

            List<UCBrandDto> brands;

            string uri = "api/Brand";
            var response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            brands = await response.Content.ReadAsAsync<List<UCBrandDto>>();

            return brands;
        }

        public async Task<List<UCBrandDto>> GetBrands(string subject)
        {
            throw new NotImplementedException();
        }
    }
}
