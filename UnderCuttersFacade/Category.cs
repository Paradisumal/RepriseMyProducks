using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Producks.UnderCuttersFacade.Models;

namespace Producks.UnderCuttersFacade
{
    class Category : ICategory
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

        public async Task<List<UCCategoryDto>> GetCategories()
        {
            var client = InitialiseClient();

            List<UCCategoryDto> categories;

            string uri = "api/Category";
            var response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            categories = await response.Content.ReadAsAsync<List<UCCategoryDto>>();

            return categories;
        }

        public async Task<List<UCCategoryDto>> GetCategories(string subject)
        {
            throw new NotImplementedException();
        }

        public async Task<UCCategoryDto> GetCategory(int id)
        {
            throw new NotImplementedException();
        }
    }
}
