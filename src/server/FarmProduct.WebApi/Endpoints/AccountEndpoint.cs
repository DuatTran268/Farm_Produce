using FarmProduce.Core.Collections;
using FarmProduct.WebApi.Models.Admin;
using FarmProduct.WebApi.Models;
using FarmProduct.WebApi.Utilities;
using MapsterMapper;
using System.Net;
using Carter;
using FarmProduce.Core.Contracts;
using FarmProduce.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FarmProduct.WebApi.Endpoints
{
    public class AccountEndpoint:ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var routeGroupBuilder = app.MapGroup(RouteAPI.Account);
            // get department not required
            routeGroupBuilder.MapPost("/register", Register)
                .WithName("Register")
                .Produces<ApiResponse<UserDTO>>();
            routeGroupBuilder.MapPost("/login", Login)
               .WithName("Login")
               .Produces<ApiResponse<UserDTO>>();
            routeGroupBuilder.MapPost("/create-account", CreateAccountByAdmin)
              .WithName("CreateAccount")
              .Produces<ApiResponse<UserDTO>>();
            //routeGroupBuilder.MapGet("/", GetAll)
            //  .WithName("getAll")
            //  .Produces<ApiResponse<UserDTO>>();
            routeGroupBuilder.MapGet("/", GetAllUser)
             .WithName("GetAllAccount")
             .Produces<ApiResponse<DetailUserDTO>>();
            routeGroupBuilder.MapGet("/{id}", GetUserByEmail)
             .WithName("GetDetail")
             .Produces<ApiResponse<DetailUserDTO>>();
        }
     
        private static async Task<IResult> Register(
        [FromServices]IUserAccount userAccount,[FromBody] RegisterDTO userDTO
        )
        {
            try
            {
            var response = await userAccount.CreateAccount(userDTO);
            return Results.Ok(ApiResponse.Success(response));

            }catch(Exception ex)
            {
                return Results.NotFound(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
        }
        private static async Task<IResult> CreateAccountByAdmin(
               [FromServices] IUserAccount userAccount, [FromBody] UserDTO userDTO
               )
        {
            try
            {
            var response = await userAccount.CreateAccountByAdmin(userDTO);
            return Results.Ok(ApiResponse.Success(response));

            }catch(Exception ex)
            {
              return Results.NotFound(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
        }
                private static async Task<IResult> Login(
               [FromServices] IUserAccount userAccount, [FromBody] LoginDTO loginDTO
               )
                {
                    var response = await userAccount.LoginAccount(loginDTO);
                    return Results.Ok(response);

           
                }

        //[Authorize(Roles = "Admin")]
       // private static async Task<IResult> GetAll(
       //[FromServices] IUserAccount userAccount
       //)
       // {
       //     try
       //     {
       //     var response = await userAccount.GetAllAccountsWithRoles();
       //     return Results.Ok(ApiResponse.Success(response));

       //     }catch(Exception ex)
       //     {
       //         return Results.NotFound(ApiResponse.Fail(HttpStatusCode.NotFound, ex.Message));
       //     }
       // }
        private static async Task<IResult> GetAllUser(
     [FromServices] IUserAccount userAccount
     )
        {
            try
            {
                var response = await userAccount.GetAllUser();
                return Results.Ok(ApiResponse.Success(response));

            }
            catch (Exception ex)
            {
                return Results.NotFound(ApiResponse.Fail(HttpStatusCode.NotFound, ex.Message));
            }
        }
        private static async Task<IResult> GetUserByEmail(
         [FromServices] IUserAccount userAccount, string id
         )
        {
            try
            {
                var response = await userAccount.GetUserWithOrdersById(id);
                return Results.Ok(ApiResponse.Success(response));

            }
            catch (Exception ex)
            {
                return Results.NotFound(ApiResponse.Fail(HttpStatusCode.NotFound, ex.Message));
            }
        }
    }
}
