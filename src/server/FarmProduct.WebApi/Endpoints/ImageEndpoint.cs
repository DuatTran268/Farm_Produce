using Carter;
using FarmProduce.Core.Collections;
using FarmProduct.WebApi.Models.Carts;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Utilities;
using Mapster;
using FarmProduct.WebApi.Models.Images;
using FarmProduce.Services.Manage.Images;
using FarmProduce.Core.Entities;
using FarmProduce.Services.Manage.Products;
using FarmProduct.WebApi.Models.Products;
using MapsterMapper;
using System.Net;
using SlugGenerator;
using FarmProduce.Services.Media;
using Microsoft.AspNetCore.Mvc;
using FarmProduct.WebApi.Models.Categories;
using Microsoft.Extensions.Hosting;
using FarmProduce.Core.DTO;
using FarmProduce.Services.Manage.Categories;

namespace FarmProduct.WebApi.Endpoints
{
    public class ImageEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {

            var routeGroupBuilder = app.MapGroup(RouteAPI.Images);

            routeGroupBuilder.MapGet("/", GetAllPageAsync)
                .WithName("GetAllImage")
                .Produces<ApiResponse<PaginationResult<ImageDto>>>();
            routeGroupBuilder.MapPost("/", AddImageAsync)
              .WithName("AddImage")
              .Accepts<ImageEditModel>("multipart/form-data")
              .Produces<ApiResponse<ImageDto>>();
<<<<<<< HEAD

			routeGroupBuilder.MapGet("/{id:int}", GetImageById)
				.WithName("GetImageById")
				.Produces<ApiResponse<ImageDto>>();


			routeGroupBuilder.MapDelete("/{id:int}", DeleteImage)
				.WithName("DeleteImage")
				.Produces(401)
				.Produces<ApiResponse<string>>();
		}
=======
            routeGroupBuilder.MapDelete("/{id:int}", DeleteAsync)
                            .WithName("DeleteImage")
                            .Produces(204)
                            .Produces(404);
        }
>>>>>>> 85fab6d3716a99a74ca168a9065e65f50aee94c7
        private static async Task<IResult> GetAllPageAsync(IImageRepo imageRepo, [AsParameters] PagingModel pagingModel, CancellationToken cancellation = default)
        {
            var images = await imageRepo.GetAllPageAsync(
                images => images.ProjectToType<ImageDto>(), pagingModel);
            var pagination = new PaginationResult<ImageDto>(images);

            return Results.Ok(ApiResponse.Success(pagination));
        }
        private static async Task<IResult> AddImageAsync(HttpContext context
     , [FromServices] IImageRepo imageRepo
     , IMapper mapper
     , [FromServices] IMediaManager mediaManager)
        {
            var model = await ImageEditModel.BindAsync(context);

            var image = model.Id > 0 ? await imageRepo.GetByIdAsync(model.Id) : null;
            if (image == null)
            {
                image = mapper.Map<Image>(model);
            }

            image.Name = model.Name;
            image.Caption = model.Caption;
            image.ProductId = model.ProductId;
            if (model.ImageFile?.Length > 0)
            {
                string hostname = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}/",
                    uploadedPath = await mediaManager.SaveFileAsync(
                        model.ImageFile.OpenReadStream(), model.ImageFile.FileName, model.ImageFile.ContentType);
                if (!string.IsNullOrWhiteSpace(uploadedPath))
                {
                    image.UrlImage = uploadedPath; // Gán đường dẫn lưu trữ hình ảnh trên máy chủ cho UrlImage
                }
            }
            await imageRepo.AddOrUpdateImage(image);

            return Results.Ok(ApiResponse.Success(mapper.Map<ImageDto>(image), HttpStatusCode.Created));
        }
<<<<<<< HEAD



		// get image by id
		private static async Task<IResult> GetImageById(
		int id, IImageRepo imageRepo, IMapper mapper)
		{
			var image = await imageRepo.GetByIdAsync(id);
			return image == null
			? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Not find id = {id}"))
			: Results.Ok(ApiResponse.Success(mapper.Map<ImageDto>(image)));
		}

		private static async Task<IResult> DeleteImage(
			int id, IImageRepo imageRepo)
		{
			return await imageRepo.DeleteImage(id)
			? Results.Ok(ApiResponse.Success("Deleted ", HttpStatusCode.NoContent))
			: Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Not find id = {id}"));
		}
	}
=======
        private static async Task<IResult> DeleteAsync(int id, IImageRepo imageRepo)
        {
            var status = await imageRepo.DeleteWithIDAsync(id);
            return Results.Ok(status ? ApiResponse.Success(HttpStatusCode.NoContent) : ApiResponse.Fail(HttpStatusCode.NotFound, $"không tìm thấy rau với mã {id}"));
        }
    }
>>>>>>> 85fab6d3716a99a74ca168a9065e65f50aee94c7
}
