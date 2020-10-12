using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Producks.UnderCuttersFacade.Models;

namespace Producks.UnderCuttersFacade
{
    public class UCProduct : IProduct
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

        public async Task<UCProductDto> GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UCProductDto>> GetProducts()
        {
            throw new NotImplementedException();
        }

        public async Task<List<UCProductDto>> GetProducts(int categoryId, string categoryName, int brandId)
        {
            var client = InitialiseClient();

            List<UCProductDto> products;

            string uri = "api/Product?category_id=" + categoryId
                                + "&category_name=" + categoryName
                                     + "&brand_id=" + brandId
                                                    + "&min_price=" + 0
                                                    + "&max_price=" + 9999;
            var response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            products = await response.Content.ReadAsAsync<List<UCProductDto>>();

            return products;
        }
    }
}