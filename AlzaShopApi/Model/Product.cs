namespace AlzaShopApi.Model
{
	/// <summary>
	/// Represents a product in the AlzaShop API.
	/// </summary>
	public class Product
	{
		/// <summary>
		/// Gets or sets the ID of the product.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the name of the product.
		/// </summary>
		public string Name { get; set; } = null!;

		/// <summary>
		/// Gets or sets the image URI of the product.
		/// </summary>
		public string ImgUri { get; set; } = null!;

		/// <summary>
		/// Gets or sets the price of the product.
		/// </summary>
		public decimal Price { get; set; }

		/// <summary>
		/// Gets or sets the description of the product.
		/// </summary>
		public string? Description { get; set; }
	}
}
