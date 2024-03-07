using FarmProduce.Core.Entities;

namespace FarmProduct.WebApi.Models.Products
{
	public class ProductDetails
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string UrlSlug { get; set; }
		public int QuanlityAvailable { get; set; }
		public string Unit { get; set; }
		public decimal Price { get; set; } = 0;
		public string Description { get; set; }
		public DateTime DateCreate { get; set; } = DateTime.Now; // lay ngay hom nay de tao san pham
		public DateTime DateUpdate { get; set; } = DateTime.Now;
		public bool Status { get; set; }
		public IList<Discount> Discounts { get; set; }
		public IList<Image> Images { get; set; }
		public IList<Comment> Comments { get; set; }
		public IList<Cart> Carts { get; set; }
	}
}
