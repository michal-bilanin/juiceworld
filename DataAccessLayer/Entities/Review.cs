using JuiceWorld.Entities;
using System;

public class Review: BaseEntity
{
    public int Rating { get; set; }
    public string Body { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public virtual Product Product { get; set; }
    public virtual User User { get; set; }
}
