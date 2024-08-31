using Domain.Enums;

namespace Domain.Dto;

public record ProductFilterDto(Guid? Id = null, string? Name = null, decimal? MinPrice = null, decimal? MaxPrice = null,
    ProductOrderEnum? OrderBy = ProductOrderEnum.None, bool IsDescending = false);
