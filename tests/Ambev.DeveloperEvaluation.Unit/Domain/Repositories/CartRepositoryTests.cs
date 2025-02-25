using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Bogus;
using NSubstitute;
using Xunit;

public class CartRepositoryTests
{
    private readonly ICartRepository _cartRepository;
    private readonly Faker<Cart> _cartFaker;
    private readonly Faker<CartProduct> _productFaker;

    public CartRepositoryTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();

        // Gerador de Carrinhos fictícios
        _cartFaker = new Faker<Cart>()
            .CustomInstantiator(f => new Cart(
                f.Random.Int(1, 1000),    // Id aleatório
                f.Random.Int(1, 5000),   // UserId fictício
                new List<CartProduct>()  // Lista de produtos vazia inicialmente
            ));

        // ✅ Gerador de produtos fictícios usando o CONSTRUTOR CORRETO
        _productFaker = new Faker<CartProduct>()
            .CustomInstantiator(f => new CartProduct(
                f.Random.Int(1, 100),     // CartId entre 1 e 100
                f.Random.Int(1, 500),    // ProductId entre 1 e 500
                f.Random.Int(1, 20),     // Quantidade (respeitando limite do construtor)
                f.Random.Decimal(1, 100) // Preço unitário aleatório
            ));
    }

    [Fact]
    public async Task CreateAsync_Should_Add_Cart()
    {
        // Arrange - Gerando um Cart aleatório
        var cart = _cartFaker.Generate();

        // ✅ Retorna o ID do Cart gerado
        _cartRepository.CreateAsync(cart).Returns(Task.FromResult(cart.Id));

        _cartRepository.GetByIdAsync(cart.Id).Returns(Task.FromResult(cart)); // Retorna o Cart gerado

        // Act
        var createdCartId = await _cartRepository.CreateAsync(cart);
        var result = await _cartRepository.GetByIdAsync(createdCartId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(cart.Id, result.Id); // 🔹 Comparando pelo ID
        Assert.Equal(cart.UserId, result.UserId); // 🔹 Comparando pelo UserId
    }


    [Fact]
    public async Task GetByIdAsync_Should_Return_Correct_Cart()
    {
        // Arrange - Gerando um Cart aleatório
        var cart = _cartFaker.Generate();
        cart.Products.Add(_productFaker.Generate()); // ✅ Adicionando um produto corretamente
        _cartRepository.GetByIdAsync(cart.Id).Returns(cart);

        // Act
        var result = await _cartRepository.GetByIdAsync(cart.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(cart.Id, result.Id); // 🔹 Comparando pelo ID
        Assert.Equal(cart.UserId, result.UserId); // 🔹 Comparando pelo UserId
        Assert.NotEmpty(result.Products); // 🔹 Garantindo que tem pelo menos um produto
    }
}
