using Commons.Enums;

namespace BusinessLayer.DTOs;

public class UserRegisterDto : BaseEntityDto
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Bio { get; set; }
}
