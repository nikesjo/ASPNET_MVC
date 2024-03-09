﻿using Infrastructure.Models;

namespace WebApp.ViewModels.Views;

public class SignInViewModel
{
    public string Title { get; set; } = "Sign in";
    public SignInModel Form { get; set; } = new SignInModel();
}
