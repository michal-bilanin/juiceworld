namespace PresentationLayer.Mvc.Models;

public class ReviewViewModel : BaseEntityViewModel
{
    public int Rating { get; set; }
    public required string Body { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
}
