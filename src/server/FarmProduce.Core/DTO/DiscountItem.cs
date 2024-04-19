using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.DTO
{
	public class DiscountItem
	{
		public decimal DiscountPrice { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string CodeName { get; set; }

	}
}
