namespace CleanArchitecture.Domain.Shared;

public record PaginationParams
{
    public const int MaxPageSize = 50;
    public int PageNumber { get; init; } = 1;

    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        init => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    public string? OrderBy { get; init; }
    public bool OrderAsc { get; init; } = true;
    public string? Search { get; init; }

}
    