﻿namespace WebApp.Models;

public class SignUpModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set;} = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
    public bool TermsAndConditions { get; set; } = false;
}
