
using FarmProduce.Core.Entities;
using FarmProduct.WebApi.Models.Admin;
using FarmProduct.WebApi.Models.Categories;
using FarmProduct.WebApi.Models.Discounts;
using FarmProduct.WebApi.Models.OrderStatuses;
using FarmProduct.WebApi.Models.Products;
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
			
			// product
			config.NewConfig<Product, ProductsDto>();
			config.NewConfig<Product, ProductDetails>();


			// discount
			config.NewConfig<Discount, DiscountDto>();



			// order status
			config.NewConfig<OrderStatus, OrderStatusDto>();




			// 

		}
	}

}
