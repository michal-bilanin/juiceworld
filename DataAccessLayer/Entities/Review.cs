using JuiceWorld.Entities;
using System;

public class Review: BaseEntity
{
    public int Rating { get; set; }
    public string Body { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public Product Product { get; set; }
    public User User { get; set; }
}
