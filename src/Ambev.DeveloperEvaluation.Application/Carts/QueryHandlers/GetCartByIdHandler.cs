using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Carts.Queries;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Carts.QueryHandlers;

public class GetCartByIdHandler : IRequestHandler<GetCartByIdQuery, Cart> // ✅ Padrão correto
{
    private readonly ICartRepository _cartRepository;

    public GetCartByIdHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<Cart> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByIdAsync(request.Id);
        if (cart == null)
            throw new KeyNotFoundException($"Cart with ID {request.Id} not found.");

        return cart;
    }
}