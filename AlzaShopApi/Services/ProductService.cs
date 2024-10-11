using AlzaShopApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlzaShopApi.Services
{
	public class ProductService : IProductService
	{
		private readonly AlzaShopDbContext _context;

		public ProductService(AlzaShopDbContext context)
		{
			_context = context;
		}

		public async Task<Product> GetProduct(int id)
		{
			return await _context.Products.FindAsync(id);
		}

		public async Task<IEnumerable<Product>> GetProducts(int pageNumber, int pageSize)
		{
			return await _context.Products
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}
	}
}
