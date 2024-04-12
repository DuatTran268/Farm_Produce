using Microsoft.AspNetCore.Mvc.Rendering;

namespace FarmProduct.WebApi.Models.Categories
{
	public class CategoryFilterCombobox
	{
		public string Name { get; set; }
		public IEnumerable<SelectListItem> CategoryList { get; set; }
	}
}
