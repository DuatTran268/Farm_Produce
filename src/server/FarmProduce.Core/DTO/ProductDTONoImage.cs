using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.DTO
{
    public class ProductDTONoImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }
        public int QuantityAvailable { get; set; }
        public int CategoryId { get; set; }
        public UnitItem Unit { get; set; }
        public decimal Price { get; set; }
        public decimal PriceVirtual { get; set; }
        public int ViewCount { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        public bool Status { get; set; }
    }
}
