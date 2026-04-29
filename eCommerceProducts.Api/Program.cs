using System.Text.Json.Serialization;
using BusinessLogicLayer;
using DataAccessLayer;
using eCommerceProducts.Api.Middlewares;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.AddBusinessLogicLayer();
builder.AddDataAccessLayer();

// builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfiles>());
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseExceptionHandlingMiddleware();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(options =>
{
    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.Run();
