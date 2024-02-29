using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class AccountDetailsAddressInfoModel
{
    [DataType(DataType.Text)]
    [Display(Name = "Address line 1", Prompt = "Enter your address line", Order = 0)]
    [Required(ErrorMessage = "Address is required")]
    [MinLength(2, ErrorMessage = "Invalid address")]
    public string Addressline_1 { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "Address line 2", Prompt = "Enter your second address line", Order = 1)]
    public string? Addressline_2 { get; set; }

    [Display(Name = "Postal code", Prompt = "Enter your postal code", Order = 2)]
    [Required(ErrorMessage = "Postal code is required")]
    [DataType(DataType.PostalCode)]
    public string PostalCode { get; set; } = null!;

    [DataType(DataType.Text)]
    [Display(Name = "City", Prompt = "Enter your city", Order = 3)]
    [Required(ErrorMessage = "City is required")]
    [MinLength(2, ErrorMessage = "Invalid city")]
    public string City { get; set; } = null!;
}
