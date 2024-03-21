using Carter;
using FarmProduce.Core.Collections;
using FarmProduce.Core.Entities;
using FarmProduce.Services.Manage.Carts;
using FarmProduce.Services.Manage.Orders;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.Carts;
using FarmProduct.WebApi.Utilities;
using Mapster;

namespace FarmProduct.WebApi.Endpoints
{
    public class CartEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var routeGroupBuilder = app.MapGroup(RouteAPI.Cart);
            routeGroupBuilder.MapGet("/", GetAllPageAsync)
                .WithName("GetAllCart")
                .Produces<ApiResponse<PaginationResult<CartDto>>>();
        }
        private static async Task<IResult> GetAllPageAsync(ICartRepo cartRepo, [AsParameters] PagingModel pagingModel, CancellationToken cancellation = default)
        {
            var orders = await cartRepo.GetAllPageAsync(
                orders => orders.ProjectToType<CartDto>(), pagingModel, cancellation);
            return Results.Ok(ApiResponse.Success(orders));
        }
    }
}
