using FarmProduce.Core.Collections;
using FarmProduce.Services.Manage.Discounts;
using FarmProduce.Services.Manage.OrderStatuses;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.Discounts;
using FarmProduct.WebApi.Models.OrderStatuses;
using Mapster;
using Microsoft.AspNetCore.Routing;

namespace FarmProduct.WebApi.Endpoints
{
	public static class OrderStatusEndpoint
	{
		public static WebApplication OrderStatusesEndpoint(this WebApplication app)
		{
			var routeGroupBuilder = app.MapGroup("/api/oderstatus");




			routeGroupBuilder.MapGet("/getall", GetAllOrderStatus)
			.WithName("GetAllOrderStatus")
			.Produces<ApiResponse<PaginationResult<OrderStatusDto>>>();

			return app;
		}

		private static async Task<IResult> GetAllOrderStatus(
		IOrderStatusRepo orderStatusRepo
		)
		{
			var oderstatus = await orderStatusRepo.GetAllOrderStatus(
				oderstatus => oderstatus.ProjectToType<OrderStatusDto>());
			return Results.Ok(ApiResponse.Success(oderstatus));
		}

	}
}
