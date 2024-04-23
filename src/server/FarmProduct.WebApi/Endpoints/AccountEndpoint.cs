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
using FarmProduce.Core.Entities;
using Microsoft.AspNetCore.Identity;
using static FarmProduce.Core.DTO.ServiceResponses;
using System.Globalization;

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
            routeGroupBuilder.MapPut("/update-account", UpdateAccount)
           .WithName("Update Account")
           .Produces<ApiResponse<UserInfo>>();
            routeGroupBuilder.MapPost("/update-account/order", Update)
        .WithName("Update Account and orders")
        .Produces<ApiResponse<DetailUserDTO>>();
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
        [Authorize(Roles ="Admin")]
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
        private static async Task<IResult> UpdateAccount(
    [FromServices] UserManager<ApplicationUser> userManager,
    string userId,
    [FromBody] UserInfo detailUserDTO)
        {
            try
            {
                if (detailUserDTO == null)
                    return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, "Model is empty"));

                var user = await userManager.FindByIdAsync(userId);
                if (user == null)
                    return Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, "User not found"));

                user.Name = detailUserDTO.Name;
                user.Email = detailUserDTO.Email;
                user.Address = detailUserDTO.Address;
                user.PhoneNumber = detailUserDTO.PhoneNumber;

                var result = await userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    return Results.Ok(ApiResponse.Fail(HttpStatusCode.InternalServerError, "Error occurred while updating user"));

                return Results.Ok(ApiResponse.Success(new GeneralResponse(true, "Account updated successfully")));
            }
            catch (Exception ex)
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.InternalServerError, $"Error occurred: {ex.Message}"));
            }
        }
        private static async Task<IResult> Update(
       [FromServices] IUserAccount userAccount, [FromBody] UserWithOrderDTO data
       )
        {
            try
            {
                var response = await userAccount.UpdateUserAndOrders(data.Id,data, data.Orders);
                return Results.Ok(ApiResponse.Success(response));

            }
            catch (Exception ex)
            {
                return Results.NotFound(ApiResponse.Fail(HttpStatusCode.BadRequest, ex.Message));
            }
        }

    }
}
