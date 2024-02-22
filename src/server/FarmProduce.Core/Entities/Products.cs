using FarmProduce.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmProduce.Core.Entities
{
	public class Products :IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string UrlSlug { get; set; }
		public string Image {  get; set; }
		public decimal Price { get; set; }	
		public string ShortDescription {  get; set; }
		public decimal PriceDiscount { get; set; }
		public DateTime DateCreate { get; set; }
		public DateTime DateUpdate { get; set; }
		public bool Status { get; set; }
		public string StatusDescription { get; set; }
		public IList<Comment>Comments { get; set; }
		public IList <CollectionImage> CollectionImages { get; set; }
		public Category Category { get; set; }
		public int CategoryId { get; set; }
		


	}
}
