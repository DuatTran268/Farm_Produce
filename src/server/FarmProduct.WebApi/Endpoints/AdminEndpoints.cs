using Carter;
using FarmProduce.Core.Collections;
using FarmProduce.Services.Manage.Admins;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Models.Admin;
using FarmProduct.WebApi.Utilities;
using Mapster;
using MapsterMapper;
using System.Net;

namespace FarmProduct.WebApi.Endpoints
{
	public  class AdminEndpoints :ICarterModule
	{

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var routeGroupBuilder = app.MapGroup(RouteAPI.Admin);


            // get department not required
            routeGroupBuilder.MapGet("/getall", GetAllAdmin)
                .WithName("GetAllDepartment")
                .Produces<ApiResponse<PaginationResult<AdminDto>>>();

            routeGroupBuilder.MapGet("/{id:int}", GetInforAdminById)
                .WithName("GetInforAdminById")
                .Produces<ApiResponse<AdminDto>>();


        }
        private static async Task<IResult> GetAllAdmin(
		IAdminRepo adminRepo
		)
		{
			var admin = await adminRepo.GetAllAdmin(
				admin => admin.ProjectToType<AdminDto>());
			return Results.Ok(ApiResponse.Success(admin));
		}

		public static async Task<IResult> GetInforAdminById(int id, IAdminRepo adminRepo, IMapper mapper)
		{
			var admin = await adminRepo.GetAdminById(id);
			return admin == null 
				? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy admin có id {id}"))
				: Results.Ok(ApiResponse.Success(mapper.Map<AdminDto>(admin)));
		}
    }
}
