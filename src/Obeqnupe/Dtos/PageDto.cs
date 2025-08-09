namespace Obeqnupe.Dtos;

public class PageDto<T>
{
    public int? PageIndex { get; set; }
    public int? TotalPages { get; set; }
    public bool? HasPreviousPage { get; set; }
    public bool? HasNextPage { get; set; }
    public IEnumerable<T> Items { get; set; } = [];
}
