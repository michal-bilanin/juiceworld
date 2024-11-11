namespace BusinessLayer.DTOs;

public class ReviewDto : BaseEntityDto
{
    public int Rating { get; set; }
    public string Body { get; set; } = null!;
    public int ProductId { get; set; }
    public int UserId { get; set; }
}
