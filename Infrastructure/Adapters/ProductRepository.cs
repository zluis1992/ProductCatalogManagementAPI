using System.Linq.Expressions;
using Domain.Dto;
using Domain.Entities;
using Domain.Enums;
using Domain.Ports;
using Infrastructure.Ports;
using LinqKit;

namespace Infrastructure.Adapters;

[Repository]
public class ProductRepository(IRepository<Product> dataSource) : IProductRepository
{
    private readonly IRepository<Product> _dataSource = dataSource
                                                        ?? throw new ArgumentNullException(nameof(dataSource));

    public Task<IEnumerable<Product>> GetProductsByFilterAsync(ProductFilterDto f)
    {
        var filter = BuildFilter(f);
        var orderBy = BuildOrderBy(f);

        return _dataSource.GetManyAsync(filter, orderBy);
    }

    public async Task<Product?> GetSingleProductByIdAsync(Guid id)
    {
        return await _dataSource.GetOneAsync(id);
    }

    public async Task<Product> SaveProductAsync(Product product)
    {
        return await _dataSource.AddAsync(product);
    }

    public async Task UpdateProductAsync(Product product)
    {
        await _dataSource.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(Product product)
    {
        await _dataSource.DeleteAsync(product);
    }

    private static Expression<Func<Product, bool>> BuildFilter(ProductFilterDto f)
    {
        Expression<Func<Product, bool>> filter = p => true;

        if (f.Id.HasValue) filter = filter.And(p => p.Id == f.Id.Value);

        if (!string.IsNullOrEmpty(f.Name)) filter = filter.And(p => p.Name.Contains(f.Name));

        if (f.MinPrice.HasValue) filter = filter.And(p => p.Price >= f.MinPrice.Value);

        if (f.MaxPrice.HasValue) filter = filter.And(p => p.Price <= f.MaxPrice.Value);

        return filter;
    }

    private static Func<IQueryable<Product>, IOrderedQueryable<Product>>? BuildOrderBy(ProductFilterDto f)
    {
        if (f.OrderBy == ProductOrderEnum.None) return null;

        return f.OrderBy switch
        {
            ProductOrderEnum.Name => f.IsDescending
                ? q => q.OrderByDescending(p => p.Name)
                : q => q.OrderBy(p => p.Name),

            ProductOrderEnum.Price => f.IsDescending
                ? q => q.OrderByDescending(p => p.Price)
                : q => q.OrderBy(p => p.Price),

            _ => null
        };
    }
}
