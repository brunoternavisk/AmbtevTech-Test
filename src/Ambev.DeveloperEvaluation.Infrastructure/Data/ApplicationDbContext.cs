using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Cart> Carts { get; set; } // 🔹 Renomeado para manter coerência
    public DbSet<CartProduct> CartProducts { get; set; } // 🔹 Renomeado para manter coerência

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 🔹 Configuração da Tabela Cart
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(c => c.Id); // 🔹 Define a chave primária

            entity.Property(c => c.Id)
                .ValueGeneratedOnAdd(); // 🔹 ID gerado automaticamente pelo banco

            entity.HasMany(c => c.Products)
                .WithOne()
                .HasForeignKey(p => p.CartId)
                .IsRequired(); // 🔹 Garante que cada CartProduct tenha um Cart vinculado

            entity.HasIndex(c => c.UserId); // 🔹 Índice para otimizar buscas por UserId
        });

        // 🔹 Configuração da Tabela CartProduct
        modelBuilder.Entity<CartProduct>(entity =>
        {
            entity.HasKey(cp => cp.Id); // 🔹 Define a chave primária

            entity.Property(cp => cp.Id)
                .ValueGeneratedOnAdd(); // 🔹 ID gerado automaticamente pelo banco

            entity.HasIndex(cp => cp.ProductId); // 🔹 Índice para otimizar buscas por ProductId
        });
    }

}