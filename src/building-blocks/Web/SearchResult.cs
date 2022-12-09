namespace BuildingBlocks.Web;

public abstract class SearchResult<T> where T : class
{
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public List<T>? Results { get; set; }
}
