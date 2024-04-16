using FarmProduce.Core.Entities;

namespace FarmProduct.WebApi.Models.PaymentsMethod
{
	public class PaymentsMethodDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int OrderId { get; set; }
		//public Order Order { get; set; }
	}
}
