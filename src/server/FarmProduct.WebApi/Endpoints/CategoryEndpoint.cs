using FarmProduce.Core.Collections;
using FarmProduce.Services.Manage.Admins;
using FarmProduce.Services.Manage.Categories;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.Admin;
using FarmProduct.WebApi.Models.Categories;
using Mapster;

namespace FarmProduct.WebApi.Endpoints
{
	public static class CategoryEndpoint
	{
		public static WebApplication CategoriesEndpoint(
		this WebApplication app)
		{
			var routeGroupBuilder = app.MapGroup("/api/categories");

			// get department not required
			routeGroupBuilder.MapGet("/getall", GetAllCategory)
				.WithName("GetAllCategory")
				.Produces<ApiResponse<PaginationResult<CategoriesDto>>>();



			return app;
		}


		private static async Task<IResult> GetAllCategory(
		ICategoriesRepo categoriesRepo
		)
		{
			var categories = await categoriesRepo.GetAllCategories(
				categories => categories.ProjectToType<CategoriesDto>());
			return Results.Ok(ApiResponse.Success(categories));
		}
	}
}
