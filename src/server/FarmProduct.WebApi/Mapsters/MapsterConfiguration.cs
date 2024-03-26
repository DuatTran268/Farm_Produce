
using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using FarmProduct.WebApi.Models.Admin;
using FarmProduct.WebApi.Models.Carts;
using FarmProduct.WebApi.Models.Categories;
using FarmProduct.WebApi.Models.CustomUI;
using FarmProduct.WebApi.Models.Discounts;
using FarmProduct.WebApi.Models.Images;
using FarmProduct.WebApi.Models.Orders;
using FarmProduct.WebApi.Models.OrderStatuses;
using FarmProduct.WebApi.Models.PaymentsMethod;
using FarmProduct.WebApi.Models.Products;
using FarmProduct.WebApi.Models.Unit;
using Mapster;

namespace FarmProduct.WebApi.Mapsters
{
	public class MapsterConfiguration : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<Admin, AdminDto>();
			config.NewConfig<Category, CategoriesDto>();
			config.NewConfig<Category, CategoriesDetail>();
			//config.NewConfig<Category, CategoriesEditModel>();
			config.NewConfig<Product, ProductsDto>();
			config.NewConfig<Product, ProductDetails>();
			config.NewConfig<Unit, UnitDto>();
			config.NewConfig<Unit, UnitItem>()
				  .Map(dest => dest.Id, src => src.Id);



			config.NewConfig<Discount, DiscountDto>();
			config.NewConfig<OrderStatus, OrderStatusDto>();
			config.NewConfig<PaymentMethod, PaymentsMethodDto>();
			config.NewConfig<Order, OrderDto>();
			config.NewConfig<CustomUI, CustomUIDto>();
            config.NewConfig<Image, ImageDto>();
			config.NewConfig<Product, ProductEditModel>();
			config.NewConfig<Image, ImageEditModel>();

        }
    }

}
