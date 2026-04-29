using DataAccessLayer.DbContext;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddDataAccessLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<DapperDbContext>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        return builder;
    }
}
