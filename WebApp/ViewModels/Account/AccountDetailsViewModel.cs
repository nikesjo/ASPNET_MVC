using WebbApp.ViewModels.Account;

namespace WebApp.ViewModels.Account;

public class AccountDetailsViewModel
{
    public bool IsExternalAccount { get; set; }
    public ProfileInfoViewModel? ProfileInfo { get; set; }
    public BasicInfoFormViewModel ? BasicInfo { get; set; }
    public AddressInfoFormViewModel? AddressInfo { get; set; }
}
