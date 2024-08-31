using Domain.Dto;
using Domain.Enums;
using MediatR;

namespace Application.Products;

public record ProductQueryFilter(
    Guid? Id = null,
    string? Name = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null,
    ProductOrderEnum OrderBy = ProductOrderEnum.None,
    bool? IsDescending = false
) : IRequest<IEnumerable<ProductDto>>;
