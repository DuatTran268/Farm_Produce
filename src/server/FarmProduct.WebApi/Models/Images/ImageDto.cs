﻿using FarmProduct.WebApi.Models.Products;

namespace FarmProduct.WebApi.Models.Images
{
	public class ImageDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string UrlImage { get; set; }
		public string Caption { get; set; }
		public int ProductId { get; set; }
		public ProductImage Products { get; set; }
	}
}
