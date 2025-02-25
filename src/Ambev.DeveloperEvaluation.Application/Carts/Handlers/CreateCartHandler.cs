using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Carts.Commands;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Carts.Handlers;

public class CreateCartHandler : IRequestHandler<CreateCartCommand, int>
{
    private readonly ICartRepository _cartRepository;

    public CreateCartHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<int> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var newCart = new Cart(request.UserId, request.Products);
        int cartId = await _cartRepository.CreateAsync(newCart);
        return cartId;
    }
}