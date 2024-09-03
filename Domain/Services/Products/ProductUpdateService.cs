using Domain.Dto;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Ports;

namespace Domain.Services.Products;

[DomainService]
public class ProductUpdateService(IProductRepository productRepository, IUnitOfWork unitOfWork)
{
    private async Task SaveChangesInUnitOfWorkAsync(CancellationToken cancellationToken)
    {
        await unitOfWork.SaveAsync(cancellationToken);
    }

    public async Task<bool> UpdateProductAsync(ProductDto updatedProduct, CancellationToken cancellationToken)
    {
        await ValidateProduct(updatedProduct);

        var existingProduct = await productRepository.GetSingleProductByIdAsync(updatedProduct.Id);
        if (existingProduct == null) return false;

        existingProduct.Name = updatedProduct.Name;
        existingProduct.Description = updatedProduct.Description;
        existingProduct.Price = updatedProduct.Price;

        await UpdateProductInRepositoryAsync(existingProduct);
        await SaveChangesInUnitOfWorkAsync(cancellationToken);

        return true;
    }

    private async Task UpdateProductInRepositoryAsync(Product product)
    {
        await productRepository.UpdateProductAsync(product);
    }

    public async Task ValidateProduct(ProductDto p)
    {
        if (p == null) throw new ArgumentNullException(nameof(p));
        var existingProduct = await productRepository.GetProductsByFilterAsync(new ProductFilterDto(null, p.Name));
        if (existingProduct.Any()) throw new ProductException("Ya existe un producto con el mismo nombre");
    }
}
