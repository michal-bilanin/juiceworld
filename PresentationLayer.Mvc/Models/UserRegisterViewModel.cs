namespace PresentationLayer.Mvc.Models;
using System.ComponentModel.DataAnnotations;

public class UserRegisterViewModel
{
    private static string SpecialCharacters = """!@#$%^&*()_+\-=\[\]{};:\\|,.<>\/?`~""";
    
    [Required]
    [Display(Name = "Username")]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 100 characters.")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#])[A-Za-z\d@$!%*?&#]{8,}$", 
        ErrorMessage = "Passwords must have at least one uppercase letter, one lowercase letter, one number, and one special character.")]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }

    [MaxLength(500, ErrorMessage = "Bio cannot exceed 500 characters.")]
    [Display(Name = "Bio")]
    public string Bio { get; set; }
}
