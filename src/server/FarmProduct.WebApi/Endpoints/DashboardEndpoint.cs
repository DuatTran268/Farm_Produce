using Carter;
using FarmProduce.Core.Contracts;
using FarmProduce.Services.Manage.Categories;
using FarmProduce.Services.Manage.Orders;
using FarmProduce.Services.Manage.Products;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.Dashboard;
using FarmProduct.WebApi.Utilities;
using Microsoft.Identity.Client;

namespace FarmProduct.WebApi.Endpoints
{
	public class DashboardEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			var routeGroupBuilder = app.MapGroup(RouteAPI.Dashboard);

			routeGroupBuilder.MapGet("/", GetInfoDashboard)
				.WithName("GetInfoDashboard")
				.Produces<DashboardModel>();
		}

		private static async Task<IResult> GetInfoDashboard(
			ICategoriesRepo categoriesRepo,
			IProductRepo productRepo,
			IOrderRepo orderRepo,
			IUserAccount userAccount
			)
		{
			var result = new DashboardModel()
			{
				CountCategory = await categoriesRepo.CountTotalCategoryOfProduct(),
				CountProduct = await productRepo.CountTotalProduct(),
				CountOrder = await orderRepo.CountTotalOrder(),
				CountUser = await userAccount.CountTotalUserAccount()
			};

			return Results.Ok(ApiResponse.Success(result));
		}
	}
}
