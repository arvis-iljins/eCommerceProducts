using BusinessLogicLayer.Mappings;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddBusinessLogicLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfiles>());
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>();
        return builder;
    }
}
