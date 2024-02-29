using FarmProduce.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FarmProduct.WebApi.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplicationBuilder ConfigureSevices(this WebApplicationBuilder builder)
        {

            builder.Services.AddMemoryCache();
            builder.Services.AddDbContext<FarmDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            return builder;
        }
        
    }
}
