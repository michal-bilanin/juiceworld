using Commons.Enums;

namespace BusinessLayer.DTOs;

public class UserDto : BaseEntityDto
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public int PasswordHashRounds { get; set; }
    public string PasswordSalt { get; set; } = null!;
    public UserRole UserRole { get; set; } = UserRole.Customer;
    public string Bio { get; set; } = null!;
}
