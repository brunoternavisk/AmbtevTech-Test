using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Carts.Queries;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Carts.QueryHandlers;

public class GetAllCartsHandler : IRequestHandler<GetAllCartsQuery, IEnumerable<Cart>>
{
    private readonly ICartRepository _cartRepository;

    public GetAllCartsHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<IEnumerable<Cart>> Handle(GetAllCartsQuery request, CancellationToken cancellationToken)
    {
        int page = request.Page > 0 ? request.Page : 1;
        int size = request.Size > 0 ? request.Size : 10;
        string orderBy = !string.IsNullOrWhiteSpace(request.OrderBy) ? request.OrderBy : "id asc";

        return await _cartRepository.GetPagedAsync(page, size, orderBy);
    }
}