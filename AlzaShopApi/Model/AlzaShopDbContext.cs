using Microsoft.EntityFrameworkCore;

namespace AlzaShopApi.Model
{
	/// <summary>
	/// Represents the database context for the AlzaShop API.
	/// </summary>
	public class AlzaShopDbContext : DbContext
	{
		/// <summary>
		/// Gets or sets the DbSet of products.
		/// </summary>
		public DbSet<Product> Products { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="AlzaShopDbContext"/> class.
		/// </summary>
		/// <param name="options">The options for configuring the database context.</param>
		public AlzaShopDbContext(DbContextOptions<AlzaShopDbContext> options) : base(options) { }
	}
}
