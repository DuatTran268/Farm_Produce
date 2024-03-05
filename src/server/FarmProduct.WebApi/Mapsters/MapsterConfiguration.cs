﻿
using FarmProduce.Core.Entities;
using FarmProduct.WebApi.Models.Admin;
using FarmProduct.WebApi.Models.Categories;
using Mapster;

namespace FarmProduct.WebApi.Mapsters
{
	public class MapsterConfiguration : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			// admin
			config.NewConfig<Admin, AdminDto>();
			
			// category
			config.NewConfig<Category, CategoriesDto>();
			config.NewConfig<Category, CategoriesDetail>();
			//config.NewConfig<Category, CategoriesEditModel>();


			// 

		}
	}

}
