using Carter;
using FarmProduce.Core.Collections;
using FarmProduce.Core.Entities;
using FarmProduce.Services.Manage.CustomUIs;
using FarmProduce.Services.Manage.Orders;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.Categories;
using FarmProduct.WebApi.Models.CustomUI;
using FarmProduct.WebApi.Utilities;
using Mapster;

namespace FarmProduct.WebApi.Endpoints
{
    public class CustomUIEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {

            var routeGroupBuilder = app.MapGroup(RouteAPI.CustomUI);

            routeGroupBuilder.MapGet("/", GetAllPageAsync)
                .WithName("GetAllCustomUI")
                .Produces<ApiResponse<PaginationResult<CustomUI>>>();
        }
        private static async Task<IResult> GetAllPageAsync(ICustomUIRepo customUIRepo, [AsParameters] PagingModel pagingModel, CancellationToken cancellation = default)
        {
            var customs = await customUIRepo.GetAllPageAsync(
                customs => customs.ProjectToType<CustomUIDto>(), pagingModel, cancellation);
            var pagination = new PaginationResult<CustomUIDto>(customs);

            return Results.Ok(ApiResponse.Success(pagination));
        }

    }
}
