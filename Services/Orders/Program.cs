using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore;
using Npgsql;
using Orders.Model;
using Orders.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

Npgsql.NpgsqlConnection.GlobalTypeMapper.MapEnum<OrdersCurrency>("OrdersCurrency");
Npgsql.NpgsqlConnection.GlobalTypeMapper.MapEnum<OrderKind>("OrdersKind");

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
