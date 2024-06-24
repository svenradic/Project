using System.Runtime.CompilerServices;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Stocks.Mapper;
using Stocks.Model;
using Stocks.Repository;
using Stocks.Repository.Common;
using Stocks.Service;
using Stocks.Service.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder
                .AllowAnyOrigin() // Add the origins you trust
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(StocksMapper));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    // Register the repositories with the connection string from the configuration
    containerBuilder.Register(context =>
    {
        var config = context.Resolve<IConfiguration>();
        string  connectionString = config.GetConnectionString("DefaultConnection");
        return new StockRepository(connectionString);
    }).As<IRepository<Stock>>().InstancePerLifetimeScope();

    containerBuilder.Register(context =>
    {
        var config = context.Resolve<IConfiguration>();
        string connectionString = config.GetConnectionString("DefaultConnection");
        return new TraderRepository(connectionString);
    }).As<IRepository<Trader>>().InstancePerLifetimeScope();

    containerBuilder.RegisterType<StockService>().As<IService<Stock>>();
    containerBuilder.RegisterType<TraderService>().As<IService<Trader>>();
    


});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
