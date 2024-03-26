﻿using Carter;
using FarmProduce.Core.Collections;
using FarmProduce.Services.Manage.Categories;
using FarmProduce.Services.Manage.Comments;
using FarmProduce.Services.Manage.Discounts;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.Categories;
using FarmProduct.WebApi.Models.Comments;
using FarmProduct.WebApi.Models.Discounts;
using FarmProduct.WebApi.Utilities;
using Mapster;
using MapsterMapper;
using System.Net;

namespace FarmProduct.WebApi.Endpoints
{
	public  class DiscountEndpoints:ICarterModule
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
        }
        private static async Task<IResult> GetAllDiscount(
		IDiscountRepo discountRepo,
		[AsParameters] PagingModel pagingModel
		)
		{
			var discounts = await discountRepo.GetAllDiscount(
				discounts => discounts.ProjectToType<DiscountDto>(), pagingModel);
			return Results.Ok(ApiResponse.Success(discounts));
		}

		// get discount by id
		public static async Task<IResult> GetDiscountByID(int id, IDiscountRepo discountRepo, IMapper mapper)
		{
			var discounts = await discountRepo.GetDiscountByID(id);
			return discounts == null
				? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Not find id = {id}"))
				: Results.Ok(ApiResponse.Success(mapper.Map<DiscountDto>(discounts)));
		}

        
    }
}
