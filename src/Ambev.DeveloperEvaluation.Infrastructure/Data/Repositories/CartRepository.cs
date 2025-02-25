using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core; 

namespace Ambev.DeveloperEvaluation.Infrastructure.Data.Repositories;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _context;
    private readonly MongoDbContext _mongoDbContext;

    public CartRepository(ApplicationDbContext context, MongoDbContext mongoDbContext)
    {
        _context = context;
        _mongoDbContext = mongoDbContext;
    }

    public async Task<int> CreateAsync(Cart cart)
    {
        // 🔹 PostgreSQL cuida do ID (autoincrement)
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();

        // 🔹 Salva no MongoDB também
        await _mongoDbContext.Carts.InsertOneAsync(cart);

        return cart.Id;
    }

    public async Task<Cart> GetByIdAsync(int id)
    {
        // 🔹 Primeiro busca no PostgreSQL
        var cart = await _context.Carts
            .Include(s => s.Products)
            .FirstOrDefaultAsync(s => s.Id == id);

        // 🔹 Se não encontrar no PostgreSQL, busca no MongoDB
        if (cart == null)
        {
            var filter = Builders<Cart>.Filter.Eq(c => c.Id, id);
            cart = await _mongoDbContext.Carts.Find(filter).FirstOrDefaultAsync();
        }

        return cart;
    }

    public async Task<IEnumerable<Cart>> GetPagedAsync(int page, int size, string orderBy)
    {
        var query = _context.Carts
            .Include(c => c.Products)
            .AsQueryable();

        string orderColumn = "Id";
        bool descending = false;

        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            var parts = orderBy.Trim().Split(" ");
            if (parts.Length == 2)
            {
                orderColumn = parts[0];
                descending = parts[1].ToLower() == "desc";
            }
        }

        // Obtém o nome exato da propriedade (respeitando a caixa) para o Dynamic LINQ
        var property = typeof(Cart).GetProperty(orderColumn, 
            System.Reflection.BindingFlags.IgnoreCase | 
            System.Reflection.BindingFlags.Public | 
            System.Reflection.BindingFlags.Instance);

        if (property != null)
        {
            orderColumn = property.Name;
        }
        else
        {
            orderColumn = "Id";
        }

        // Usa "descending" e "ascending" em vez de "desc" e "asc"
        query = descending
            ? query.OrderBy($"{orderColumn} descending")
            : query.OrderBy($"{orderColumn} ascending");

        var carts = await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();

        return carts;
    }
    
    public async Task<int> CountAsync()
    {
        return await _context.Carts.CountAsync();
    }

    public async Task UpdateAsync(Cart card)
    {
        var existingSale = await _context.Carts
            .Include(s => s.Products)
            .FirstOrDefaultAsync(s => s.Id == card.Id);

        if (existingSale == null)
            throw new InvalidOperationException("Sale not found in database");

        // Updates the main data
        _context.Entry(existingSale).CurrentValues.SetValues(card);
        _context.Entry(existingSale).State = EntityState.Modified;

        // Remove old items that are no longer in the new list
        var itemsToRemove = existingSale.Products
            .Where(existingItem => !card.Products.Any(newItem => newItem.Id == existingItem.Id))
            .ToList();

        foreach (var item in itemsToRemove)
        {
            _context.CartProducts.Remove(item); // Removing from bank
        }

        // Update existing items and add only new ones
        foreach (var product in card.Products)
        {
            var existingItem = existingSale.Products.FirstOrDefault(i => i.Id == product.Id);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(product);
                _context.Entry(existingItem).State = EntityState.Modified;
            }
            else
            {
                existingSale.Products.Add(product); // Add only the new items
                _context.Entry(product).State = EntityState.Added;
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        // 🔹 Deleta no PostgreSQL
        var cart = await _context.Carts.FindAsync(id);
        if (cart != null)
        {
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }

        // 🔹 Também remove no MongoDB
        var filter = Builders<Cart>.Filter.Eq(c => c.Id, id);
        await _mongoDbContext.Carts.DeleteOneAsync(filter);
    }
}
