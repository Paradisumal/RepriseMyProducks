using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Producks.UnderCuttersFacade.Models;

namespace Producks.UnderCuttersFacade
{
    interface ICategory
    {
        Task<UCCategoryDto> GetCategory(int id);
        Task<List<UCCategoryDto>> GetCategories();
        Task<List<UCCategoryDto>> GetCategories(string subject);
    }
}
