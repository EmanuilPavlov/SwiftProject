using Microsoft.Data.Sqlite;
using NLog;
using Scalar.AspNetCore;
using SwiftProject.Data;
using SwiftProject.Parsers;
using SwiftProject.Repositories;
using SwiftProject.Services;

var builder = WebApplication.CreateBuilder(args);
var logger = LogManager.GetCurrentClassLogger();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<ISqliteConnectionFactory, SqliteConnectionFactory>();

builder.Services.AddScoped<IMT103Repository, MT103Repository>();
builder.Services.AddScoped<IMT103Service, MT103Service>();
builder.Services.AddSingleton<IMT103Parser, MT103Parser>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        logger.Info("Initializing SQLite database...");

        var factory = scope.ServiceProvider.GetRequiredService<ISqliteConnectionFactory>();
        using var connection = factory.CreateConnection();
        connection.Open();

        var sqlPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Scripts", "init.sql");
        var sqlQuery = File.ReadAllText(sqlPath);
        logger.Info($"Successfully loaded script from {sqlPath}");

        using var command = connection.CreateCommand();
        command.CommandText = sqlQuery;
        command.ExecuteNonQuery();
        logger.Info("Database initialized successfully");
    }
    catch (Exception ex) {
        logger.Error(ex, "Database initialization failed");
        throw;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
