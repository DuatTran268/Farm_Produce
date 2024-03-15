using Carter;
using FarmProduce.Core.Collections;
using FarmProduce.Services.Manage.Carts;
using FarmProduct.WebApi.Models.Carts;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Utilities;
using Mapster;
using FarmProduct.WebApi.Models.Images;
using FarmProduce.Services.Manage.Images;

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
        }
        private static async Task<IResult> GetAllPageAsync(IImageRepo imageRepo, [AsParameters] PagingModel pagingModel, CancellationToken cancellation = default)
        {
            var orders = await imageRepo.GetAllPageAsync(
                orders => orders.ProjectToType<ImageDto>(), pagingModel, cancellation);
            return Results.Ok(ApiResponse.Success(orders));
        }
    }
}
