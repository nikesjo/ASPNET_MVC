using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class AccountSecurityModel
{
    [Display(Name = "Current password", Prompt = "********", Order = 0)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Invalid password")]
    public string CurrentPassword { get; set; } = null!;

    [Display(Name = "New Password", Prompt = "********", Order = 1)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Invalid password")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\\$%\\^&\\*]).{8,}", ErrorMessage = "Invalid password, must be a strong password")]
    public string NewPassword { get; set; } = null!;

    [Display(Name = "Confirm new password", Prompt = "********", Order = 2)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password must be confirmed")]
    [Compare(nameof(NewPassword), ErrorMessage = "Password does not match")]
    public string ConfirmNewPassword { get; set; } = null!;
}
