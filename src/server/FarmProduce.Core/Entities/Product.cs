using FarmProduce.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.Entities
{
	public class Product :IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string UrlSlug { get; set; }
		public int QuanlityAvailable { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; }
		public decimal Price { get; set; } = 0;

		public string Description { get; set; }
	
		public DateTime DateCreate { get; set; } = DateTime.Now; // lay ngay hom nay de tao san pham
		public DateTime DateUpdate { get; set; } = DateTime.Now;
		public bool Status { get; set; }
		public Unit Unit { get; set; }
		public int UnitId { get; set; }	
		public IList<Discount> Discounts { get; set; }
		public IList<Image> Images { get; set; }
		public IList<Comment> Comments { get; set; }
		public IList<OrderItem> OrderItems { get; set; }

	}
}
