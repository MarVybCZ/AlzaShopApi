using Microsoft.EntityFrameworkCore;

namespace AlzaShopApi.Model
{
	public class AlzaShopDbContext : DbContext
	{
		public DbSet<Product> Products { get; set; }
		public AlzaShopDbContext(DbContextOptions<AlzaShopDbContext> options) : base(options) { }
	}
}
