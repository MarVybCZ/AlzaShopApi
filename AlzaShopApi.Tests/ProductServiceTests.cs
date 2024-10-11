using AlzaShopApi.Model;
using AlzaShopApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlzaShopApi.Tests
{
	[TestClass]
	public class ProductServiceTests
	{
		private DbContextOptions<AlzaShopDbContext> _options;

		[TestInitialize]
		public void Setup()
		{
			// Nastavení in-memory databáze pro testy
			_options = new DbContextOptionsBuilder<AlzaShopDbContext>()
				.UseInMemoryDatabase(databaseName: "InMemoryAlzaShopDb")
				.Options;

			// Naplnění databáze testovacími daty
			using (var context = new AlzaShopDbContext(_options))
			{
				context.Products.AddRange(
					new Product { Id = 1, Name = "Test Product 1", Price = 100, Description = "Description 1", ImgUri = "" },
					new Product { Id = 2, Name = "Test Product 2", Price = 200, Description = "Description 2", ImgUri = "" },
					new Product { Id = 3, Name = "Test Product 3", Price = 200, Description = "Description 3", ImgUri = "" },
					new Product { Id = 4, Name = "Test Product 4", Price = 200, Description = "Description 4", ImgUri = "" },
					new Product { Id = 5, Name = "Test Product 5", Price = 200, Description = "Description 5", ImgUri = "" },
					new Product { Id = 6, Name = "Test Product 6", Price = 200, Description = "Description 6", ImgUri = "" },
					new Product { Id = 7, Name = "Test Product 7", Price = 100, Description = "Description 7", ImgUri = "" },
					new Product { Id = 8, Name = "Test Product 8", Price = 200, Description = "Description 8", ImgUri = "" },
					new Product { Id = 9, Name = "Test Product 9", Price = 200, Description = "Description 9", ImgUri = "" },
					new Product { Id = 10, Name = "Test Product 10", Price = 200, Description = "Description 10", ImgUri = "" },
					new Product { Id = 11, Name = "Test Product 11", Price = 200, Description = "Description 11", ImgUri = "" },
					new Product { Id = 12, Name = "Test Product 12", Price = 200, Description = "Description 12", ImgUri = "" }
				);
				context.SaveChanges();
			}
		}

		[TestMethod]
		public async Task GetProducts_ShouldReturnAllProductsInTwoSteps()
		{
			// Použijeme in-memory databázi
			using (var context = new AlzaShopDbContext(_options))
			{
				var productService = new ProductService(context);

				// Act
				var products = await productService.GetProducts(1, 10);

				// Assert
				Assert.AreEqual(10, products.Count());
				Assert.IsTrue(products.Any(p => p.Name == "Test Product 1"));
				Assert.IsTrue(products.Any(p => p.Name == "Test Product 2"));

				// Act
				products = await productService.GetProducts(2, 10);

				// Assert
				Assert.AreEqual(2, products.Count());
				Assert.IsTrue(products.Any(p => p.Name == "Test Product 11"));
				Assert.IsTrue(products.Any(p => p.Name == "Test Product 12"));
			}
		}

		[TestMethod]
		public async Task GetProduct_ById()
		{
			using (var context = new AlzaShopDbContext(_options))
			{
				var productService = new ProductService(context);

				// Act
				var product1 = await productService.GetProduct(1);
				var product6 = await productService.GetProduct(6);

				// Assert				
				Assert.IsTrue(product1.Name == "Test Product 1");
				Assert.IsTrue(product6.Name == "Test Product 6");
			}
		}

		[TestMethod]
		public async Task UpdateProductDescription()
		{
			using (var context = new AlzaShopDbContext(_options))
			{
				var productService = new ProductService(context);

				var product = await productService.GetProduct(1);

				product.Description = "Updated description";

				var updatedProduct = await productService.UpdateProductDescription(product);

				Assert.IsTrue(updatedProduct.Description == "Updated description");
			}
		}

		[TestCleanup]
		public void Cleanup()
		{
			// Vymazání dat po každém testu
			using (var context = new AlzaShopDbContext(_options))
			{
				context.Database.EnsureDeleted();
			}
		}
	}
}
