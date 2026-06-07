using System.ComponentModel.DataAnnotations;
using B2.App.Exceptions;

namespace B2.App.Dtos;

public class PaginationDto
{
    [Range(1, int.MaxValue)] public int Page { get; set; } = 1;

    [Range(1, int.MaxValue)] public int PageSize { get; set; }
}

public static class PaginationExtensions
{
    public static IEnumerable<T> Paginate<T>(this IEnumerable<T> src, PaginationDto? dto)
    {
        if (dto != null && (dto.Page < 1 || dto.PageSize < 0))
        {
            throw new BadRequestException("Cannot paginate on invalid dto");
        }
        return dto == null ? src : src.Skip(dto.PageSize * (dto.Page - 1)).Take(dto.PageSize == 0 ? src.Count() : dto.PageSize);
    }
}