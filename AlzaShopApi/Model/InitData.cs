namespace AlzaShopApi.Model
{
	public static class InitData
	{
		public static void Initialize(AlzaShopDbContext context)
		{
			// Pokud již existují produkty, inicializace není potřeba
			if (context.Products.Any())
			{
				return;
			}

			// Vložení testovacích nebo výchozích produktů
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

			// Uložení změn do databáze
			context.SaveChanges();
		}
	}
}
