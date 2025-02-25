using MediatR;
using System.Collections.Generic;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands;

public class UpdateCartCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<CartProduct> Products { get; set; }

    public UpdateCartCommand(int id, int userId, List<CartProduct> products)
    {
        Console.WriteLine($"Recebido no construtor: {System.Text.Json.JsonSerializer.Serialize(this)}");

        if (id <= 0)
            throw new ArgumentException("Cart Id must be a positive integer.");

        if (userId <= 0)
            throw new ArgumentException("UserId must be a positive integer.");

        if (products == null || products.Count == 0)
            throw new ArgumentException("Cart must have at least one product.");

        foreach (var product in products)
        {
            Console.WriteLine($"Produto recebido: {System.Text.Json.JsonSerializer.Serialize(product)}");
            if (product.Id <= 0)
                throw new ArgumentException($"Product ID must be a positive integer. Found: {product.Id}");
        }

        Id = id;
        UserId = userId;
        Products = products;
    }
}