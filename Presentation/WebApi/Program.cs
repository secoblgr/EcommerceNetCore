using Application.Interfaces;
using Application.Usecasses.CategoryServices;
using Persistence.Context;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>();                                   // db servisimizi ekledik.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));         // kullandýýgmýz repository servisimizi ekledik.
builder.Services.AddScoped<ICategoryServices, CategoryServices>();               // catgeory servisimizi ekledik.

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
