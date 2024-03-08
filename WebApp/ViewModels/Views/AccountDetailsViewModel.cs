using Infrastructure.Models;

namespace WebApp.ViewModels.Views;

public class AccountDetailsViewModel
{
    public string Title { get; set; } = "Account Details";
    public AccountDetailsBasicInfoModel BasicInfo { get; set; } = new AccountDetailsBasicInfoModel()
    {
        ProfileImage = "images/profile-image.svg",
        FirstName = "John",
        LastName = "Doe",
        Email = "john.doe@domain.com"
    };
    public AccountDetailsAddressInfoModel AddressInfo { get; set; } = new AccountDetailsAddressInfoModel();
}
