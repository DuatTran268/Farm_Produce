using Carter;
using FarmProduce.Core.Contracts;
using FarmProduce.Core.Entities;
using FarmProduce.Data.Contexts;
using FarmProduce.Data.Seeders;
using FarmProduce.Services.Manage.Account;
using FarmProduce.Services.Manage.Admins;
using FarmProduce.Services.Manage.Categories;
using FarmProduce.Services.Manage.Comments;
using FarmProduce.Services.Manage.CustomUIs;
using FarmProduce.Services.Manage.Discounts;
using FarmProduce.Services.Manage.Images;
using FarmProduce.Services.Manage.Orders;
using FarmProduce.Services.Manage.OrderStatuses;
using FarmProduce.Services.Manage.PaymentMethods;
using FarmProduce.Services.Manage.Products;
using FarmProduce.Services.Manage.Units;
using FarmProduce.Services.Timing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace FarmProduct.WebApi.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplicationBuilder ConfigureSevices(this WebApplicationBuilder builder)
        {

            builder.Services.AddMemoryCache();
            builder.Services.AddDbContext<FarmDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
			builder.Services.AddScoped<IDataSeeder, DataSeeder>();
			builder.Services.AddScoped<ITimeProvider, LocalTimeProvider>();
            builder.Services.AddScoped<IUnitRepo,  UnitRepo>();
            // admin
			builder.Services.AddScoped<IAdminRepo, AdminRepo>();
            // category
            builder.Services.AddScoped<ICategoriesRepo, CategoriesRepo>();
           // product
            builder.Services.AddScoped<IProductRepo, ProductRepo>();
            // comment
            builder.Services.AddScoped<ICommentRepo, CommentRepo>();
            // discount
            builder.Services.AddScoped<IDiscountRepo, DiscountRepo>();
            // order
            builder.Services.AddScoped<IOrderStatusRepo, OrderStatusRepo>();
            // payment 
            builder.Services.AddScoped<IPaymentMethodRepo, PaymentMethodRepo>();
            //order
            builder.Services.AddScoped<IOrderRepo, OrderRepo>();
            //customUI
            builder.Services.AddScoped<ICustomUIRepo, CustomUIRepo>();
            builder.Services.AddScoped<IUserAccount, AccountRepository>();
            //cart
            builder.Services.AddScoped<IImageRepo, ImageRepo>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<FarmDbContext>()
                .AddSignInManager()
                .AddRoles<IdentityRole>();
            builder.Services.AddAuthorization();
            builder.Services.AddCarter();
			return builder;
        }

        public static WebApplicationBuilder ConfigureCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("FarmProductApp", policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            return builder;
        }

		public static WebApplicationBuilder ConfigureSwaggerOpenApi(
			this WebApplicationBuilder builder)
		{
			builder.Services.AddEndpointsApiExplorer();
			
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };
            });
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            return builder;
		}

		public static WebApplication SetupRequestPipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCors("FarmProductApp");


            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }

	}
}
