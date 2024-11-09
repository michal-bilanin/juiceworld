using System.ComponentModel.DataAnnotations.Schema;

namespace JuiceWorld.Entities;

public class Review : BaseEntity
{
    public int Rating { get; set; }
    public string Body { get; set; } = null!;
    public int ProductId { get; set; }
    public int UserId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } = null!;
}
