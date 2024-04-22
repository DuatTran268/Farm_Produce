using FarmProduce.Core.Entities;
using FarmProduct.WebApi.Models.Carts;
using FarmProduct.WebApi.Models.Comments;
using FarmProduct.WebApi.Models.Discounts;
using FarmProduct.WebApi.Models.Images;
using FarmProduct.WebApi.Models.Unit;

namespace FarmProduct.WebApi.Models.Products
{
	public class ProductDetails
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string UrlSlug { get; set; }
		public int QuantityAvailable { get; set; }
		public UnitDto Unit { get; set; }
		public decimal Price { get; set; } = 0;
        public decimal PriceVirtual { get; set; }
        public string Description { get; set; }
        public int ViewCount { get; set; }
        public DateTime DateCreate { get; set; } = DateTime.Now; // lay ngay hom nay de tao san pham
		public DateTime DateUpdate { get; set; } = DateTime.Now;
		public bool Status { get; set; }
		public IList<DiscountDto> Discounts { get; set; }
		public IList<ImageDto> Images { get; set; }
		public IList<CommentDto> Comments { get; set; }
		public IList<CartDto> Carts { get; set; }
	}
}
