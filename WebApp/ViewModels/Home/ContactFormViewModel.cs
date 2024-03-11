using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.Home;

public class ContactFormViewModel
{
    [DataType(DataType.Text)]
    [Display(Name = "Full name", Prompt = "Enter your full name", Order = 0)]
    [Required(ErrorMessage = "Invalid full name")]
    [MinLength(2, ErrorMessage = "Invalid full name")]
    public string FullName { get; set; } = null!;

    [Display(Name = "Email address", Prompt = "Enter your email address", Order = 1)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Invalid email address")]
    [RegularExpression("^[^@\\s]+@[^@\\s]+\\.[^@\\s]{2,}$", ErrorMessage = "Your email address is invalid")]
    public string Email { get; set; } = null!;

    [Display(Name = "Service", Prompt = "Choose the service you are interested in", Order = 2)]
    public string? Service { get; set; }

    [Display(Name = "Message", Prompt = "Enter your message here...", Order = 3)]
    [Required(ErrorMessage = "Invalid message")]
    [DataType(DataType.MultilineText)]
    [MinLength(5, ErrorMessage = "Invalid message")]
    public string Message { get; set; } = null!;
}
