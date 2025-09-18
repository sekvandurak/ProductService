namespace ProductService.Infrastructure.Repositories;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProductService.Application.Interfaces;
using ProductService.Domain.Products.Entities;
using ProductService.Infrastructure.Persistence;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products.ToListAsync(cancellationToken);
    }



    public Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        _context.Products.Update(product);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Product product, CancellationToken cancellationToken)
    {
        _context.Products.Remove(product);
        return Task.CompletedTask;
    }
}
