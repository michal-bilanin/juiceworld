namespace BusinessLayer.DTOs;

public class TagFilterDto : PaginationDto
{
    public int? Id { get; set; }
    public string? Name { get; set; }
}
