using AlzaShopApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlzaShopApi.Services
{
	/// <summary>
	/// Represents a service for managing products.
	/// </summary>
	public class ProductService : IProductService
	{
		private readonly AlzaShopDbContext _context;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProductService"/> class.
		/// </summary>
		/// <param name="context">The database context.</param>
		public ProductService(AlzaShopDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Retrieves a product by its ID.
		/// </summary>
		/// <param name="id">The ID of the product.</param>
		/// <returns>The product with the specified ID.</returns>
		public async Task<Product?> GetProduct(int id)
		{
			return await _context.Products.FindAsync(id);
		}

		/// <summary>
		/// Retrieves a list of products based on the specified page number and page size.
		/// </summary>
		/// <param name="pageNumber">The page number.</param>
		/// <param name="pageSize">The page size.</param>
		/// <returns>A list of products.</returns>
		public async Task<IEnumerable<Product>> GetProducts(int pageNumber, int pageSize)
		{
			return await _context.Products
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}

		/// <summary>
		/// Updates the description of a product.
		/// </summary>
		/// <param name="product">The product to update.</param>
		/// <returns>The updated product, or null if the product does not exist.</returns>
		public async Task<Product?> UpdateProductDescription(Product product)
		{
			var existingProduct = await _context.Products.FindAsync(product.Id);

			if (existingProduct == null) return null;

			existingProduct.Description = product.Description;
			await _context.SaveChangesAsync();

			return existingProduct;
		}
	}
}
