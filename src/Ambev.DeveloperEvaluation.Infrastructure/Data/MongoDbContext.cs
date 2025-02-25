using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace Ambev.DeveloperEvaluation.Infrastructure.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetSection("MongoDbSettings:ConnectionString").Value;
        var databaseName = configuration.GetSection("MongoDbSettings:DatabaseName").Value;

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString), "MongoDB connection string is missing in appsettings.json");
        }

        if (string.IsNullOrEmpty(databaseName))
        {
            throw new ArgumentNullException(nameof(databaseName), "MongoDB database name is missing in appsettings.json");
        }

        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Cart> Carts => _database.GetCollection<Cart>("Carts");
}