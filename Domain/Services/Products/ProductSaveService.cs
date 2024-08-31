using Domain.Entities;
using Domain.Ports;

namespace Domain.Services.Products;

[DomainService]
public class ProductSaveService(IProductRepository productRepository, IUnitOfWork unitOfWork)
{
    public async Task<Product> SaveProductAsync(Product p, CancellationToken cancellationToken)
    {
        ValidateProduct(p);
        var product = await SaveProductInRepositoryAsync(p);
        await SaveChangesInUnitOfWorkAsync(cancellationToken);
        return product;
    }

    private async Task<Product> SaveProductInRepositoryAsync(Product product)
    {
        return await productRepository.SaveProductAsync(product);
    }

    private async Task SaveChangesInUnitOfWorkAsync(CancellationToken cancellationToken)
    {
        await unitOfWork.SaveAsync(cancellationToken);
    }

    public static void ValidateProduct(Product p)
    {
        if (p == null) throw new ArgumentNullException(nameof(p));
    }
}
