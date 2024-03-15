using Carter;
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


	using (var scope = app.Services.CreateScope())
	{
		var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
		seeder.Initialize();
	}

	app.MapCarter();
	app.Run();
}
