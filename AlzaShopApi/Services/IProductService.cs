using AlzaShopApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace AlzaShopApi.Services
{
	/// <summary>
	/// Represents a service for managing products.
	/// </summary>
	public interface IProductService
	{
		/// <summary>
		/// Retrieves a product by its ID.
		/// </summary>
		/// <param name="id">The ID of the product.</param>
		/// <returns>The product with the specified ID.</returns>
		Task<Product?> GetProduct(int id);

		/// <summary>
		/// Retrieves a list of products with pagination support.
		/// </summary>
		/// <param name="pageNumber">The page number.</param>
		/// <param name="pageSize">The number of products per page.</param>
		/// <returns>A list of products.</returns>
		Task<IEnumerable<Product>> GetProducts(int pageNumber, int pageSize);

		/// <summary>
		/// Updates the description of a product.
		/// </summary>
		/// <param name="product">The product to update.</param>
		/// <returns>The updated product, or null if the product does not exist.</returns>
		Task<Product?> UpdateProductDescription(Product product);
	}
}
