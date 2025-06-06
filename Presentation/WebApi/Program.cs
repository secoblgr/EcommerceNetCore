using Application.Interfaces;
using Application.Usecasses.CartItemServices;
using Application.Usecasses.CartServices;
using Application.Usecasses.CategoryServices;
using Application.Usecasses.CustomerServices;
using Application.Usecasses.OrderItemServices;
using Application.Usecasses.OrderServices;
using Application.Usecasses.ProductServices;
using Persistence.Context;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>();                                   // db servisimizi ekledik.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));         // kulland��gm�z repository servisimizi ekledik.
builder.Services.AddScoped<ICategoryServices, CategoryServices>();               // catgeory servisimizi ekledik.
builder.Services.AddScoped<ICustomerServices, CustomerServices>();              // customer servisimizi ekledik.
builder.Services.AddScoped<IOrderServices, OrderServices>();              // order servisimizi ekledik.
builder.Services.AddScoped<IOrderItemServices, OrderItemServices>();              // orderitem servisimizi ekledik
builder.Services.AddScoped<IProductServices, ProductServices>();              // product servisimizi ekledik.
builder.Services.AddScoped<ICartServices, CartServices>();              // product servisimizi ekledik.
builder.Services.AddScoped<ICartItemServices, CartItemServices>();              // product servisimizi ekledik.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
