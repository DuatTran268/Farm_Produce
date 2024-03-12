using FarmProduce.Core.Entities;

namespace FarmProduct.WebApi.Models.OrderStatuses
{
	public class OrderStatusDto
	{
		public int Id { get; set; }

		public string StatusCode { get; set; }
		public DateTime StatusDate { get; set; }
		public string Description { get; set; }
		public Order Order { get; set; }

	}
}
