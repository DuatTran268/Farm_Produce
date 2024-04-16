using Microsoft.AspNetCore.Mvc.Rendering;

namespace FarmProduct.WebApi.Models.Products
{
	public class ProductFilterCombobox
	{
		public string Name { get; set; }
		public IEnumerable<SelectListItem> ProductList { get; set; }
	}
}
