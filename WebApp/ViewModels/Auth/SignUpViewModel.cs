using Infrastructure.Models;

namespace WebApp.ViewModels.Auth;

public class SignUpViewModel
{
    public SignUpModel Form { get; set; } = new SignUpModel();
    public bool TermsAndConditions { get; set; } = false;
}
