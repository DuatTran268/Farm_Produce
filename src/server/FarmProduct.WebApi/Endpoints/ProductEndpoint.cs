using FarmProduce.Core.Collections;
using FarmProduce.Services.Manage.Products;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.Categories;
using FarmProduct.WebApi.Models.Products;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FarmProduct.WebApi.Endpoints
{
	public static class ProductEndpoint
	{
		public static WebApplication ProductsEndpoint(this WebApplication app)
		{
			var routeGroupBuilder = app.MapGroup("/api/products");



			// get department not required
			routeGroupBuilder.MapGet("/getall", GetAllProducts)
				.WithName("GetAllProducts")
				.Produces<ApiResponse<PaginationResult<ProductsDto>>>();

			// get by slug
			routeGroupBuilder.MapGet("/slugProduct/{slug:regex(^[a-z0-9_-]+$)}", GetProductBySlug)
				.WithName("GetProductBySlug")
				.Produces<ApiResponse<ProductDetails>>();

			routeGroupBuilder.MapGet("limitNewest/{limit:int}", GetLimitProductNewest)
			   .WithName("GetLimitProductNewest")
			   .Produces<ApiResponse<IList<ProductDetails>>>();

			return app;

		}

		// get all
		private static async Task<IResult> GetAllProducts(IProductRepo productRepo)
		{
			var products = await productRepo.GetAllProducts(
				products => products.ProjectToType<ProductsDto>());
			return Results.Ok(ApiResponse.Success(products));
		}

		// get by slug
		private static async Task<IResult> GetProductBySlug([FromRoute] string slug, IProductRepo productRepo, IMapper mapper)
		{
			var product = await productRepo.GetDetailProductBySlug(slug);
			return product == null
				? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Not find slug = {slug}"))
				: Results.Ok(ApiResponse.Success(mapper.Map<ProductDetails>(product)));
		}

		// get limit product newest
		private static async Task<IResult> GetLimitProductNewest(int limit, IProductRepo productRepo, ILogger<IResult> logger)
		{
			var productId = await productRepo.GetLitmitProductNewest(limit, pd => pd.ProjectToType<ProductDetails>());
			return Results.Ok(ApiResponse.Success(productId));
		}

	}
}
