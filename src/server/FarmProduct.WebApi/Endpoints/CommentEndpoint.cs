using Carter;
using FarmProduce.Core.Collections;
using FarmProduce.Services.Manage.Categories;
using FarmProduce.Services.Manage.Comments;
using FarmProduce.Services.Manage.Products;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.Admin;
using FarmProduct.WebApi.Models.Categories;
using FarmProduct.WebApi.Models.Comments;
using FarmProduct.WebApi.Models.Products;
using FarmProduct.WebApi.Utilities;
using Mapster;
using MapsterMapper;
using System.Net;

namespace FarmProduct.WebApi.Endpoints
{
	public class CommentEndpoint:ICarterModule
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
        }
        // get all
        private static async Task<IResult> GetAllComment(ICommentRepo commentRepo, [AsParameters] PagingModel pagingModel )
		{
			var comments = await commentRepo.GetAllComments(
				comments => comments.ProjectToType<CommentDto>(),pagingModel);
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

       
    }
}
