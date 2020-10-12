using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Producks.UnderCuttersFacade.Models
{
    public class UCProductDto
    {
        public int Id { get; set; }
        public string Ean { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool InStock { get; set; }
        [DataType(DataType.Date)]
        public string ExpectedRestock { get; set; }
    }
}