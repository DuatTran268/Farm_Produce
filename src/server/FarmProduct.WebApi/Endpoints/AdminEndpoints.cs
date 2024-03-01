using FarmProduce.Core.Collections;
using FarmProduce.Services.Manage.Admins;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.Admin;
using Mapster;

namespace FarmProduct.WebApi.Endpoints
{
	public static class AdminEndpoints 
	{

		public static WebApplication AdminEndpoint(
		this WebApplication app)
		{
			var routeGroupBuilder = app.MapGroup("/api/admins");


			// get department not required
			routeGroupBuilder.MapGet("/getall", GetAllAdmin)
				.WithName("GetAllDepartment")
				.Produces<ApiResponse<PaginationResult<AdminDto>>>();

			return app;
		}
		private static async Task<IResult> GetAllAdmin(
		IAdminRepo adminRepo
		)
		{
			var admin = await adminRepo.GetAllAdmin(
				admin => admin.ProjectToType<AdminDto>());
			return Results.Ok(ApiResponse.Success(admin));
		}


	}
}
