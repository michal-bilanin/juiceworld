using Commons.Enums;

namespace BusinessLayer.DTOs;

public class UserRegisterDto : BaseEntityDto
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Bio { get; set; } = null!;
}