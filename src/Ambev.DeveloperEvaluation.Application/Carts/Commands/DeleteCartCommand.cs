using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.Commands;

public class DeleteCartCommand : IRequest<Unit>
{
    public int Id { get; set; }

    public DeleteCartCommand(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Cart Id must be a positive integer.");

        Id = id;
    }
}