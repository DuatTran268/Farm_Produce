
using FarmProduce.Core.Entities;
using FarmProduct.WebApi.Models.Admin;
using FarmProduct.WebApi.Models.Categories;
using FarmProduct.WebApi.Models.Products;
using FarmProduct.WebApi.Models.Unit;
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
			config.NewConfig<Category, CategoriesEditModel>();
			config.NewConfig<Product, ProductsDto>();
			config.NewConfig<Product, ProductDetails>();
			config.NewConfig<Unit, UnitDto>();

			// 

		}
	}

}
