using BuildingBlocks.Domain;
using BuildingBlocks.Dto;
using Mapster;
using Microsoft.EntityFrameworkCore;
namespace BuildingBlocks.Pagination;
public static class Extensions
{
    public static async Task<PaginatedDtoResponse<TDto>> ToPaginatedDtoListAsync<T, TDto>(this IQueryable<T> source, int pageNumber, int pageSize)
        where T : class, IEntity
        where TDto : IDto
    {
        if (source == null) throw new ApplicationException();
        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        pageSize = pageSize < 10 ? 10 : pageSize;
        int count = await source.CountAsync();
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        var queryable = source.OrderBy(a => a.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).AsQueryable();
        var dtoItems = await queryable.ProjectToType<TDto>().ToListAsync();
        return new PaginatedDtoResponse<TDto>(dtoItems, count, pageNumber, pageSize);
    }
}
