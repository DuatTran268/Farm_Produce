using Microsoft.AspNetCore.Mvc.Rendering;

namespace FarmProduct.WebApi.Models.PaymentsMethod
{
	public class PaymentMethodFilterCombobox
	{
		public string Name { get; set; }
		public IEnumerable<SelectListItem> PaymentMethodList { get; set; }
	}
}
