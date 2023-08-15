using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore;
using Npgsql;
using Orders.Models;
using Orders.Rabbit;
using Orders.Repositories;
using Orders.Services;
using Orders.Services.Cache;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Npgsql.NpgsqlConnection.GlobalTypeMapper.MapEnum<OrderKind>("OrdersKind");

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IRabbitClient, RabbitClient>();
builder.Services.AddScoped<ICacheService, CacheService>();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<OrderDbContext>
(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// /app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
