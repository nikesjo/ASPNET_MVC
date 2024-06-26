﻿using System.ComponentModel.DataAnnotations;
using Infrastructure.Helpers;

namespace Infrastructure.Models;

public class SignUpModel
{
    [DataType(DataType.Text)]
    [Display(Name = "First name", Prompt = "Enter your first name", Order = 0)]
    [Required(ErrorMessage = "Invalid first name")]
    [MinLength(2, ErrorMessage = "Invalid first name")]
    public string FirstName { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "Last name", Prompt = "Enter your last name", Order = 1)]
    [Required(ErrorMessage = "Invalid last name")]
    [MinLength(2, ErrorMessage = "Invalid last name")]
    public string LastName { get; set;} = null!;

    [Display(Name = "Email address", Prompt = "Enter your email address", Order = 2)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Invalid email address")]
    [RegularExpression("^[^@\\s]+@[^@\\s]+\\.[^@\\s]{2,}$", ErrorMessage = "Your email address is invalid")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password", Prompt = "********", Order = 3)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Invalid password")]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\\$%\\^&\\*]).{8,}", ErrorMessage = "Invalid password, must be a strong password")]
    public string Password { get; set; } = null!;

    [Display(Name = "Confirm password", Prompt = "********", Order = 4)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password must be confirmed")]
    [Compare(nameof(Password), ErrorMessage = "Password does not match")]
    public string ConfirmPassword { get; set; } = null!;

    [Display(Name = "I agree to the Terms & Conditions.", Order = 5)]
    [CheckBoxRequired(ErrorMessage = "You must accept the terms and conditions to proceed.")]
    public bool TermsAndConditions { get; set; } = false;
}
