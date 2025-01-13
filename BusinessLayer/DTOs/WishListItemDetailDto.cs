namespace BusinessLayer.DTOs;

public class WishListItemDetailDto
{
    public required UserDto User { get; set; }
    public required ProductDto Product { get; set; }
}
