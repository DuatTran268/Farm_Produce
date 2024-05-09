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
using FarmProduce.Services.Manage.PaymentMethods;
using FarmProduct.WebApi.Models.OrderStatuses;
using FarmProduct.WebApi.Models.PaymentsMethod;
using MapsterMapper;
using System.Net;
using FarmProduct.WebApi.Models.Products;
using FarmProduce.Services.Manage.OrderItems;
using FarmProduce.Services.Manage.Discounts;
using System.Reflection.Metadata.Ecma335;
using FarmProduce.Core.Contracts;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FarmProduct.WebApi.Endpoints
{
    public class OrderEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {

            var routeGroupBuilder = app.MapGroup(RouteAPI.Order);

            routeGroupBuilder.MapGet("/", GetAllPageAsync)
                .WithName("GetAllOrder")
                .Produces<ApiResponse<PaginationResult<DetailOrder>>>();
            routeGroupBuilder.MapGet("/{id:int}", GetByIdAsync)
                .WithName("GetById")
                .Produces<ApiResponse<OrderDetailDTO>>();
            routeGroupBuilder.MapGet("/order-item", GetAllOrderItemPageAsync)
           .WithName("Ge tAll Order Item")
           .Produces<ApiResponse<DetailOrder>>();
            routeGroupBuilder.MapPut("/update-order", UpdateAsync)
                .Produces(401)
               .Produces<ApiResponse<OrderUpdateDTO>>();
            routeGroupBuilder.MapPost("/create-order-item", AddOrderItemAsync)
             .WithName("Create Order Item")
             .Accepts<OrderItemEditModel>("multipart/form-data")
              .Produces(401)
             .Produces<ApiResponse<OrderItemDTO>>();

			routeGroupBuilder.MapDelete("/{id:int}", DeleteOrder)
	            .WithName("DeleteOrder")
	            .Produces(401)
	            .Produces<ApiResponse<string>>();
		}
        private static async Task<IResult> GetAllPageAsync([FromServices] IOrderRepo orderRepo, [AsParameters] PagingModel pagingModel, CancellationToken cancellation = default)
        {
            var orders = await orderRepo.GetAllPageAsync(
                orders => orders.ProjectToType<DetailOrder>(), pagingModel, cancellation);
            var pagination = new PaginationResult<DetailOrder>(orders);

            return Results.Ok(ApiResponse.Success(pagination));
        }
        private static async Task<IResult> GetByIdAsync(int id,IOrderRepo orderRepo, CancellationToken cancellation)
        {
            var order = await orderRepo.GetOrderById(id, cancellation);
            return order == null ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Khong ton tai id {id}"))
                 : Results.Ok(ApiResponse.Success(order));
        }
        private static async Task<IResult> GetAllOrderItemPageAsync([FromServices] IOrderItemRepo orderRepo, CancellationToken cancellation = default)
        {
            var orders = await orderRepo.GetAllOrderItem(
                orders => orders.ProjectToType<OrderItem>(), cancellation);
          

            return Results.Ok(ApiResponse.Success(orders));
        }

      
        private static async Task<IResult> UpdateAsync([FromServices] IOrderRepo orderRepo,OrderUpdateDTO orderDetailDTO,IMapper mapper)
        {
            try
            {
                var response = await orderRepo.UpdateOrder(orderDetailDTO);
                return Results.Ok(ApiResponse.Success(response));

            }
            catch (Exception ex)
            {
                return Results.NotFound(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
        }
        private static async Task<IResult> AddOrderItemAsync(HttpContext context, [FromServices] IOrderItemRepo orderItemRepo, IMapper mapper)
        {
            var model = await OrderItemEditModel.BindAsync(context);

            if (model == null)
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, "Invalid order data"));
            }
            var order = new OrderItem
            {

                OrderId= model.OrderId,
                ProductId= model.ProductId,
                Quantity= model.Quantity,
                
            };
            await orderItemRepo.AddOrUpdate(order);
            return Results.Ok(ApiResponse.Success(mapper.Map<OrderItemDTO>(order), model.Id > 0 ? HttpStatusCode.OK : HttpStatusCode.Created));
        }

		private static async Task<IResult> DeleteOrder(
			int id, IOrderRepo orderRepo)
		{
			return await orderRepo.DeleteOrder(id)
			? Results.Ok(ApiResponse.Success(" Deleted success ", HttpStatusCode.NoContent))
			: Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Not find id = {id}"));
		}

	}


}
