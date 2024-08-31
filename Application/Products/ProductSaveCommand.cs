using Domain.Dto;
using MediatR;

namespace Application.Products;

public record ProductSaveCommand(
    string Name, string? Description, decimal Price
) : IRequest<ProductDto>;
