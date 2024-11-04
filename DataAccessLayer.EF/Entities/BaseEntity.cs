using System.ComponentModel.DataAnnotations;

namespace JuiceWorld.Entities;

/**
 * Base entity for all entities in the database.
 */
public class BaseEntity
{
    [Key]
    public int Id { get; init; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
