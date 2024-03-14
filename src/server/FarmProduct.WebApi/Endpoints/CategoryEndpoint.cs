using FarmProduce.Core.Collections;
using FarmProduce.Core.DTO;
using FarmProduce.Services.Manage.Admins;
using FarmProduce.Services.Manage.Categories;
using FarmProduce.Services.Manage.Products;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.Admin;
using FarmProduct.WebApi.Models.Categories;
using FarmProduct.WebApi.Models.Comments;
using FarmProduct.WebApi.Models.Products;
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

			// get category by id

			routeGroupBuilder.MapGet("/{id:int}", GetInforCategoryByID)
				.WithName("GetInforCategoryByID")
				.Produces<ApiResponse<CategoriesDto>>();


			// get by slug
			routeGroupBuilder.MapGet("/slugCategory/{slug:regex(^[a-z0-9_-]+$)}", GetCategoryBySlug)
				.WithName("GetCategoryBySlug")
				.Produces<ApiResponse<CategoriesDetail>>();

			routeGroupBuilder.MapGet("limit/{limit:int}", GetLimitProduct)
			   .WithName("GetLimitProduct")
			   .Produces<ApiResponse<IList<CategoriesDetail>>>();

			routeGroupBuilder.MapGet("limitNewest/{limit:int}", GetLimitCategoryNewest)
			   .WithName("GetLimitCategoryNewest")
			   .Produces<ApiResponse<IList<CategoriesDetail>>>();

			// get list products by slug of category


			routeGroupBuilder.MapGet("/product/slugCategory/{slug:regex(^[a-z0-9_-]+$)}", GetListProductBySlugCategory)
				.WithName("GetListProductBySlugCategory")
				.Produces<ApiResponse<PaginationResult<ProductsDto>>>();



			return app;
		}

		// get category by slug
		private static async Task<IResult> GetAllCategory(
		ICategoriesRepo categoriesRepo
		)
		{
			var categories = await categoriesRepo.GetAllCategories(
				categories => categories.ProjectToType<CategoriesDto>());
			return Results.Ok(ApiResponse.Success(categories));
		}

		// get category by id
		public static async Task<IResult> GetInforCategoryByID(int id, ICategoriesRepo categoriesRepo, IMapper mapper)
		{
			var categories = await categoriesRepo.GetCategoryById(id);
			return categories == null
				? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Not find id = {id}"))
				: Results.Ok(ApiResponse.Success(mapper.Map<AdminDto>(categories)));
		}

		// get by slug category
		private static async Task<IResult> GetCategoryBySlug([FromRoute] string slug, ICategoriesRepo categoriesRepo, IMapper mapper)
		{
			var category = await categoriesRepo.GetDetailCategoryBySlug(slug);
			return category == null
				? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Not find slug = {slug}"))
				: Results.Ok(ApiResponse.Success(mapper.Map<CategoriesDetail>(category)));
		}


		// get limit number category
		private static async Task<IResult> GetLimitProduct(int limit, ICategoriesRepo categoriesRepo, ILogger<IResult> logger)
		{
			var cateId = await categoriesRepo.GetNLimitCategory(limit, cate => cate.ProjectToType<CategoriesDetail>());
			return Results.Ok(ApiResponse.Success(cateId));
		}

		private static async Task<IResult> GetLimitCategoryNewest(int limit, ICategoriesRepo categoriesRepo, ILogger<IResult> logger)
		{
			var cateId = await categoriesRepo.GetLimitCategoryNewest(limit, cate => cate.ProjectToType<CategoriesDetail>());
			return Results.Ok(ApiResponse.Success(cateId));
		}

		private static async Task<IResult> GetListProductBySlugCategory([FromRoute] string slug, [AsParameters] PagingModel pagingModel, ICategoriesRepo categoriesRepo)
		{
			var productQuery = new ProductQuery()
			{
				UrlSlug = slug
			};
			var productList = await categoriesRepo.GetListProductsWithSlugOfCategory(
				productQuery, 
				pagingModel, 
				product => product.ProjectToType<ProductsDto>());
			var paginationResult = new PaginationResult<ProductsDto>(productList);
			return productList == null
				? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Not find slug = '{slug}'"))
				: Results.Ok(ApiResponse.Success(paginationResult));

		}
		

	}
}
