using Ambev.DeveloperEvaluation.Application.Carts.Commands;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, Unit>
{
    private readonly ICartRepository _cartRepository;

    public UpdateCartHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<Unit> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByIdAsync(request.Id);
        if (cart == null)
            throw new KeyNotFoundException($"Cart with ID {request.Id} not found.");

        // 🔹 Atualizando manualmente os valores do carrinho
        cart.UserId = request.UserId;
        cart.Products = request.Products;

        await _cartRepository.UpdateAsync(cart);

        return Unit.Value;
    }
}