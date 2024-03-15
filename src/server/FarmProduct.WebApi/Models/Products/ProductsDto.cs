using FarmProduce.Core.Entities;
using FarmProduct.WebApi.Models.Unit;

namespace FarmProduct.WebApi.Models.Products
{
	public class ProductsDto
	{
		public int Id { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public int QuantityAvailable { get; set; }
        public UnitDto Unit { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        public bool Status { get; set; }
    }
}
