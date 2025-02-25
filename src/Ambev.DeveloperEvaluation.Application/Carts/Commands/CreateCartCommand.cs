using MediatR;
using System.Collections.Generic;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands;

public class CreateCartCommand : IRequest<int>
{
    public int UserId { get; set; }
    public List<CartProduct> Products { get; set; }

    public CreateCartCommand(int userId, List<CartProduct> products)
    {
        if (userId <= 0)
            throw new ArgumentException("UserId must be a positive integer.");

        if (products == null || products.Count == 0)
            throw new ArgumentException("Cart must have at least one product.");

        UserId = userId;
        Products = products;
    }
}