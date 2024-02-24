using FarmProduce.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.Entities
{
	public class Order :IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string UrlSlug { get; set; }
		public DateTime DateOrder {  get; set; }
		public string Note { get; set; }
		public int PaymentOptionId {  get; set; }
		public IList<PaymentOption> PaymentOptions { get; set; }
		public IList<OrderStatus> OrderStatuses { get; set; }
		public Buyer Buyer { get; set; }
		public int BuyerId { get; set; }
		public IList<Products> Products { get; set; }

	

	}
}
