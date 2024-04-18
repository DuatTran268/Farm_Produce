using FarmProduce.Core.Entities;

namespace FarmProduct.WebApi.Models.Discounts
{
	public class DiscountEditModel
	{
		public decimal DiscountPrice { get; set; }
		public DateTime StartDate { get; set; } = DateTime.Now;
		public DateTime EndDate { get; set; }
		public string Status { get; set; }
		public int? OrderId { get; set; }

	}
}
