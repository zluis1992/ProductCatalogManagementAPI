using Domain.Dto;
using Domain.Ports;
using MediatR;

namespace Application.Products;

public class ProductQueryFilterHandler(IProductRepository repository) : IRequestHandler<ProductQueryFilter, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(ProductQueryFilter request, CancellationToken cancellationToken)
    {
        var filter = new ProductFilterDto(
            Id: request.Id,
            Name: request.Name,
            MinPrice: request.MinPrice,
            MaxPrice: request.MaxPrice
        );

        var products = await repository.GetProductsByFilterAsync(filter);
        return products.Where(x => x.Active).Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price));
    }
}
