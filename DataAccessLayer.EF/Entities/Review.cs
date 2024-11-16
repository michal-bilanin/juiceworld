using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuiceWorld.Entities;

public class Review : BaseEntity
{
    [Range(1, 5)]
    public int Rating { get; set; }

    [MaxLength(500)]
    public string Body { get; set; } = null!;

    public int ProductId { get; set; }
    public int UserId { get; set; }

    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } = null!;
}
