namespace SharedKernel.Common.AbstractClasses;

public class PaginationModel<T>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalElements { get; set; }
    public List<T> Items { get; set; } = new();
}