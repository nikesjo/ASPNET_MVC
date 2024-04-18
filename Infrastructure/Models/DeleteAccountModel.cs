using Infrastructure.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class DeleteAccountModel
{
    [Display(Name = "Yes, I want to delete my account")]
    [CheckBoxRequired(ErrorMessage = "You must confirm to delete your account.")]
    public bool ConfirmDelete { get; set; } = false;
}
