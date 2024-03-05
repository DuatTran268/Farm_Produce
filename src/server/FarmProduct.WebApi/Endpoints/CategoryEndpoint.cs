using FarmProduce.Core.Collections;
using FarmProduce.Services.Manage.Admins;
using FarmProduce.Services.Manage.Categories;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.Admin;
using FarmProduct.WebApi.Models.Categories;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

			// get by slug
			routeGroupBuilder.MapGet("/slugCategory/{slug:regex(^[a-z0-9_-]+$)}", GetCategoryBySlug)
				.WithName("GetCategoryBySlug")
				.Produces<ApiResponse<CategoriesDetail>>();

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


		// get by slug category
		private static async Task<IResult> GetCategoryBySlug([FromRoute] string slug, ICategoriesRepo categoriesRepo, IMapper mapper)
		{
			var category = await categoriesRepo.GetDetailCategoryBySlug(slug);
			return category == null 
				? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Not find slug = {slug}"))
				: Results.Ok(ApiResponse.Success(mapper.Map<CategoriesDetail>(category)));
		}

	}
}
