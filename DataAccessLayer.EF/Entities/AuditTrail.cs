using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Commons.Enums;

namespace JuiceWorld.Entities;

public class AuditTrail : BaseEntity
{
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } = null!;

    public TrailType TrailType { get; set; }

    [MaxLength(100)]
    public string EntityName { get; set; } = null!;

    public int PrimaryKey { get; set; }

    public List<string> ChangedColumns { get; set; } = [];
}
