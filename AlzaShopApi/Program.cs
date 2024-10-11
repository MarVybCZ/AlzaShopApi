
using AlzaShopApi.Model;
using AlzaShopApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AlzaShopApi
{
	public class Program
	{
		private static WebApplicationBuilder AddSwagger(WebApplicationBuilder builder)
		{
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();

			builder.Services.AddSwaggerGen(c =>
			{
				// Get the path of the assembly file
				string assemblyPath = Assembly.GetExecutingAssembly().Location;

				// Get the creation date and time of the assembly file
				DateTime creationTime = File.GetCreationTime(assemblyPath);

				c.SwaggerDoc("v1", new OpenApiInfo { Title = "AlzaShopApi API", Version = "v1", Description = creationTime.ToString() });

				// Set the comments path for the Swagger JSON and UI.
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});

			return builder;
		}

		public static void ConfigureDB(IApplicationBuilder app)
		{
			// Ostatní konfigurace...

			// Získání scope pro pøístup k databázi
			using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetRequiredService<AlzaShopDbContext>();

				// Provádìní migrací (automatické vytvoøení DB)
				context.Database.Migrate();

				// Volání metody pro naplnìní výchozími daty
				InitData.Initialize(context);
			}

			// Ostatní konfigurace (Swagger, Routing, etc.)
		}

		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var configuration = new ConfigurationBuilder()
		   .AddJsonFile("appsettings.json", false, true)
		   .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
		   .AddEnvironmentVariables()
		   .Build();

			var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new NullReferenceException("No database defined");

			// Add services to the container.
			builder.Services.AddControllers();

			builder.Services.AddDbContext<AlzaShopDbContext>(options => options.UseSqlServer(connectionString));

			builder.Services.AddTransient<IProductService, ProductService>();

			builder = AddSwagger(builder);

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			ConfigureDB(app);

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
