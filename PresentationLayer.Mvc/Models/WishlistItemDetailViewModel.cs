namespace PresentationLayer.Mvc.Models;

public class WishlistItemDetailViewModel
{
    public int Id { get; set; }
    public required UserSimpleViewModel User { get; set; }
    public required ProductViewModel Product { get; set; }
}
