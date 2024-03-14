using FarmProduce.Core.Collections;
using FarmProduce.Core.DTO;
using FarmProduce.Services.Manage.PaymentMethods;
using FarmProduce.Services.Manage.Products;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.Categories;
using FarmProduct.WebApi.Models.Comments;
using FarmProduct.WebApi.Models.PaymentsMethod;
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

			// get product by id
			routeGroupBuilder.MapGet("/{id:int}", GetProductByID)
				.WithName("GetProductByID")
				.Produces<ApiResponse<ProductsDto>>();


			// get by slug
			routeGroupBuilder.MapGet("/slugProduct/{slug:regex(^[a-z0-9_-]+$)}", GetProductBySlug)
				.WithName("GetProductBySlug")
				.Produces<ApiResponse<ProductDetails>>();

			routeGroupBuilder.MapGet("limitNewest/{limit:int}", GetLimitProductNewest)
			   .WithName("GetLimitProductNewest")
			   .Produces<ApiResponse<IList<ProductDetails>>>();


			// using slug of user to get posts by user upload
			routeGroupBuilder.MapGet("/cmt/slugProduct/{slug:regex(^[a-z0-9_-]+$)}", GetCommentBySlugProduct)
				.WithName("GetCommentBySlugProduct")
				.Produces<ApiResponse<PaginationResult<CommentDto>>>();
			return app;

		}

		// get all
		private static async Task<IResult> GetAllProducts(IProductRepo productRepo)
		{
			var products = await productRepo.GetAllProducts(
				products => products.ProjectToType<ProductsDto>());
			return Results.Ok(ApiResponse.Success(products));
		}

		// get by id of category
		public static async Task<IResult> GetProductByID(int id, IProductRepo productRepo, IMapper mapper)
		{
			var products = await productRepo.GetProductById(id);
			return products == null
				? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Not find id = {id}"))
				: Results.Ok(ApiResponse.Success(mapper.Map<ProductsDto>(products)));
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


		// using slug of product to get all comment
		private static async Task<IResult> GetCommentBySlugProduct ([FromRoute] string slug,
		  [AsParameters] PagingModel pagingModel, IProductRepo productRepo)
		{
			var cmtQuery = new CommentQuery()
			{
				UrlSlug = slug
			};
			var cmtList = await productRepo.GetCommentWithPaged(
				cmtQuery,
				pagingModel,
				comment => comment.ProjectToType<CommentDto>());
			var paginationResult = new PaginationResult<CommentDto>(cmtList);
			return cmtList == null
				? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Ko tồn tại slug '{slug}'"))
		
				: Results.Ok(ApiResponse.Success(paginationResult));
		}


	}
}
