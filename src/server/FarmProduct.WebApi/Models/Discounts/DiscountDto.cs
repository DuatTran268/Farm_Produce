namespace FarmProduct.WebApi.Models.Discounts
{
	public class DiscountDto
	{
		public int Id { get; set; }
		public decimal DiscountPrice { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Status { get; set; }
	}
}
