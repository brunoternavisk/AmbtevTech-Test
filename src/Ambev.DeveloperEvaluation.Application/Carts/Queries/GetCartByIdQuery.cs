using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Carts.Queries;

public class GetCartByIdQuery : IRequest<Cart>
{
    public int Id { get; set; }

    public GetCartByIdQuery(int id)
    {
        Id = id;
    }
}