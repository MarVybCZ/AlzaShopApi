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

		/// <summary>
		/// Initializes a new instance of the <see cref="ProductController"/> class.
		/// </summary>
		/// <param name="context">The database context.</param>
		/// <param name="productService">The product service.</param>
		public ProductController(AlzaShopDbContext context, IProductService productService)
		{
			_context = context;
			_productService = productService;
		}

		//[HttpGet]
		//public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		//{
		//    return await _context.Products.ToListAsync();
		//}

		/// <summary>
		/// Gets a list of products.
		/// </summary>
		/// <param name="pageNumber">The page number.</param>
		/// <param name="pageSize">The page size.</param>
		/// <returns>The list of products.</returns>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts(int pageNumber = 1, int pageSize = 10)
		{
			var products = await _productService.GetProducts(pageNumber, pageSize);
			return Ok(products);
		}

		/// <summary>
		/// Gets a product by ID.
		/// </summary>
		/// <param name="id">The ID of the product.</param>
		/// <returns>The product.</returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var product = await _productService.GetProduct(id);
			if (product == null) return NotFound();
			return product;
		}

		/// <summary>
		/// Updates the description of a product.
		/// </summary>
		/// <param name="id">The ID of the product.</param>
		/// <param name="product">The updated product.</param>
		/// <returns>The result of the update operation.</returns>
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateProductDescription(int id, Product product)
		{
			if (id != product.Id) return BadRequest();

			var updatedProduct = await _productService.UpdateProductDescription(product);

			if (updatedProduct == null)
			{
				return NotFound();
			}

			return NoContent();
		}
	}
}
