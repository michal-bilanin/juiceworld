namespace BusinessLayer.DTOs;

public class OrderProductDto : BaseEntityDto
{
    public int Quantity { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
}
