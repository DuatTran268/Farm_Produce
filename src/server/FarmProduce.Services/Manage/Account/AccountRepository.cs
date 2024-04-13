using FarmProduce.Core.Contracts;
using FarmProduce.Core.DTO;
using FarmProduce.Core.Entities;
using FarmProduce.Data.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static FarmProduce.Core.DTO.ServiceResponses;

namespace FarmProduce.Services.Manage.Account
{
    public class AccountRepository(UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager, IConfiguration config) : IUserAccount
    {

        public async Task<GeneralResponse> CreateAccount(RegisterDTO userDTO)
        {
            if (userDTO is null) return new GeneralResponse(false, "Model is empty");
            var newUser = new ApplicationUser()
            {

                Name = userDTO.Name,
                Email = userDTO.Email,
                PasswordHash = userDTO.Password,
                UserName = userDTO.Email
            };
            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user is not null) return new GeneralResponse(false, "User registered already");

            var createUser = await userManager.CreateAsync(newUser!, userDTO.Password);
            if (!createUser.Succeeded) return new GeneralResponse(false, "Error occured.. please try again");

            //Assign Default Role : Admin to first registrar; rest is user
           
            //if (checkAdmin is null)
            //{
            //    await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
            //    await userManager.AddToRoleAsync(newUser, "Admin");
            //    return new GeneralResponse(true, "Account Created");
            //}
        
                var checkUser = await roleManager.FindByNameAsync("User");
                if (checkUser is null)
                    await roleManager.CreateAsync(new IdentityRole() { Name = "User" });

                await userManager.AddToRoleAsync(newUser, "User");
                return new GeneralResponse(true, "Account Created");
            
        }
        public async Task<GeneralResponse> CreateAccountByAdmin(UserDTO userDTO)
        {
            if (userDTO is null)
                return new GeneralResponse(false, "Model is empty");

            var newUser = new ApplicationUser()
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                PasswordHash = userDTO.Password,
                UserName = userDTO.Email
            };

            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user != null)
                return new GeneralResponse(false, "User already registered");

            var createUser = await userManager.CreateAsync(newUser, userDTO.Password);
            if (!createUser.Succeeded)
                return new GeneralResponse(false, "Error occurred. Please try again");

            // Assign role based on user's choice
            if (!string.IsNullOrEmpty(userDTO.Role))
            {
                var existingRole = await roleManager.FindByNameAsync(userDTO.Role);
                if (existingRole == null)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = userDTO.Role });
                }
                await userManager.AddToRoleAsync(newUser, userDTO.Role);
            }
            else
            {
                // Default role if user doesn't specify
                var defaultRole = "User"; // Or any default role you prefer
                var existingRole = await roleManager.FindByNameAsync(defaultRole);
                if (existingRole == null)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = defaultRole });
                }
                await userManager.AddToRoleAsync(newUser, defaultRole);
            }

            return new GeneralResponse(true, "Account Created");
        }


        public async Task<LoginResponse> LoginAccount(LoginDTO loginDTO)
        {
            if (loginDTO == null)
                return new LoginResponse(false, null!, "Login container is empty");

            var getUser = await userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser is null)
                return new LoginResponse(false, null!, "User not found");

            bool checkUserPasswords = await userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!checkUserPasswords)
                return new LoginResponse(false, null!, "Invalid email/password");

            var getUserRole = await userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.Name, getUser.Email, getUserRole.First());
            string token = GenerateToken(userSession);
            return new LoginResponse(true, token!, "Login completed");
        }

        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<GeneralResponse> DeleteAccount(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return new GeneralResponse(false, "User not found");

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return new GeneralResponse(false, "Error occurred while deleting user");
            return new GeneralResponse(true, "Account deleted successfully");
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllAccounts()
        {
            return await userManager.Users.ToListAsync();
        }
        public async Task<IEnumerable<DetailUserDTO>> GetAllUser()
        {
            var usersWithOrders = await userManager.Users
                .Select(user => new DetailUserDTO
                {
                   Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Orders = user.Orders.Select(order => new OrderDTO
                    {
                        Id = order.Id,
                        TotalPrice = order.TotalPrice,
                        OrderItems= order.OrderItems,
                        PaymentMethods=order.PaymentMethods,
                        OrderStatusId= order.OrderStatusId,
                    }).ToList()
                })
                .ToListAsync();

            return usersWithOrders;
        }
        public async Task<IEnumerable<UserWithRolesDTO>> GetAllAccountsWithRoles()
        {
            var usersWithRoles = new List<UserWithRolesDTO>();

            foreach (var user in await userManager.Users.ToListAsync())
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var userWithRoles = new UserWithRolesDTO
                {
                    Name = user.Name,
                    Email = user.Email,
                    Roles = userRoles.ToList()
                };
                usersWithRoles.Add(userWithRoles);
            }
            return usersWithRoles;
        }
        public async Task<DetailUserDTO> GetUserWithOrdersById(string userId)
        {
            var user = await userManager.Users
                .Where(u => u.Id == userId)
                .Select(u => new DetailUserDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Orders = u.Orders.Select(order => new OrderDTO
                    {
                        Id = order.Id,
                        TotalPrice = order.TotalPrice,
                        OrderItems = order.OrderItems,
                        PaymentMethods = order.PaymentMethods,
                        OrderStatusId = order.OrderStatusId,
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
