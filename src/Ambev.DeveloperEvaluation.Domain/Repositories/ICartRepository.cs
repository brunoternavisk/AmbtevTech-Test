using Ambev.DeveloperEvaluation.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ICartRepository
{
    Task<int> CreateAsync(Cart cart);
    Task<Cart> GetByIdAsync(int id);
    Task<IEnumerable<Cart>> GetPagedAsync(int page, int size, string orderBy);
    Task<int> CountAsync();
    Task UpdateAsync(Cart cart);
    Task DeleteAsync(int id);
}