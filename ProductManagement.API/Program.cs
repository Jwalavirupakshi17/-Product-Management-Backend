using Microsoft.EntityFrameworkCore;
using ProductManagement.Data.Data;
using ProductManagement.Data.Interfaces;
using ProductManagement.Data.Repositories;
using ProductManagement.Services.Implementations;
using ProductManagement.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var corsPolicy = "_allowReactApp";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsPolicy);
app.UseAuthorization();
app.MapControllers();
app.Run();