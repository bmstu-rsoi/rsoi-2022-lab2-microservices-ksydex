namespace SharedKernel.Extensions;

public static class PageExtensions
{
    public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int page, int pageSize)
        => source.Skip((page - 1) * pageSize).Take(pageSize);
}