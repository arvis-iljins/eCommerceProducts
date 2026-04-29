using Dapper;
using DataAccessLayer.DbContext;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DapperDbContext _context;

    public ProductRepository(DapperDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetProduct(int productId)
    {
        const string sql = @"SELECT productid, productname, category, unitprice, quantityinstock
                              FROM public.products
                              WHERE productid = @ProductId";

        return await _context.Connection.QuerySingleOrDefaultAsync<Product>(sql, new { ProductId = productId });
    }

    public async Task<IEnumerable<Product>> GetProductsByCondition(Func<Product, bool> predicate)
    {
        const string sql = @"SELECT productid, productname, category, unitprice, quantityinstock
                              FROM public.products";

        var products = await _context.Connection.QueryAsync<Product>(sql);
        return products.Where(predicate);
    }

    public async Task<int> AddProduct(Product product)
    {
        const string sql = @"INSERT INTO public.products (productname, category, unitprice, quantityinstock)
                              VALUES (@ProductName, @Category, @UnitPrice, @QuantityInStock)
                              RETURNING productid";

        return await _context.Connection.ExecuteScalarAsync<int>(sql, product);
    }

    public async Task UpdateProduct(Product product)
    {
        const string sql = @"UPDATE public.products
                              SET productname = @ProductName,
                                  category = @Category,
                                  unitprice = @UnitPrice,
                                  quantityinstock = @QuantityInStock
                              WHERE productid = @ProductId";

        await _context.Connection.ExecuteAsync(sql, product);
    }

    public async Task DeleteProduct(int productId)
    {
        const string sql = "DELETE FROM public.products WHERE productid = @ProductId";

        await _context.Connection.ExecuteAsync(sql, new { ProductId = productId });
    }
}
