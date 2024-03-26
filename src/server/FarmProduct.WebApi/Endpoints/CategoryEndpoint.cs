using Carter;
using FarmProduce.Core.Collections;
using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using FarmProduce.Services.Manage.Admins;
using FarmProduce.Services.Manage.Categories;
using FarmProduce.Services.Manage.Units;
using FarmProduce.Services.Media;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.Admin;
using FarmProduct.WebApi.Models.Categories;
using FarmProduct.WebApi.Models.Products;
using FarmProduct.WebApi.Utilities;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using SlugGenerator;
using System.Net;

namespace FarmProduct.WebApi.Endpoints
{
	public class CategoryEndpoint:ICarterModule
	{
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var routeGroupBuilder = app.MapGroup(RouteAPI.Category);

            // get department not required
            routeGroupBuilder.MapGet("/getall", GetAllCategory)
                .WithName("GetAllCategory")
                .Produces<ApiResponse<PaginationResult<CategoriesDto>>>();

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

			routeGroupBuilder.MapGet("/product/slugCategory/{slug:regex(^[a-z0-9_-]+$)}", GetListProductBySlugCategory)
				.WithName("GetListProductBySlugCategory")
				.Produces<ApiResponse<PaginationResult<ProductsDto>>>();

			routeGroupBuilder.MapPost("/", AddOrUpdateCategory)
				.WithName("AddUpdateCategory")
				.Accepts<CategoriesEditModel>("multipart/form-data")
				.Produces(401)
				.Produces<ApiResponse<CategoryItem>>();

			routeGroupBuilder.MapDelete("/{id:int}", DeleteCategory)
				.WithName("DeleteCategory")
				.Produces(401)
				.Produces<ApiResponse<string>>();

		}
        private static async Task<IResult> GetAllCategory(
		ICategoriesRepo categoriesRepo,
		[AsParameters] PagingModel pagingModel
		)
		{
			var categories = await categoriesRepo.GetAllCategories(
				categories => categories.ProjectToType<CategoriesDto>(),pagingModel);
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


		// add or update
		private static async Task<IResult> AddOrUpdateCategory(HttpContext context,
			[FromServices] ICategoriesRepo categoriesRepo, IMapper mapper, [FromServices] IMediaManager media)
		{
			var model = await CategoriesEditModel.BindAsync(context);
			var slug = model.Name.GenerateSlug();
			if (await categoriesRepo.IsCategorySlugExistedAsync(model.Id, slug))
			{
				return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict,
					$"Slug '{slug}' existed"));
			}
			var categories = model.Id > 0
				? await categoriesRepo.GetCategoryByIdAsync(model.Id) : null;

			if (categories == null)
			{
				categories = new Category();
			}
			categories.Name = model.Name;
			//categories.UrlSlug = model.UrlSlug;
			categories.UrlSlug = model.Name.GenerateSlug();

			if (model.ImageFile?.Length > 0)
			{
				string hostname = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}/",
					uploadedPath = await media.SaveFileAsync(
						model.ImageFile.OpenReadStream(), model.ImageFile.FileName, model.ImageFile.ContentType);
				if (!string.IsNullOrWhiteSpace(uploadedPath))
				{
					categories.ImageUrl = uploadedPath;
				}
			}

			await categoriesRepo.AddOrUpdateAsync(categories);
			return Results.Ok(ApiResponse.Success(mapper.Map<CategoryItem>(categories), HttpStatusCode.Created));
		}

		private static async Task<IResult> DeleteCategory(
			int id, ICategoriesRepo categoriesRepo)
		{
			return await categoriesRepo.DeleteCategory(id)
			? Results.Ok(ApiResponse.Success("Category deleted ", HttpStatusCode.NoContent))
			: Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Not find category id = {id}"));
		}

	}
}
