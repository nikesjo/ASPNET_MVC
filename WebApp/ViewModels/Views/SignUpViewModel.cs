using WebApp.Models;

namespace WebApp.ViewModels.Views;

public class SignUpViewModel
{
    public string Title { get; set; } = "Sign Up";
    public SignUpModel Form { get; set; } = new SignUpModel();
    public bool TermsAndConditions { get; set; } = false;
}
