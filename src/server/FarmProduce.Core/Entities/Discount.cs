﻿using FarmProduce.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.Entities
{
    public class Discount: IEntity
    {
        public int Id { get; set; }
        public decimal DiscountPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CodeName { get; set; }
		public IList<Order> Orders { get; set; }

	}
}
