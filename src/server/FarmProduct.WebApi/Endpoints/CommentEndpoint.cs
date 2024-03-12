using FarmProduce.Core.Collections;
using FarmProduce.Services.Manage.Comments;
using FarmProduce.Services.Manage.Products;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.Comments;
using FarmProduct.WebApi.Models.Products;
using Mapster;

namespace FarmProduct.WebApi.Endpoints
{
	public static class CommentEndpoint
	{
		public static WebApplication CommentsEndpoint(this WebApplication app)
		{
			var routeGroupBuilder = app.MapGroup("/api/comments");

			// get all comment
			routeGroupBuilder.MapGet("/getall", GetAllComment)
			.WithName("GetAllComment")
			.Produces<ApiResponse<PaginationResult<CommentDto>>>();



			return app;

		}
		private static async Task<IResult> GetAllComment(ICommentRepo commentRepo)
		{
			var comments = await commentRepo.GetAllComments(
				comments => comments.ProjectToType<CommentDto>());
			return Results.Ok(ApiResponse.Success(comments));
		}

	}
}
