using AlzaShopApi.Model;
using AlzaShopApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlzaShopApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly AlzaShopDbContext _context;
		private readonly IProductService _productService;

		public ProductController(AlzaShopDbContext context, IProductService productService)
		{
			_context = context;
			_productService = productService;
		}

		//[HttpGet]
		//public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		//{
		//	return await _context.Products.ToListAsync();
		//}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts(int pageNumber = 1, int pageSize = 10)
		{
			var products = await _productService.GetProducts(pageNumber, pageSize);
			return Ok(products);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var product = await _productService.GetProduct(id);
			if (product == null) return NotFound();
			return product;
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateProductDescription(int id, Product product)
		{
			if (id != product.Id) return BadRequest();

			var existingProduct = await _context.Products.FindAsync(id);

			if (existingProduct == null) return NotFound();

			existingProduct.Description = product.Description;
			await _context.SaveChangesAsync();
			return NoContent();
		}
	}
}
