using System.ComponentModel.DataAnnotations.Schema;

namespace JuiceWorld.Entities;

public class CartItemDto : BaseEntityDto
{
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
}
