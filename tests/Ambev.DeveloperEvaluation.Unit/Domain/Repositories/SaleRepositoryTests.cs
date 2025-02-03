using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class SaleRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly SaleRepository _saleRepository;

    public SaleRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Sempre cria um novo banco em memória
            .Options;

        _context = new ApplicationDbContext(options);
        _saleRepository = new SaleRepository(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public async Task CreateAsync_Should_Add_Sale()
    {
        var sale = new Sale(
            "123456",                         // saleNumber
            Guid.NewGuid(),                   // customerId
            Guid.NewGuid(),                   // branchId
            new List<SaleItem>()                // items (pode ser vazio inicialmente)
        );


        await _saleRepository.CreateAsync(sale);
        var result = await _saleRepository.GetByIdAsync(sale.Id);

        Assert.NotNull(result);
        Assert.Equal("123456", result.SaleNumber);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Correct_Sale()
    {
        var sale = new Sale(
            "999999", // Número esperado
            Guid.NewGuid(),
            Guid.NewGuid(),
            new List<SaleItem>()
        );

        await _saleRepository.CreateAsync(sale);
        var result = await _saleRepository.GetByIdAsync(sale.Id);

        Assert.NotNull(result);
        Assert.Equal("999999", result.SaleNumber);
    }
}