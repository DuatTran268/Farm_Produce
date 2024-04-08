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
            routeGroupBuilder.MapGet("/", GetAll)
              .WithName("getAll")
              .Produces<ApiResponse<UserDTO>>();
        }
        private static async Task<IResult> Register(
        [FromServices]IUserAccount userAccount,[FromBody] RegisterDTO userDTO
        )
        {
            var response = await userAccount.CreateAccount(userDTO);
            return Results.Ok(ApiResponse.Success(response));
        }
        private static async Task<IResult> CreateAccountByAdmin(
               [FromServices] IUserAccount userAccount, [FromBody] UserDTO userDTO
               )
        {
            var response = await userAccount.CreateAccountByAdmin(userDTO);
            return Results.Ok(ApiResponse.Success(response));
        }
        private static async Task<IResult> Login(
       [FromServices] IUserAccount userAccount, [FromBody] LoginDTO loginDTO
       )
        {
            var response = await userAccount.LoginAccount(loginDTO);
            return Results.Ok(ApiResponse.Success(response));
        }
        private static async Task<IResult> GetAll(
       [FromServices] IUserAccount userAccount
       )
        {
            var response = await userAccount.GetAllAccountsWithRoles();
            return Results.Ok(ApiResponse.Success(response));
        }
    }
}
