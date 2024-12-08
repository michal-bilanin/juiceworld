using Commons.Enums;

namespace BusinessLayer.DTOs;

public class UserDto : BaseEntityDto
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public int PasswordHashRounds { get; set; }
    public required string PasswordSalt { get; set; }
    public UserRole UserRole { get; set; } = UserRole.Customer;
    public required string Bio { get; set; }
}
