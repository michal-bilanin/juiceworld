namespace BusinessLayer.DTOs;

public class WishListItemDetailDto
{
    public UserDto User { get; set; } = null!;
    public ProductDto Product { get; set; } = null!;
}
