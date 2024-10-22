using System.ComponentModel.DataAnnotations;

namespace JuiceWorld.Entities;

/**
 * Base entity for all entities in the database.
 */
public class BaseEntityDto
{
    public int Id { get; set; }
}
