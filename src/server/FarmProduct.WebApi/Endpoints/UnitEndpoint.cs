using FarmProduce.Core.Collections;
using FarmProduct.WebApi.Models.Categories;
using FarmProduct.WebApi.Models;
using FarmProduce.Services.Manage.Categories;
using Mapster;
using FarmProduce.Services.Manage.Units;
using FarmProduce.Services.Manage.Products;
using FarmProduct.WebApi.Models.Unit;

namespace FarmProduct.WebApi.Endpoints
{
    public static class UnitEndpoint
    {
        public static WebApplication MapUnitEndpoint(
        this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/units");

            routeGroupBuilder.MapGet("/getall", GetAllAsync)
                .WithName("GetAllUnit")
                .Produces<ApiResponse<PaginationResult<CategoriesDto>>>();
            return app;
        }
        private static async Task<IResult> GetAllAsync(IUnitRepo unitRepo,PagingModel pagingModel,CancellationToken cancellation=default)
        {
            var products = await unitRepo.GetAllPageAsync(
                products => products.ProjectToType<UnitDto>(),pagingModel, cancellation);
            return Results.Ok(ApiResponse.Success(products));
        }
    }

}
