﻿using Domain.Entities;
using Domain.Ports;

namespace Domain.Services.Products;

[DomainService]
public class ProductDeleteService(IProductRepository productRepository, IUnitOfWork unitOfWork)
{
    private readonly IProductRepository _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<bool> DeleteProductAsync(Guid productId, CancellationToken cancellationToken)
    {
        var existingProduct = await _productRepository.GetSingleProductByIdAsync(productId);
        if (existingProduct == null) return false;

        await DeleteProductFromRepositoryAsync(existingProduct);
        await SaveChangesInUnitOfWorkAsync(cancellationToken);

        return true;
    }

    private async Task DeleteProductFromRepositoryAsync(Product product)
    {
        await _productRepository.DeleteProductAsync(product);
    }

    private async Task SaveChangesInUnitOfWorkAsync(CancellationToken cancellationToken)
    {
        await _unitOfWork.SaveAsync(cancellationToken);
    }
}
