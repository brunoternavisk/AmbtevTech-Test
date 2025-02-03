using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
    Task<Guid> CreateAsync(Sale sale);
    Task<Sale> GetByIdAsync(Guid id);
    Task<IEnumerable<Sale>> GetAllAsync();
    Task UpdateAsync(Sale sale);
    Task DeleteAsync(Guid id);
}