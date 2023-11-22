namespace API.Domain.Dto;

public class PaginationOptionsDto
{
    public int Skip { get; set; } = 0;
    public int Limit { get; set; } = 10;
}