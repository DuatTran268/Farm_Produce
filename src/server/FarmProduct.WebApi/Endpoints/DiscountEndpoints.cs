using Carter;
using FarmProduce.Core.Collections;
using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using FarmProduce.Services.Manage.Categories;
using FarmProduce.Services.Manage.Comments;
using FarmProduce.Services.Manage.Discounts;
using FarmProduce.Services.Manage.Units;
using FarmProduct.WebApi.Filters;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.Categories;
using FarmProduct.WebApi.Models.Comments;
using FarmProduct.WebApi.Models.Discounts;
using FarmProduct.WebApi.Models.Unit;
using FarmProduct.WebApi.Utilities;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FarmProduct.WebApi.Endpoints
{
	public class DiscountEndpoints : ICarterModule
	{

		public void AddRoutes(IEndpointRouteBuilder app)
		{
			var routeGroupBuilder = app.MapGroup(RouteAPI.Discount);
			// get department not required
			routeGroupBuilder.MapGet("/getall", GetAllDiscount)
				.WithName("GetAllDiscount")
				.Produces<ApiResponse<PaginationResult<DiscountDto>>>();


			// get commnet by id
			routeGroupBuilder.MapGet("/{id:int}", GetDiscountByID)
				.WithName("GetDiscountByID")
				.Produces<ApiResponse<DiscountDto>>();
            routeGroupBuilder.MapGet("/{name}", GetDiscountByName)
                .WithName("GetDiscountByName")
                .Produces<ApiResponse<DiscountDto>>();


            routeGroupBuilder.MapPost("/", CreateNewDiscount)
				.WithName("CreateNewDiscount")
				.AddEndpointFilter<ValidatorFilter<DiscountEditModel>>()
				.Produces(401)
				.Produces<ApiResponse<DiscountItem>>();

			routeGroupBuilder.MapPut("/{id:int}", UpdateDiscount)
				.WithName("UpdateDiscount")
				.Produces(401)
				.Produces<ApiResponse<string>>();

			routeGroupBuilder.MapDelete("/{id:int}", DeleteVoucherDiscount)
				.WithName("DeleteVoucherDiscount")
				.Produces(401)
				.Produces<ApiResponse<string>>();

		}
		private static async Task<IResult> GetAllDiscount(
		IDiscountRepo discountRepo,
		[AsParameters] PagingModel pagingModel
		)
		{
			var discounts = await discountRepo.GetAllDiscount(
				discounts => discounts.ProjectToType<DiscountDto>(), pagingModel);
			var pagination = new PaginationResult<DiscountDto>(discounts);

			return Results.Ok(ApiResponse.Success(pagination));
		}

		// get discount by id
		public static async Task<IResult> GetDiscountByID(int id, IDiscountRepo discountRepo, IMapper mapper)
		{
			var discounts = await discountRepo.GetDiscountByID(id);
			return discounts == null
				? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Not find id = {id}"))
				: Results.Ok(ApiResponse.Success(mapper.Map<DiscountDto>(discounts)));
		}
        public static async Task<IResult> GetDiscountByName(string name, IDiscountRepo discountRepo, IMapper mapper)
        {
            var discounts = await discountRepo.GetDiscountByName(name);
            return discounts == null
                ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Not find name = {name}"))
                : Results.Ok(ApiResponse.Success(mapper.Map<DiscountDto>(discounts)));
        }

        // create new discount
        private static async Task<IResult> CreateNewDiscount(DiscountEditModel model,
			 [FromServices] IDiscountRepo discountRepo, IMapper mapper)
		{
			var discount = mapper.Map<Discount>(model);
			await discountRepo.AddOrUpdateDiscountAsync(discount);

			return Results.Ok(ApiResponse.Success(
				mapper.Map<DiscountItem>(discount), HttpStatusCode.Created));
		}


		private static async Task<IResult> UpdateDiscount(
			int id, DiscountEditModel model,
			IValidator<DiscountEditModel> validator,
			[FromServices] IDiscountRepo discountRepo,
			IMapper mapper)
		{
			var validatorResult = await validator.ValidateAsync(model);
			if (!validatorResult.IsValid)
			{
				return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, validatorResult));
			}

			var discount = mapper.Map<Discount>(model);
			discount.Id = id;

			return await discountRepo.AddOrUpdateDiscountAsync(discount)
				? Results.Ok(ApiResponse.Success("Update success", HttpStatusCode.NoContent))
				: Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"not find unit"));

		}

		private static async Task<IResult> DeleteVoucherDiscount(
			int id, IDiscountRepo discountRepo)
		{
			return await discountRepo.DeleteDiscount(id)
			? Results.Ok(ApiResponse.Success(" Deleted success ", HttpStatusCode.NoContent))
			: Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Not find id = {id}"));
		}


	}
}
