using Carter;
using FarmProduce.Core.Collections;
using FarmProduce.Services.Manage.OrderStatuses;
using FarmProduce.Services.Manage.PaymentMethods;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.OrderStatuses;
using FarmProduct.WebApi.Models.PaymentsMethod;
using FarmProduct.WebApi.Models.Products;
using FarmProduct.WebApi.Utilities;
using Mapster;
using MapsterMapper;
using System.Net;

namespace FarmProduct.WebApi.Endpoints
{
	public class PaymentMethodEndpoint:ICarterModule
	{
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var routeGroupBuilder = app.MapGroup(RouteAPI.PaymentMethod);


            // get department not required
            routeGroupBuilder.MapGet("/getall", GetAllPaymentMethod)
                .WithName("GetAllPaymentMethod")
                .Produces<ApiResponse<PaginationResult<PaymentsMethodDto>>>();

            // get order status by id
            routeGroupBuilder.MapGet("/{id:int}", GetPaymentMethodByID)
                .WithName("GetPaymentMethodByID")
                .Produces<ApiResponse<PaymentsMethodDto>>();
        }
        private static async Task<IResult> GetAllPaymentMethod(
		IPaymentMethodRepo paymentMethodRepo
		)
		{
			var paymentMethod = await paymentMethodRepo.GetAllPaymentMethod(
				paymentMethod => paymentMethod.ProjectToType<PaymentsMethodDto>());
			return Results.Ok(ApiResponse.Success(paymentMethod));
		}

		// get payment method by id
		public static async Task<IResult> GetPaymentMethodByID(int id, IPaymentMethodRepo paymentMethodRepo, IMapper mapper)
		{
			var paymentMethods = await paymentMethodRepo.GetPaymentMethodById(id);
			return paymentMethods == null
				? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Not find id = {id}"))
				: Results.Ok(ApiResponse.Success(mapper.Map<PaymentsMethodDto>(paymentMethods)));
		}

       
    }
}
