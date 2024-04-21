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
            routeGroupBuilder.MapGet("/order-item", GetAllOrderItemPageAsync)
           .WithName("GetAllOrderItem")
           .Produces<ApiResponse<DetailOrder>>();
            routeGroupBuilder.MapPost("/create-order", AddAsync)
               .WithName("Create Order ")
               .Accepts<OrderEditModel>("multipart/form-data")
                .Produces(401)
               .Produces<ApiResponse<DetailOrder>>();
            routeGroupBuilder.MapPost("/create-order-item", AddOrderItemAsync)
             .WithName("Create Order Item")
             .Accepts<OrderItemEditModel>("multipart/form-data")
              .Produces(401)
             .Produces<ApiResponse<OrderItemDTO>>();
        }
        private static async Task<IResult> GetAllPageAsync([FromServices] IOrderRepo orderRepo, [AsParameters] PagingModel pagingModel, CancellationToken cancellation = default)
        {
            var orders = await orderRepo.GetAllPageAsync(
                orders => orders.ProjectToType<DetailOrder>(), pagingModel, cancellation);
            var pagination = new PaginationResult<DetailOrder>(orders);

            return Results.Ok(ApiResponse.Success(pagination));
        }
        private static async Task<IResult> GetAllOrderItemPageAsync([FromServices] IOrderItemRepo orderRepo, CancellationToken cancellation = default)
        {
            var orders = await orderRepo.GetAllOrderItem(
                orders => orders.ProjectToType<OrderItem>(), cancellation);
          

            return Results.Ok(ApiResponse.Success(orders));
        }


        private static async Task<IResult> AddAsync(HttpContext context, [FromServices] IOrderRepo orderRepo, IOrderItemRepo orderItemRepo, IMapper mapper)
        {
            var model = await OrderEditModel.BindAsync(context);

            if (model == null)
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, "Invalid order data"));
            }


            // Tạo một đối tượng Order từ model
            var order = new Order
            {

                TotalPrice = model.TotalPrice,
                OrderStatusId = model.OrderStatusId,
                ApplicationUserId = model.ApplicationUserId,
                DiscountId = model.DiscountId,
                PaymentMethodId = model.PaymentMethodId
            };

            //// Thêm Order vào cơ sở dữ liệu
            //await orderRepo.AddOrUpdate(order);

            //// Lấy Id của Order vừa được thêm
            //var orderId = order.Id;

            //// Duyệt qua danh sách OrderItems từ model và thêm vào Order
            //foreach (var item in model.OrderItems)
            //{
            //    var orderItem = new OrderItem
            //    {
            //        ProductId = item.ProductId,
            //        Quantity = item.Quantity,
            //        Price = item.Price,
            //        OrderId = orderId,


            //    };

            //    // Nếu Id của OrderItem là 0, tạo mới OrderItem
            //    if (item.Id == 0)
            //    {
            //        // Gán OrderId cho OrderItem mới tạo
            //        orderItem.OrderId = orderId;

            //        // Thêm mới OrderItem vào cơ sở dữ liệu
            //        await orderItemRepo.AddOrUpdate(orderItem);
            //    }
            //    else
            //    {
            //        // Nếu Id của OrderItem khác 0, sử dụng Id hiện có
            //        orderItem.Id = item.Id;
            //        await orderItemRepo.AddOrUpdate(orderItem);
            //    }

            //    // Thêm OrderItem vào danh sách OrderItems của Order
            //    order.OrderItems.Add(orderItem);
            //}

            // Cập nhật Order trong cơ sở dữ liệu với danh sách OrderItems mới
            if (model.Id == 0)
            {
                order.DateOrder = DateTime.Now;
            }

            await orderRepo.AddOrUpdate(order);

            // Trả về kết quả thành công
            return Results.Ok(ApiResponse.Success(mapper.Map<DetailOrder>(order), model.Id > 0 ? HttpStatusCode.OK : HttpStatusCode.Created));

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
    }


}
