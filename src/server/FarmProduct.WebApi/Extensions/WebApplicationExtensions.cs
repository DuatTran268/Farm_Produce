using FarmProduce.Data.Contexts;
using FarmProduce.Data.Seeders;
using FarmProduce.Services.Manage.Admins;
using FarmProduce.Services.Manage.Categories;
using FarmProduce.Services.Manage.Comments;
using FarmProduce.Services.Manage.Products;
using FarmProduce.Services.Manage.Units;
using FarmProduce.Services.Timing;
using Microsoft.EntityFrameworkCore;

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
			builder.Services.AddSwaggerGen();
			return builder;
		}

		public static WebApplication SetupRequestPipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCors("FarmProductApp");
            return app;
        }

	}
}
