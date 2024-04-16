using Carter;
using FarmProduce.Core.Collections;
using FarmProduce.Core.Entities;
using FarmProduce.Services.Manage.Discounts;
using FarmProduce.Services.Manage.Images;
using FarmProduce.Services.Manage.OrderStatuses;
using FarmProduce.Services.Manage.Products;
using FarmProduce.Services.Manage.Units;
using FarmProduce.Services.Media;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.Comments;
using FarmProduct.WebApi.Models.Discounts;
using FarmProduct.WebApi.Models.OrderStatuses;
using FarmProduct.WebApi.Models.Products;
using FarmProduct.WebApi.Models.Unit;
using FarmProduct.WebApi.Utilities;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System.Net;

namespace FarmProduct.WebApi.Endpoints
{
	public class OrderStatusEndpoint:ICarterModule
	{
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var routeGroupBuilder = app.MapGroup(RouteAPI.OrderStatus);
            routeGroupBuilder.MapGet("/getall", GetAllOrderStatus)
            .WithName("GetAllOrderStatus")
            .Produces<ApiResponse<PaginationResult<OrderStatusDto>>>();


            // get order status by id
            routeGroupBuilder.MapGet("/{id:int}", GetOrderStatusById)
                .WithName("GetOrderStatusById")
                .Produces<ApiResponse<OrderStatusDto>>();

			routeGroupBuilder.MapGet("/combobox", FilterComboboxOrderStautus)
				.WithName("FilterComboboxOrderStautus")
				.Produces<ApiResponse<OderStatusFilterCombobox>>();
             routeGroupBuilder.MapPost("/", AddAsync)
                .WithName("Add Order status")
                .Accepts<OrderStatusEditModel>("multipart/form-data")
                .Produces(401)
                .Produces<ApiResponse<OrderStatus>>();
		}
        private static async Task<IResult> GetAllOrderStatus(
		IOrderStatusRepo orderStatusRepo)
		{
			var oderstatus = await orderStatusRepo.GetAllOrderStatus(
				oderstatus => oderstatus.ProjectToType<OrderStatusDto>());
			return Results.Ok(ApiResponse.Success(oderstatus));
		}
		// get Order Status by id
		public static async Task<IResult> GetOrderStatusById(int id, IOrderStatusRepo orderStatusRepo, IMapper mapper)
		{
			var orderstatus = await orderStatusRepo.GetOrderStatusByID(id);
			return orderstatus == null
				? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Not find id = {id}"))
				: Results.Ok(ApiResponse.Success(mapper.Map<OrderStatusDto>(orderstatus)));
		}

		// combobox of order status
		private static async Task<IResult> FilterComboboxOrderStautus(
		IOrderStatusRepo statusRepo)
		{
			var model = new OderStatusFilterCombobox()
			{
				OrderStatusList = (await statusRepo.GetOrderStatusCombobox())
				.Select(t => new SelectListItem()
				{
					Text = t.StatusCode,
					Value = t.Id.ToString()
				})
			};
			return Results.Ok(ApiResponse.Success(model));
		}
        private static async Task<IResult> AddAsync(HttpContext context, [FromServices] IOrderStatusRepo orderStatusRepo,IMapper mapper ,[FromServices] IMediaManager mediaManager)
        {
            var model = await OrderStatusEditModel.BindAsync(context);

            if (model == null)
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, "Invalid order status data"));

            }
            var orderStatus = await orderStatusRepo.GetOrderStatusByID(model.Id);

            if (orderStatus == null)
            {
                    orderStatus = mapper.Map<OrderStatus>(model);
            }

            if (await orderStatusRepo.IsIdExisted(model.Id))
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict, $"Id da toi tai"));
            }

            orderStatus.StatusCode = model.StatusCode;
            orderStatus.OrderId = model.OrderId;
            orderStatus.Description = model.Description;
          
            try
            {
                await orderStatusRepo.AddOrUpdate(orderStatus);
                return Results.Ok(ApiResponse.Success(mapper.Map<OrderStatusDto>(orderStatus), model.Id > 0 ? HttpStatusCode.OK : HttpStatusCode.Created));
            }
            catch (Exception ex)
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
        }
    }
}
