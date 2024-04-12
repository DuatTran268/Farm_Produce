using Microsoft.AspNetCore.Mvc.Rendering;

namespace FarmProduct.WebApi.Models.OrderStatuses
{
	public class OderStatusFilterCombobox
	{
		public string Name { get; set; }
		public IEnumerable<SelectListItem> OrderStatusList { get; set; }
	}
}
