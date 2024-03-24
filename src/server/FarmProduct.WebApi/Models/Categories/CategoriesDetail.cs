using FarmProduct.WebApi.Models.Products;

namespace FarmProduct.WebApi.Models.Categories
{
	public class CategoriesDetail
	{
        public int Id { get; set; }
		public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string ImageUrl { get; set; }
        public IList<ProductsDto> Products { get; set; }
    }
}
