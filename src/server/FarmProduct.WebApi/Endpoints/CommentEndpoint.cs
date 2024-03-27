using Carter;
using FarmProduce.Core.Collections;
using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using FarmProduce.Services.Manage.Categories;
using FarmProduce.Services.Manage.Comments;
using FarmProduce.Services.Manage.Products;
using FarmProduce.Services.Manage.Units;
using FarmProduct.WebApi.Filters;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.Admin;
using FarmProduct.WebApi.Models.Categories;
using FarmProduct.WebApi.Models.Comments;
using FarmProduct.WebApi.Models.Products;
using FarmProduct.WebApi.Models.Unit;
using FarmProduct.WebApi.Utilities;
using FluentValidation;
using Mapster;
using MapsterMapper;
using System.Net;

namespace FarmProduct.WebApi.Endpoints
{
	public class CommentEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			var routeGroupBuilder = app.MapGroup(RouteAPI.Comment);

			// get all comment
			routeGroupBuilder.MapGet("/getall", GetAllComment)
			.WithName("GetAllComment")
			.Produces<ApiResponse<PaginationResult<CommentDto>>>();


			// get commnet by id
			routeGroupBuilder.MapGet("/{id:int}", GetCommentByID)
				.WithName("GetCommentByID")
				.Produces<ApiResponse<CommentDto>>();

			routeGroupBuilder.MapPost("/", AddNewComment)
				.WithName("AddNewComment")
				.AddEndpointFilter<ValidatorFilter<CommentEditModel>>()
				.Produces(401)
				.Produces<ApiResponse<CommentItem>>();

			routeGroupBuilder.MapPut("/{id:int}", UpdateComment)
				.WithName("UpdateComment")
				.Produces(401)
				.Produces<ApiResponse<string>>();
		}
		// get all
		private static async Task<IResult> GetAllComment(ICommentRepo commentRepo, [AsParameters] PagingModel pagingModel)
		{
			var comments = await commentRepo.GetAllComments(
				comments => comments.ProjectToType<CommentDto>(), pagingModel);
			return Results.Ok(ApiResponse.Success(comments));
		}

		// get comment by id
		public static async Task<IResult> GetCommentByID(int id, ICommentRepo commentRepo, IMapper mapper)
		{
			var commnets = await commentRepo.GetCommnetByID(id);
			return commnets == null
				? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Not find id = {id}"))
				: Results.Ok(ApiResponse.Success(mapper.Map<CommentDto>(commnets)));
		}
		// add new commnet
		private static async Task<IResult> AddNewComment(CommentEditModel model,
			ICommentRepo commentRepo, IMapper mapper)
		{


			var comment = mapper.Map<Comment>(model);
			await commentRepo.AddOrUpdateComment(comment);

			return Results.Ok(ApiResponse.Success(
				mapper.Map<CommentItem>(comment), HttpStatusCode.Created));
		}

		private static async Task<IResult> UpdateComment(
		int id, CommentEditModel model,
		IValidator<CommentEditModel> validator,
		ICommentRepo commentRepo,
		IMapper mapper
		)
		{
			var validatorResult = await validator.ValidateAsync(model);
			if (!validatorResult.IsValid)
			{
				return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, validatorResult));
			}

			var comment = mapper.Map<Comment>(model);
			comment.Id = id;

			return await commentRepo.AddOrUpdateComment(comment)
				? Results.Ok(ApiResponse.Success("Comment update success", HttpStatusCode.NoContent))
				: Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"not find unit"));

		}

	}
}
