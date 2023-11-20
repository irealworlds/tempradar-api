namespace API.Domain.Dto;

public class PaginatedResultDto<TEntity> where TEntity : class
{
    public required IEnumerable<TEntity> Items { get; set; }
    public required int Total { get; set; }
}