using FarmProduce.Data.Seeders;
using FarmProduct.WebApi.Endpoints;
using FarmProduct.WebApi.Extensions;
using FarmProduct.WebApi.Mapsters;
using FarmProduct.WebApi.Validations;

var builder = WebApplication.CreateBuilder(args);
{
	builder.ConfigureCors()
			.ConfigureSevices()
			.ConfigureSwaggerOpenApi()
			.ConfigureMapster()
			.ConfigureFluentValdation();
}
var app = builder.Build();
{
	app.SetupRequestPipeline();
	app.AdminEndpoint();
	app.CategoriesEndpoint();
	app.ProductsEndpoint();
	app.CommentsEndpoint();
	app.DiscountsEndpoints();
	app.OrderStatusesEndpoint();
	app.PaymentsMethodEndpoint();



	using (var scope = app.Services.CreateScope())
	{
		var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
		seeder.Initialize();
	}


	app.Run();
}
