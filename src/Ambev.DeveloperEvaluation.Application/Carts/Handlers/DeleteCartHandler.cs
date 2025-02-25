using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Carts.Commands;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Carts.Handlers;

public class DeleteCartHandler : IRequestHandler<DeleteCartCommand, Unit>
{
    private readonly ICartRepository _cartRepository;

    public DeleteCartHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<Unit> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByIdAsync(request.Id);
        if (cart == null)
            throw new KeyNotFoundException($"Cart with ID {request.Id} not found.");

        await _cartRepository.DeleteAsync(request.Id);
        return Unit.Value;
    }
}