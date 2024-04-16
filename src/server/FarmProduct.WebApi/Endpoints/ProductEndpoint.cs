using Carter;
using FarmProduce.Core.Collections;
using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using FarmProduce.Services.Manage.Categories;
using FarmProduce.Services.Manage.Images;
using FarmProduce.Services.Manage.Products;
using FarmProduce.Services.Manage.Units;
using FarmProduce.Services.Media;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.Categories;
using FarmProduct.WebApi.Models.Comments;
using FarmProduct.WebApi.Models.Products;
using FarmProduct.WebApi.Utilities;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SlugGenerator;
using System.Net;

namespace FarmProduct.WebApi.Endpoints
{
	public class ProductEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			var routeGroupBuilder = app.MapGroup(RouteAPI.Product);



			// get department not required
			routeGroupBuilder.MapGet("/getall", GetAllProducts)
				.WithName("GetAllProducts")
				.Produces<ApiResponse<PaginationResult<ProductsDto>>>();

			//
			routeGroupBuilder.MapGet("/{id:int}", getProductById)
			.WithName("getProductById")
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
			routeGroupBuilder.MapDelete("/{id:int}", DeleteProduct)
						  .WithName("DeleteProduct")
						  .Produces(204)
						  .Produces(404);

			routeGroupBuilder.MapPost("/", AddProduct)
				.WithName("AddProduct")
				.Accepts<ProductEditModel>("multipart/form-data")
				.Produces(401)
				.Produces<ApiResponse<ProductsDto>>();


			routeGroupBuilder.MapGet("/combobox", FilterComboboxProduct)
				.WithName("FilterComboboxProduct")
				.Produces<ApiResponse<ProductFilterCombobox>>();

		}
		private static async Task<IResult> GetAllProducts(IProductRepo productRepo, [AsParameters] PagingModel pagingModel, [AsParameters] ProductQuery productQuery)
		{
			try
			{
				var products = await productRepo.GetAllProducts(
			products => products.ProjectToType<ProductsDto>(), productQuery, pagingModel);
				var paginationResult = new PaginationResult<ProductsDto>(products);
				return Results.Ok(ApiResponse.Success(paginationResult));
			}
			catch (Exception ex)
			{
				return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
			}
		}
		// get by id 
		private static async Task<IResult> getProductById(
			int id, IProductRepo productRepo, IMapper mapper)
		{
			var product = await productRepo.GetProductById(id);

			return product == null
			? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Not find id = {id}"))
			: Results.Ok(ApiResponse.Success(mapper.Map<ProductsDto>(product)));
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
		private static async Task<IResult> GetCommentBySlugProduct([FromRoute] string slug,
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
		private static async Task<IResult> AddProduct(HttpContext context, [FromServices] IProductRepo productRepo, [FromServices] IImageRepo imageRepo, IMapper mapper, ProductEditModel validator, [FromServices] IMediaManager mediaManager)
		{
			var model = await ProductEditModel.BindAsync(context);

			if (model == null)
			{
				return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, "Invalid product data"));
			}

			var slug = model.Name.GenerateSlug();

			if (string.IsNullOrEmpty(slug))
			{
				return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, "Invalid product name"));
			}

			var product = await productRepo.GetProductById(model.Id);

			if (product == null)
			{
				product = mapper.Map<Product>(model);
			}

			if (await productRepo.IsSlugProductExisted(model.Id, slug))
			{
				return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict, $"Slug '{slug}' đã được sử dụng"));
			}

			product.UrlSlug = slug;
			product.Name = model.Name;
			product.Description = model.Description;
			product.QuanlityAvailable = model.QuanlityAvailable;
			product.CategoryId = model.CategoryId;
			product.Price = model.Price;
			product.Status = model.Status;
			product.UnitId = model.UnitId;

			//foreach (var imageFile in model.Images)
			//{
			//    if (imageFile == null || imageFile.Length == 0)
			//    {
			//        continue;
			//    }

			//    string uploadedPath = await mediaManager.SaveFileAsync(imageFile.OpenReadStream(), imageFile.FileName, imageFile.ContentType);

			//    if (!string.IsNullOrWhiteSpace(uploadedPath))
			//    {
			//        var image = new Image
			//        {
			//            Name = imageFile.FileName,
			//            UrlImage = uploadedPath,
			//            ProductId = product.Id,
			//            Caption = "Caption"
			//        };
			//        product.Images.Add(image);  

			//        // Thêm ảnh mới vào bảng Images
			//        await imageRepo.AddOrUpdateImage(image);
			//    }
			//}

			if (model.Id == 0)
			{
				product.DateCreate = DateTime.Now;
			}
			product.DateUpdate = DateTime.Now;

			try
			{
				await productRepo.AddOrUpdateProduct(product);
				return Results.Ok(ApiResponse.Success(mapper.Map<ProductsDto>(product), model.Id > 0 ? HttpStatusCode.OK : HttpStatusCode.Created));
			}
			catch (Exception ex)
			{
				return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
			}
		}



		private static async Task<IResult> DeleteProduct(int id, IProductRepo productRepo)
		{
			var status = await productRepo.DeleteWithIDAsync(id);
			return Results.Ok(status ? ApiResponse.Success(HttpStatusCode.NoContent) : ApiResponse.Fail(HttpStatusCode.NotFound, $"không tìm thấy rau với mã {id}"));
		}

		private static async Task<IResult> FilterComboboxProduct(
		IProductRepo productRepo)
		{
			var model = new ProductFilterCombobox()
			{
				ProductList = (await productRepo.GetProductCombobox())
				.Select(t => new SelectListItem()
				{
					Text = t.Name,
					Value = t.Id.ToString()
				})
			};
			return Results.Ok(ApiResponse.Success(model));
		}

	}
}
