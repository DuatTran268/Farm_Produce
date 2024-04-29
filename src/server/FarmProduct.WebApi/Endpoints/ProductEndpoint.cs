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

			routeGroupBuilder.MapGet("/slugidProduct/{slug:regex(^[a-z0-9_-]+$)}", GetIdAndSlugOfProductForComment)
				.WithName("GetIdAndSlugOfProductForComment")
				.Produces<ApiResponse<ProductIdSlug>>();


			routeGroupBuilder.MapGet("limitNewest/{limit:int}", GetLimitProductNewest)
			   .WithName("GetLimitProductNewest")
			   .Produces<ApiResponse<IList<ProductDetails>>>();


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
				.Produces<ApiResponse<ProductDTONoImage>>();


			routeGroupBuilder.MapGet("/combobox", FilterComboboxProduct)
				.WithName("FilterComboboxProduct")
				.Produces<ApiResponse<ProductFilterCombobox>>();

			routeGroupBuilder.MapPost("/viewCount/{slug:regex(^[a-z0-9_-]+$)}", IncreaseViewProducts)
				.WithName("IncreaseViewProducts")
				.Produces<ApiResponse<string>>();

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

		private static async Task<IResult> GetIdAndSlugOfProductForComment([FromRoute] string slug, IProductRepo productRepo, IMapper mapper)
		{
			var productIdSlugComment = await productRepo.GetIdSlugOfProductForComment(slug);

			return productIdSlugComment == null
				? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Not find slug = {slug}"))
				: Results.Ok(ApiResponse.Success(mapper.Map<ProductIdSlug>(productIdSlugComment)));
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

            try
            {
                // Thêm sản phẩm vào cơ sở dữ liệu nếu sản phẩm chưa tồn tại
                if (model.Id == 0)
                {
                    var product = mapper.Map<Product>(model);
                    product.UrlSlug = slug;
                    product.DateCreate = DateTime.Now;
                    product.DateUpdate = DateTime.Now;
					product.Images = null;
                    await productRepo.AddOrUpdateProduct(product);
                    model.Id = product.Id;
                }
                else
                {
                    // Kiểm tra xem sản phẩm có tồn tại không
                    var existingProduct = await productRepo.GetProductById(model.Id);
                    if (existingProduct == null)
                    {
                        return Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Product with ID {model.Id} not found"));
                    }
                }
                var validImages = model.Images.Where(image => image != null && image.Length > 0 && !string.IsNullOrEmpty(image.FileName)).ToList();
                foreach (var imageFile in validImages)
                {
                   

                    string uploadedPath = await mediaManager.SaveFileAsync(imageFile.OpenReadStream(), imageFile.FileName, imageFile.ContentType);

                    if (!string.IsNullOrWhiteSpace(uploadedPath))
                    {
                        var image = new Image
                        {
                            Name = imageFile.FileName,
                            UrlImage = uploadedPath,
                            ProductId = model.Id, // Sử dụng Id của sản phẩm để liên kết với hình ảnh
                            Caption = "Caption"
                        };
                            await imageRepo.AddOrUpdateImage(image);
                        
                    }
                }

                return Results.Ok(ApiResponse.Success(mapper.Map<ProductsDto>(model), model.Id > 0 ? HttpStatusCode.OK : HttpStatusCode.Created));
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

		// increase view product 
		private static async Task<IResult> IncreaseViewProducts(
			string slug,
			IProductRepo productRepo)
		{
			await productRepo.IncreaseViewCountAsync(slug);
			return Results.Ok(ApiResponse.Success($"Increase view {slug} success"));
		}

	}
}
