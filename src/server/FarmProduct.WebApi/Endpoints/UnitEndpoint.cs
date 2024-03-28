﻿using FarmProduce.Core.Collections;
using FarmProduct.WebApi.Models.Categories;
using FarmProduct.WebApi.Models;
using FarmProduce.Services.Manage.Categories;
using Mapster;
using FarmProduce.Services.Manage.Units;
using FarmProduce.Services.Manage.Products;
using FarmProduct.WebApi.Models.Unit;
using Carter;
using FarmProduct.WebApi.Utilities;

namespace FarmProduct.WebApi.Endpoints
{
    public class UnitEndpoint:ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var routeGroupBuilder = app.MapGroup(RouteAPI.Unit);

            routeGroupBuilder.MapGet("/getall", GetAllPageAsync)
                .WithName("GetAllUnit")
                .Produces<ApiResponse<PaginationResult<UnitDto>>>();
        }
        private static async Task<IResult> GetAllPageAsync(IUnitRepo unitRepo,[AsParameters]PagingModel pagingModel,CancellationToken cancellation=default)
        {
            var products = await unitRepo.GetAllPageAsync(
                products => products.ProjectToType<UnitDto>(),pagingModel, cancellation);
            return Results.Ok(ApiResponse.Success(products));
        }

       
    }

}