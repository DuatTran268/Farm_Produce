using FarmProduce.Core.Collections;
using FarmProduce.Services.Manage.Categories;
using FarmProduce.Services.Manage.Discounts;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.Categories;
using FarmProduct.WebApi.Models.Discounts;
using Mapster;

namespace FarmProduct.WebApi.Endpoints
{
	public static class DiscountEndpoints
	{
		public static WebApplication DiscountsEndpoints(this WebApplication app)
		{
			var routeGroupBuilder = app.MapGroup("/api/discount");
			// get department not required
			routeGroupBuilder.MapGet("/getall", GetAllDiscount)
				.WithName("GetAllDiscount")
				.Produces<ApiResponse<PaginationResult<DiscountDto>>>();

			return app;

		}


		private static async Task<IResult> GetAllDiscount(
		IDiscountRepo discountRepo
		)
		{
			var discounts = await discountRepo.GetAllDiscount(
				discounts => discounts.ProjectToType<DiscountDto>());
			return Results.Ok(ApiResponse.Success(discounts));
		}
	}
}
