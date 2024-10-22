using JuiceWorld.Enums;

namespace JuiceWorld.Entities;

public class UserDto : BaseEntityDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public int PasswordHashRounds { get; set; }
    public string PasswordSalt { get; set; }
    public UserRole UserRole { get; set; } = UserRole.Customer;
    public string Bio { get; set; }
}
