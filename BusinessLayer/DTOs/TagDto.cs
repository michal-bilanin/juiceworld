namespace BusinessLayer.DTOs;

public class TagDto : BaseEntityDto
{
    public string Name { get; set; } = null!;
    public string ColorHex { get; set; } = null!;
}
