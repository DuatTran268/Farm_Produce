using FarmProduce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.DTO
{
	public class OrderStatusItem
	{
		public int Id { get; set; }
		public string StatusCode { get; set; }
		public DateTime StatusDate { get; set; }
		public string Description { get; set; }
		public int OrderId { get; set; }
		//public Order Order { get; set; }
	}
}
