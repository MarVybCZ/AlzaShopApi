using AlzaShopApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace AlzaShopApi.Services
{
	public interface IProductService
	{
		Task<Product> GetProduct(int id);
		Task<IEnumerable<Product>> GetProducts(int pageNumber, int pageSize);
	}
}
