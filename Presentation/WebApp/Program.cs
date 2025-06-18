using Application.Interfaces;
using Application.Interfaces.IProductsRepository;
using Application.Usecasses.CategoryServices;
using Application.Usecasses.CustomerServices;
using Application.Usecasses.OrderItemServices;
using Application.Usecasses.OrderServices;
using Application.Usecasses.ProductServices;
using Persistence.Context;
using Persistence.Repositories;
using Persistence.Repositories.ProductsRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));         // kullandýýgmýz repository servisimizi ekledik.
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();               // catgeory servisimizi ekledik.
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();              


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cart}/{action=Index}/{id?}");

app.Run();
