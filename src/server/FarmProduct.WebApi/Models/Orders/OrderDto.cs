using FarmProduce.Core.Entities;

namespace FarmProduct.WebApi.Models.Orders
{
	public class OrderDto
	{
        public int Id { get; set; }
        public DateTime DateOrder { get; set; }
        public int TotalPrice { get; set; }
        public int OrderStatusId { get; set; }
        public int CustomerId { get; set; }
      
    }
}
