using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public interface IProductRepository
{
    Task<Product?> GetProduct(int productId);
    Task<IEnumerable<Product>> GetProductsByCondition(Func<Product, bool> predicate);
    Task<int> AddProduct(Product product);
    Task UpdateProduct(Product product);
    Task DeleteProduct(int productId);
}
