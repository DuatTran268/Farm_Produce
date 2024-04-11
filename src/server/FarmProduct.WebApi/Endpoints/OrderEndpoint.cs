using FarmProduce.Core.Collections;
using FarmProduce.Services.Manage.Units;
using FarmProduct.WebApi.Models.Categories;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.Unit;
using Mapster;
using FarmProduct.WebApi.Models.Orders;
using FarmProduce.Services.Manage.Orders;
using Carter;
using Microsoft.AspNetCore.Mvc;
using FarmProduce.Core.Entities;
using FarmProduct.WebApi.Utilities;
using FarmProduce.Core.DTO;
using FarmProduce.Services.Manage.Comments;

namespace FarmProduct.WebApi.Endpoints
{
    public  class OrderEndpoint: ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {

            var routeGroupBuilder = app.MapGroup(RouteAPI.Order);

            routeGroupBuilder.MapGet("/", GetAllPageAsync)
                .WithName("GetAllOrder")
                .Produces<ApiResponse<PaginationResult<OrderDto>>>();
        }
        private static async Task<IResult> GetAllPageAsync([FromServices] IOrderRepo orderRepo, [AsParameters] PagingModel pagingModel, CancellationToken cancellation = default)
        {
            var orders = await orderRepo.GetAllPageAsync(
                orders => orders.ProjectToType<OrderDto>(), pagingModel, cancellation);
            var pagination = new PaginationResult<OrderDto>(orders);

            return Results.Ok(ApiResponse.Success(pagination));
        }


	}
}
