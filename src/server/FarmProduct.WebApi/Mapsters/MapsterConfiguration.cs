
using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using FarmProduct.WebApi.Models.Admin;
using FarmProduct.WebApi.Models.Carts;
using FarmProduct.WebApi.Models.Categories;
using FarmProduct.WebApi.Models.Comments;
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
			config.NewConfig<Category, CategoriesDto>();
			config.NewConfig<Category, CategoriesDetail>();
			config.NewConfig<Category, CategoriesEditModel>();
			config.NewConfig<Product, ProductsDto>()
                .Map(dest => dest.QuanlityAvailable, src => src.QuanlityAvailable); ;
			config.NewConfig<Product, ProductDetails>();
			config.NewConfig<Unit, UnitDto>();
			config.NewConfig<Unit, UnitItem>()
				  .Map(dest => dest.Id, src => src.Id);

			config.NewConfig<Comment, CommentDto>();
			config.NewConfig<Comment, CommentItem>()
				.Map(dest => dest.Id, src => src.Id);
			config.NewConfig<CommentEditModel, Comment>();



			config.NewConfig<Discount, DiscountDto>();
			config.NewConfig<Discount, DiscountItem>();
			config.NewConfig<DiscountEditModel, Discount>();

			config.NewConfig<OrderStatus, OrderStatusDto>();
			config.NewConfig<PaymentMethod, PaymentsMethodDto>();
			config.NewConfig<Order, OrderDto>();
			config.NewConfig<CustomUI, CustomUIDto>();
            config.NewConfig<Image, ImageDto>();
			config.NewConfig<Product, ProductEditModel>();
			config.NewConfig<Image, ImageEditModel>();
			config.NewConfig<Order, OrderDto>();

        }
    }

}
