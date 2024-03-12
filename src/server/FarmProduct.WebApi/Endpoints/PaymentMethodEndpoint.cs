using FarmProduce.Core.Collections;
using FarmProduce.Services.Manage.OrderStatuses;
using FarmProduce.Services.Manage.PaymentMethods;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.PaymentsMethod;
using FarmProduct.WebApi.Models.Products;
using Mapster;

namespace FarmProduct.WebApi.Endpoints
{
	public static class PaymentMethodEndpoint
	{
		public static WebApplication PaymentsMethodEndpoint(this WebApplication app)
		{

			var routeGroupBuilder = app.MapGroup("/api/paymentMethods");


			// get department not required
			routeGroupBuilder.MapGet("/getall", GetAllPaymentMethod)
				.WithName("GetAllPaymentMethod")
				.Produces<ApiResponse<PaginationResult<PaymentsMethodDto>>>();

			return app;
		}


		private static async Task<IResult> GetAllPaymentMethod(
		IPaymentMethodRepo paymentMethodRepo
		)
		{
			var paymentMethod = await paymentMethodRepo.GetAllPaymentMethod(
				paymentMethod => paymentMethod.ProjectToType<PaymentsMethodDto>());
			return Results.Ok(ApiResponse.Success(paymentMethod));
		}

	}
}
