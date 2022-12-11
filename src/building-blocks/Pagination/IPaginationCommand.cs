namespace BuildingBlocks.Pagination;
public interface IPaginationCommand
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
