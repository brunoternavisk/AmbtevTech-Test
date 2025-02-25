using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Infrastructure.Data;
using Ambev.DeveloperEvaluation.Infrastructure.Data.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Serilog;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class InfrastructureModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;

        Log.Information("🔌 Configurando infraestrutura...");

        // ✅ PostgreSQL ou InMemory Database
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            Log.Information("📦 Usando banco de dados InMemory...");
            builder.Services.AddDbContext<DefaultContext>(options =>
                options.UseInMemoryDatabase("TestDatabase"));
        }
        else
        {
            Log.Information("📦 Configurando PostgreSQL...");
            builder.Services.AddDbContext<DefaultContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }

        // ✅ Registrando MongoDbContext
        Log.Information("📦 Registrando MongoDbContext...");
        builder.Services.AddSingleton<MongoDbContext>();

        // ✅ Registrando repositórios
        Log.Information("🛠️ Registrando repositórios...");
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ICartRepository, CartRepository>();

        Log.Information("✅ Infraestrutura configurada!");
    }
}