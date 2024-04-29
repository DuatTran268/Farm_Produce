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
    RoleManager<IdentityRole> roleManager, IConfiguration config, FarmDbContext dbContext) : IUserAccount
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
                new Claim(ClaimTypes.Role, user.Role),
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
        //public async Task<IEnumerable<DetailUserDTO>> GetAllUser()
        //{

        //    var usersWithOrders = await userManager.Users
        //        .Select(user => new DetailUserDTO
        //        {
        //            Id = user.Id,
        //            Name = user.Name,
        //            Email = user.Email,
        //            Address = user.Address,
        //            PhoneNumber = user.PhoneNumber,

        //            Orders = user.Orders.Select(order => new OrderDTO
        //            {
        //                Id = order.Id,
        //                TotalPrice = order.TotalPrice,
        //                OrderItems = order.OrderItems,
        //                PaymentMethodId = order.PaymentMethodId,
        //                OrderStatusId = order.OrderStatusId,
        //            }).ToList()
        //        })
        //        .ToListAsync();

        //    return usersWithOrders;
        //}


        public async Task<IEnumerable<DetailUserDTO>> GetAllUser()
        {
            var usersWithRolesAndOrders = new List<DetailUserDTO>();

            // Lấy tất cả người dùng
            var users = await userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var userOrders = await dbContext.Orders.Include(x => x.OrderItems).Include(x=>x.Discount).Where(order => order.ApplicationUserId == user.Id).ToListAsync();

                var userWithRolesAndOrders = new DetailUserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Address = user.Address,
                    PhoneNumber = user.PhoneNumber,
                    Roles = userRoles.ToList(),
                    Orders = userOrders.Select(order => new OrderDTO
                    {
                        Id = order.Id,
                        TotalPrice = order.TotalPrice,
                        DateOrder = order.DateOrder,
                        CodeNameDiscount =order.Discount.CodeName,
                        OrderItems = order.OrderItems.Select(item => new OrderItemDTO
                        {
                            Id = item.Id,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                        }).ToList(),
                        PaymentMethodId = order.PaymentMethodId,
                        OrderStatusId = order.OrderStatusId
                    }).ToList()
                };

                usersWithRolesAndOrders.Add(userWithRolesAndOrders);
            }

            return usersWithRolesAndOrders;
        }



        //public async Task<IEnumerable<Us>> GetAllAccountsWithRoles()
        //{
        //    var usersWithRoles = new List<UserWithRolesDTO>();

        //    foreach (var user in await userManager.Users.ToListAsync())
        //    {
        //        var userRoles = await userManager.GetRolesAsync(user);
        //        var userWithRoles = new UserWithRolesDTO
        //        {
        //            Name = user.Name,
        //            Email = user.Email,
        //            Roles = userRoles.ToList()
        //        };
        //        usersWithRoles.Add(userWithRoles);
        //    }
        //    return usersWithRoles;
        //}
        public async Task<DetailUserDTO> GetUserWithOrdersById(string userId)
        {
            var user = await userManager.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            var userRoles = await userManager.GetRolesAsync(user);

            var userOrders = await dbContext.Orders.Where(order => order.ApplicationUserId == userId).ToListAsync();

            var userWithOrders = new DetailUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Roles = userRoles.ToList(),
                Orders = userOrders.Select(order => new OrderDTO
                {
                    Id = order.Id,
                    TotalPrice = order.TotalPrice,
                    //OrderItems = order.OrderItems.Select(item => new OrderItemDTO
                    //{

                    //    Id = item.Id,
                    //    ProductId = item.ProductId,
                    //    Quantity = item.Quantity,
                    //    // Price = item.Price
                    //}).ToList(),
                    PaymentMethodId = order.PaymentMethodId,
                    OrderStatusId = order.OrderStatusId,
                }).ToList()
            };

            return userWithOrders;
        }

        public async Task<GeneralResponse> UpdateAccount(string userId, UserInfo detailUserDTO)
        {
            if (detailUserDTO == null)
                return new GeneralResponse(false, "Model is empty");

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return new GeneralResponse(false, "User not found");

            user.Name = detailUserDTO.Name;
            user.Email = detailUserDTO.Email;
            user.Address = detailUserDTO.Address;
            user.PhoneNumber = detailUserDTO.PhoneNumber;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return new GeneralResponse(false, "Error occurred while updating user");

            return new GeneralResponse(true, "Account updated successfully");
        }
        public async Task<GeneralResponse> UpdateUserAndOrders(string userId, UserWithOrderDTO detailUserDTO, List<OrderDTO> orderDTOs)
        {
            try
            {
                if (detailUserDTO == null || orderDTOs == null)
                {
                    return new GeneralResponse(false, "User information or order information is empty");
                }

                var user = await userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return new GeneralResponse(false, "User not found");
                }

                user.Name = detailUserDTO.Name;
                user.Email = detailUserDTO.Email;
                user.Address = detailUserDTO.Address;
                user.PhoneNumber = detailUserDTO.PhoneNumber;
                user.Orders = new List<Order>();

                foreach (var orderDTO in orderDTOs)
                {
                    if (orderDTO == null)
                    {
                        return new GeneralResponse(false, "Order information is empty");
                    }

                    Order order;
                    if (orderDTO.Id == 0)
                    {
                        order = new Order
                        {
                            TotalPrice = orderDTO.TotalPrice,
                            PaymentMethodId = orderDTO.PaymentMethodId==0 ? 2 :orderDTO.PaymentMethodId,
                            OrderStatusId = orderDTO.OrderStatusId==0 ? 1: orderDTO.OrderStatusId,
                            ApplicationUserId = userId,
                            DateOrder = DateTime.Now,
                            OrderItems = new List<OrderItem>()
                        };

                        dbContext.Orders.Add(order);
                    }
                    else
                    {
                        order = await dbContext.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == orderDTO.Id);

                        if (order == null)
                        {
                            return new GeneralResponse(false, $"Order with ID {orderDTO.Id} not found");
                        }

                        order.TotalPrice = orderDTO.TotalPrice;
                        order.PaymentMethodId = orderDTO.PaymentMethodId;
                        order.OrderStatusId = orderDTO.OrderStatusId;
                        order.DateOrder = DateTime.Now;
                    }

                    order.OrderItems.Clear();

                    foreach (var orderItemDTO in orderDTO.OrderItems)
                    {
                        var orderItem = new OrderItem
                        {
                            ProductId = orderItemDTO.ProductId,
                            Quantity = orderItemDTO.Quantity,
                            OrderId = orderDTO.Id
                        };
                        order.OrderItems.Add(orderItem);

                        var product = await dbContext.Products.FindAsync(orderItemDTO.ProductId);
                        if (product != null)
                        {
                            product.QuantityAvailable -= orderItemDTO.Quantity;
                        }
                    }

                    if (!string.IsNullOrEmpty(orderDTO.CodeNameDiscount) && orderDTO.CodeNameDiscount != "string")
                    {
                        var discount = await dbContext.Discounts.FirstOrDefaultAsync(d => d.CodeName == orderDTO.CodeNameDiscount);
                        if (discount != null)
                        {
                            order.Discount = discount;
                        }
                    }
                  
                        else
                        {
                            var discount = await dbContext.Discounts.FirstOrDefaultAsync(d => d.CodeName == "Discount0");
                            if (discount != null)
                            {
                                order.Discount = discount;
                            }
                        
                    }
                }

                await dbContext.SaveChangesAsync();

                return new GeneralResponse(true, "User information and orders updated successfully");
            }
            catch (Exception ex)
            {
                return new GeneralResponse(false, $"An error occurred: {ex.Message}");
            }
        }


    }
}
