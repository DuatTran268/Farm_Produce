using FarmProduce.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.Entities
{
	public class Comment : IEntity
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string UrlSlug { get; set; }
		public string Content { get; set; }
		public DateTime Created { get; set; } = DateTime.Now;
		public bool Status { get; set; }
		public Products Product { get; set; }
		public int ProductId { get; set; }

	}
}
