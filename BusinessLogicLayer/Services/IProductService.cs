using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Services;

public interface IProductService
{
    Task<ProductDto?> GetProductAsync(int productId);
    Task<IEnumerable<ProductDto>> GetProductsByConditionAsync(string? category);
    Task<int> AddProductAsync(CreateProductRequest request);
    Task UpdateProductAsync(int productId, UpdateProductRequest request);
    Task DeleteProductAsync(int productId);
}
