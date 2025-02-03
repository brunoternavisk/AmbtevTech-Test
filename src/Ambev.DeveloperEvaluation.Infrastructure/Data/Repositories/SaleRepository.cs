namespace Ambev.DeveloperEvaluation.Domain.Repositories;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Infrastructure.Data;

public class SaleRepository : ISaleRepository
{
    private readonly ApplicationDbContext _context;

    public SaleRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> CreateAsync(Sale sale)
    {
        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();
        return sale.Id;
    }

    public async Task<Sale> GetByIdAsync(Guid id)
    {
        return await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Sale>> GetAllAsync()
    {
        return await _context.Sales.Include(s => s.Items).ToListAsync();
    }

    public async Task UpdateAsync(Sale sale)
    {
        var existingSale = await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == sale.Id);

        if (existingSale == null)
            throw new InvalidOperationException("Sale not found in database");

        // Updates the main data
        _context.Entry(existingSale).CurrentValues.SetValues(sale);
        _context.Entry(existingSale).State = EntityState.Modified;

        // Remove old items that are no longer in the new list
        var itemsToRemove = existingSale.Items
            .Where(existingItem => !sale.Items.Any(newItem => newItem.Id == existingItem.Id))
            .ToList();

        foreach (var item in itemsToRemove)
        {
            _context.SaleItems.Remove(item); // Removing from bank
        }

        // Update existing items and add only new ones
        foreach (var item in sale.Items)
        {
            var existingItem = existingSale.Items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(item);
                _context.Entry(existingItem).State = EntityState.Modified;
            }
            else
            {
                existingSale.Items.Add(item); // Add only the new items
                _context.Entry(item).State = EntityState.Added;
            }
        }

        await _context.SaveChangesAsync();
    }


    
    public async Task DeleteAsync(Guid id)
    {
        var sale = await _context.Sales.FindAsync(id);
        if (sale != null)
        {
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
        }
    }
}
