﻿using FarmProduce.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.Entities
{
	public class Buyer : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string UrlSlug { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public IList<Order> Orders { get; set; }
	}
}
