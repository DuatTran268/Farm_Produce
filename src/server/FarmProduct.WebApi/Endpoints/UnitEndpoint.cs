using FarmProduce.Core.Collections;
using FarmProduct.WebApi.Models.Categories;
using FarmProduct.WebApi.Models;
using FarmProduce.Services.Manage.Categories;
using Mapster;
using FarmProduce.Services.Manage.Units;
using FarmProduce.Services.Manage.Products;
using FarmProduct.WebApi.Models.Unit;
using Carter;
using FarmProduct.WebApi.Utilities;
using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using FarmProduct.WebApi.Models.Products;
using MapsterMapper;
using System.Net;
using SlugGenerator;
using FarmProduct.WebApi.Filters;
using FluentValidation;

namespace FarmProduct.WebApi.Endpoints
{
	public class UnitEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			var routeGroupBuilder = app.MapGroup(RouteAPI.Unit);

			routeGroupBuilder.MapGet("/getall", GetAllPageAsync)
				.WithName("GetAllUnit")
				.Produces<ApiResponse<PaginationResult<UnitDto>>>();

			routeGroupBuilder.MapGet("/pagination", GetUnitPagination)
				.WithName("GetUnitPagination")
				.Produces<ApiResponse<IList<UnitItem>>>();

			routeGroupBuilder.MapGet("/{id:int}", GetUnitById)
			.WithName("GetUnitById")
			.Produces<ApiResponse<UnitItem>>();

			routeGroupBuilder.MapPost("/", AddNewUnit)
				.WithName("AddNewUnit")
				.AddEndpointFilter<ValidatorFilter<UnitEditModel>>()
				.Produces(401)
				.Produces<ApiResponse<UnitItem>>();


			routeGroupBuilder.MapPut("/{id:int}", UpdateUnit)
			.WithName("UpdateUnit")
			.Produces(401)
			.Produces<ApiResponse<string>>();

			routeGroupBuilder.MapDelete("/{id:int}", DeleteUnit)
				.WithName("DeleteUnit")
				.Produces(401)
				.Produces<ApiResponse<string>>();

		}
		private static async Task<IResult> GetAllPageAsync(IUnitRepo unitRepo, [AsParameters] PagingModel pagingModel, CancellationToken cancellation = default)
		{
			var products = await unitRepo.GetAllPageAsync(
				products => products.ProjectToType<UnitDto>(), pagingModel, cancellation);
			return Results.Ok(ApiResponse.Success(products));
		}

		private static async Task<IResult> GetUnitPagination([AsParameters] UnitFilterModel model, IUnitRepo unitRepo)
		{
			var unitList = await unitRepo.GetPagedUnit(model, model.Name);

			var pagingnationResult = new PaginationResult<UnitItem>(unitList);
			return Results.Ok(ApiResponse.Success(pagingnationResult));

		}

		// get by id
		private static async Task<IResult> GetUnitById(
			int id, IUnitRepo unitRepo, IMapper mapper)
		{
			var unit = await unitRepo.GetUnitById(id);

			return unit == null
			? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Not find id = {id}"))
			: Results.Ok(ApiResponse.Success(mapper.Map<UnitItem>(unit)));
		}


		// add new unit
		private static async Task<IResult> AddNewUnit(UnitEditModel model,
			IUnitRepo unitRepo, IMapper mapper)
		{

			if (await unitRepo.IsUnitSlugExistedAsync(0, model.UrlSlug))
			{
				return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict,
				$"Slug '{model.UrlSlug}' existed "));
			}


			var unit = mapper.Map<Unit>(model);
			await unitRepo.AddOrUpdateUnitAsync(unit);

			return Results.Ok(ApiResponse.Success(
				mapper.Map<UnitItem>(unit), HttpStatusCode.Created));
		}


		private static async Task<IResult> UpdateUnit(
		int id, UnitEditModel model,
		IValidator<UnitEditModel> validator,
		IUnitRepo unitRepo,
		IMapper mapper
		)
		{
			var validatorResult = await validator.ValidateAsync(model);
			if (!validatorResult.IsValid)
			{
				return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, validatorResult));
			}

			if (await unitRepo.IsUnitSlugExistedAsync(0, model.UrlSlug))
			{
				return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict,
					$"Slug '{model.UrlSlug}' is existed "));
			}

			var unit = mapper.Map<Unit>(model);
			unit.Id = id;

			return await unitRepo.AddOrUpdateUnitAsync(unit)
				? Results.Ok(ApiResponse.Success("Unit update success", HttpStatusCode.NoContent))
				: Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"not find unit"));

		}

		private static async Task<IResult> DeleteUnit(
			int id, IUnitRepo unitRepo)
		{
			return await unitRepo.DeleteUnit(id)
			? Results.Ok(ApiResponse.Success("Unit deleted ", HttpStatusCode.NoContent))
			: Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Not find unit id = {id}"));
		}

	}

}
