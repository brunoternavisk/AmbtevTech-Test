using MediatR;
using System.Collections.Generic;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Carts.Queries;

public class GetAllCartsQuery : IRequest<IEnumerable<Cart>> 
{
    public int Page { get; }
    public int Size { get; }
    public string OrderBy { get; }

    public GetAllCartsQuery(int page, int size, string orderBy)
    {
        Page = page;
        Size = size;
        OrderBy = orderBy;
    }
}