using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Producks.UnderCuttersFacade.Models;

namespace Producks.UnderCuttersFacade
{
    public interface IProduct
    {
        Task<UCProductDto> GetProduct(int id);
        Task<List<UCProductDto>> GetProducts();
        Task<List<UCProductDto>> GetProducts(int categoryId, string categoryName, int brandId);
    }
}