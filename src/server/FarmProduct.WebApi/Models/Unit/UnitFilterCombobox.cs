using Microsoft.AspNetCore.Mvc.Rendering;

namespace FarmProduct.WebApi.Models.Unit
{
	public class UnitFilterCombobox
	{
		public string Name { get; set; }
		public IEnumerable<SelectListItem> UnitList { get; set; }
	}
}
