﻿using FarmProduce.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.Entities
{
	public class Admin : IEntity
	{
		public int Id { get; set; }
		public string FullName { get; set; }	
		public string Password { get; set; }	
		public string Email { get; set; }
		public string UrlSlug { get; set; }
	}
}