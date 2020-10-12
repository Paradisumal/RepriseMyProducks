using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Producks.UnderCuttersFacade.Models;

namespace Producks.UnderCuttersFacade
{
    public interface IBrand
    {
        Task<UCBrandDto> GetBrand(int id);
        Task<List<UCBrandDto>> GetBrands();
        Task<List<UCBrandDto>> GetBrands(string subject);
    }
}