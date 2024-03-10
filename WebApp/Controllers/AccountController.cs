using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels.Account;
using WebbApp.ViewModels.Account;

namespace WebApp.Controllers;

[Authorize]
public class AccountController(UserManager<UserEntity> userManager) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;


    #region Details
    [HttpGet]
    [Route("/account/details")]
    public async Task<IActionResult> Details()
    {
        var viewModel = new AccountDetailsViewModel
        {
            ProfileInfo = await PopulateProfileInfoAsync()
        };

        viewModel.BasicInfo ??= await PopulateBasicInfoAsync();
        viewModel.AddressInfo ??= await PopulateAddressInfoAsync();

        return View(viewModel);
    }
    #endregion


    #region [HttpPost] Details
    [HttpPost]
    [Route("/account/details")]
    public async Task<IActionResult> Details(AccountDetailsViewModel viewModel)
    {
        if (viewModel.BasicInfo != null)
        {
            if (viewModel.BasicInfo.FirstName != null && viewModel.BasicInfo.LastName != null && viewModel.BasicInfo.Email != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    user.FirstName = viewModel.BasicInfo.FirstName;
                    user.LastName = viewModel.BasicInfo.LastName;
                    user.Email = viewModel.BasicInfo.Email;
                    user.PhoneNumber = viewModel.BasicInfo.Phone;
                    user.Bio = viewModel.BasicInfo.Biography;

                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("IncorrectValues", "Something went wrong! Unable to save data.");
                        ViewData["StatusMessage"] = "danger|Something went wrong! Unable to save data.";
                    }
                }
            }
        }

        if (viewModel.AddressInfo != null)
        {
            if (viewModel.AddressInfo.Addressline_1 != null && viewModel.AddressInfo.PostalCode != null && viewModel.AddressInfo.City != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    user.FirstName = viewModel.BasicInfo.FirstName;
                    user.LastName = viewModel.BasicInfo.LastName;
                    user.Email = viewModel.BasicInfo.Email;
                    user.PhoneNumber = viewModel.BasicInfo.Phone;
                    user.Bio = viewModel.BasicInfo.Biography;

                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("IncorrectValues", "Something went wrong! Unable to save data.");
                        ViewData["StatusMessage"] = "danger|Something went wrong! Unable to save data.";
                    }
                }
            }
        }

        viewModel.ProfileInfo = await PopulateProfileInfoAsync();
        viewModel.BasicInfo ??= await PopulateBasicInfoAsync();
        viewModel.AddressInfo ??= await PopulateAddressInfoAsync();

        return View(viewModel);
    }

    #endregion


    #region Populate Info
    private async Task<ProfileInfoViewModel> PopulateProfileInfoAsync()
    {

    }

    private async Task<BasicInfoFormViewModel> PopulateBasicInfoAsync()
    {

    }

    private async Task<AddressInfoFormViewModel> PopulateAddressInfoAsync()
    {

    }
    #endregion
}
