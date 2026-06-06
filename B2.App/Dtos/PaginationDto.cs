using System.ComponentModel.DataAnnotations;
using B2.App.Exceptions;

namespace TIN.Core.Dtos;

public class PaginationDto
{
    [Range(1, int.MaxValue)] public required int Page { get; set; } = 1;

    [Range(1, int.MaxValue)] public required int PageSize { get; set; } = int.MaxValue;
}

public static class PaginationExtensions
{
    public static IEnumerable<T> Paginate<T>(this IEnumerable<T> src, PaginationDto? dto)
    {
        if (dto != null && (dto.Page < 1 || dto.PageSize < 1))
        {
            throw new BadRequestException("Cannot paginate on invalid dto");
        }
        return dto == null ? src : src.Skip(dto.PageSize * (dto.Page - 1)).Take(dto.PageSize);
    }
}