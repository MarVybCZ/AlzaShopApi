
using AlzaShopApi.Model;
using AlzaShopApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AlzaShopApi
{
	/// <summary>
	/// Program class.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// Adds Swagger configuration to the web application builder.
		/// </summary>
		/// <param name="builder">The web application builder.</param>
		/// <returns>The updated web application builder.</returns>
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

		/// <summary>
		/// Configures the database for the application.
		/// </summary>
		/// <param name="app">The application builder.</param>
		public static void ConfigureDB(IApplicationBuilder app)
		{
			using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetRequiredService<AlzaShopDbContext>();

				context.Database.Migrate();

				InitData.Initialize(context);
			}
		}

		/// <summary>
		/// The entry point of the application.
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
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

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			ConfigureDB(app);

			app.Run();
		}
	}
}
