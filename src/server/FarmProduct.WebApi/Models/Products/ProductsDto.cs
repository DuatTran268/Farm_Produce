﻿using FarmProduce.Core.Entities;
using FarmProduct.WebApi.Models.Categories;
using FarmProduct.WebApi.Models.Unit;

namespace FarmProduct.WebApi.Models.Products
{
	public class ProductsDto
	{
		public int Id { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }
        public int QuantityAvailable { get; set; }
        public int CategoryId { get; set; }
		public int UnitId { get; set; }
		public decimal Price { get; set; }
        public decimal PriceVirtual { get; set; }
        public int ViewCount{ get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        public bool Status { get; set; }
        public IList<Image> Images { get; set; }
    }
}
