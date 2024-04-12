using Carter;
using FarmProduce.Core.Collections;
using FarmProduce.Core.Contracts;
using FarmProduce.Services.Manage.OrderStatuses;
using FarmProduce.Services.Manage.PaymentMethods;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.Categories;
using FarmProduct.WebApi.Models.OrderStatuses;
using FarmProduct.WebApi.Models.PaymentsMethod;
using FarmProduct.WebApi.Models.Products;
using FarmProduct.WebApi.Utilities;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace FarmProduct.WebApi.Endpoints
{
	public class PaymentMethodEndpoint : ICarterModule
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



			routeGroupBuilder.MapGet("/combobox", FilterComboboxPaymentMethod)
				.WithName("FilterComboboxPaymentMethod")
				.Produces<ApiResponse<PaymentMethodFilterCombobox>>();

		}
		private static async Task<IResult> GetAllPaymentMethod(
		IPaymentMethodRepo paymentMethodRepo,
		[AsParameters] PagingModel pagingModel
		)
		{
			var paymentMethod = await paymentMethodRepo.GetAllPaymentMethod(
				paymentMethod => paymentMethod.ProjectToType<PaymentsMethodDto>(), pagingModel);
			var pagination = new PaginationResult<PaymentsMethodDto>(paymentMethod);

			return Results.Ok(ApiResponse.Success(pagination));
		}

		// get payment method by id
		public static async Task<IResult> GetPaymentMethodByID(int id, IPaymentMethodRepo paymentMethodRepo, IMapper mapper)
		{
			var paymentMethods = await paymentMethodRepo.GetPaymentMethodById(id);
			return paymentMethods == null
				? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Not find id = {id}"))
				: Results.Ok(ApiResponse.Success(mapper.Map<PaymentsMethodDto>(paymentMethods)));
		}

		private static async Task<IResult> FilterComboboxPaymentMethod(IPaymentMethodRepo paymentMethodRepo)
		{
			var model = new PaymentMethodFilterCombobox()
			{
				PaymentMethodList = (await paymentMethodRepo.GetPaymentMethodComboobox())
				.Select(t => new SelectListItem()
				{
					Text = t.Name,
					Value = t.Id.ToString()
				})
			};
			return Results.Ok(ApiResponse.Success(model));
		}


	}
}
