using Application.Interfaces;
using Application.Interfaces.ICartItemsRepository;
using Application.Interfaces.ICartsRepository;
using Application.Interfaces.IOrdersRepository;
using Application.Interfaces.IProductsRepository;
using Application.Usecasses.AccountServices;
using Application.Usecasses.CartItemServices;
using Application.Usecasses.CartServices;
using Application.Usecasses.CategoryServices;
using Application.Usecasses.ContactServices;
using Application.Usecasses.CustomerServices;
using Application.Usecasses.OrderItemServices;
using Application.Usecasses.OrderServices;
using Application.Usecasses.ProductServices;
using Application.Usecasses.SubscriberServices;
using Application.Usecasses.SupportServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Context.Identity;
using Persistence.Repositories;
using Persistence.Repositories.CartItemsRepository;
using Persistence.Repositories.CartsRepository;
using Persistence.Repositories.OrdersRepository;
using Persistence.Repositories.ProductsRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));         // kullandııgmız repository servisimizi ekledik.
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();               // catgeory servisimizi ekledik.
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<ICartServices, CartServices>();
builder.Services.AddScoped<ICartItemServices, CartItemServices>();
builder.Services.AddScoped<ICartsRepository, CartsRepository>();
builder.Services.AddScoped<ICartItemsRepository, CartItemsRepository>();
builder.Services.AddScoped<IOrderRepository, OrdersRepository>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserIdentityRepository, UserIdentityRepository>();
builder.Services.AddScoped<ISubscriberServices, SubscriberServices>();
builder.Services.AddScoped<ISupportServices, SupportServices>();
builder.Services.AddScoped<IContactServices, ContactServices>();







//identity servisi ve servis ayarları.
builder.Services.AddDbContext<AppIdentityDbContext>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Giriþ yapmadan eriþilmeye çalýþýldýðýnda yönlendirilecek sayfa
    });
builder.Services.AddIdentity<AppIdentityUser, AppIdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{

    options.Password.RequireDigit = true; //Þifre Sayýsal karakteri desteklesin mi?
    options.Password.RequiredLength = 6;  //Þifre minumum karakter sayýsý
    options.Password.RequireLowercase = true; //Þifre küçük harf olabilir
    options.Password.RequireUppercase = true; //Þifre büyük harf olabilir
    options.Password.RequireNonAlphanumeric = false; //Sembol bulunabilir
    options.Lockout.MaxFailedAccessAttempts = 5; //Kullanýcý kaç baþarýsýz giriþten sonra sisteme giriþ yapamasýn
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); //Baþarýsýz giriþ iþlemlerinden sonra ne kadar süre sonra sisteme giriþ hakký tanýnsýn
    options.Lockout.AllowedForNewUsers = true; //Yeni üyeler için kilit sistemi geçerli olsun mu
    options.User.RequireUniqueEmail = true; //Kullanýcý benzersiz e-mail adresine sahip olsun
    options.SignIn.RequireConfirmedEmail = false; //Kayýt iþlemleri için email onaylamasý zorunlu olsun mu?
    options.SignIn.RequireConfirmedPhoneNumber = false; //Telefon onayý olsun mu?
});


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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
