namespace AlzaShopApi.Model
{
	/// <summary>
	/// Provides methods for initializing the database with test or default products.
	/// </summary>
	public static class InitData
	{
		/// <summary>
		/// Initializes the database with test or default products if no products exist.
		/// </summary>
		/// <param name="context">The database context.</param>
		public static void Initialize(AlzaShopDbContext context)
		{
			// If there are already products, initialization is not needed
			if (context.Products.Any())
			{
				return;
			}

			// Insert test or default products
			context.Products.AddRange(
				new Product
				{
					Name = "Product 1",
					ImgUri = "https://example.com/product1.png",
					Price = 199.99M,
					Description = "Description for product 1"
				},
				new Product
				{
					Name = "Product 2",
					ImgUri = "https://example.com/product2.png",
					Price = 299.99M,
					Description = "Description for product 2"
				},
				new Product
				{
					Name = "Product 3",
					ImgUri = "https://example.com/product3.png",
					Price = 399.99M,
					Description = "Description for product 3"
				}
			);

			// Save changes to the database
			context.SaveChanges();
		}
	}
}
