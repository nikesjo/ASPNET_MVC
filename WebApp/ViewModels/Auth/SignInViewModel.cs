using Infrastructure.Models;

namespace WebApp.ViewModels.Auth;

public class SignInViewModel
{
    public string Title { get; set; } = "Sign in";
    public SignInModel Form { get; set; } = new SignInModel();
}
