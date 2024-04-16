using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.ViewModels.Account;
using WebbApp.ViewModels.Account;

namespace WebApp.Controllers;

[Authorize]
public class AccountController(UserManager<UserEntity> userManager, AddressManager addressManager) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly AddressManager _addressManager = addressManager;


    #region Details
    [HttpGet]
    [Route("/account/details")]
    public async Task<IActionResult> Details()
    {
        var claims = HttpContext.User.Identities.FirstOrDefault();

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
                        ModelState.AddModelError("IncorrectValues", "Something went wrong! Unable to update basic information.");
                        ViewData["StatusMessage"] = "danger|Something went wrong! Unable to update basic information.";
                    }else
                    {
                        ViewData["StatusMessage"] = "success|Basic Information was saved successfully.";
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
                    var address = await _addressManager.GetAddressAsync(user.Id);
                    if (address != null)
                    {
                        address.AddressLine_1 = viewModel.AddressInfo.Addressline_1;
                        address.AddressLine_2 = viewModel.AddressInfo.Addressline_2;
                        address.PostalCode = viewModel.AddressInfo.PostalCode;
                        address.City = viewModel.AddressInfo.City;

                        var result = await _addressManager.UpdateAddressAsync(address);
                        if (!result)
                        {
                            ModelState.AddModelError("IncorrectValues", "Something went wrong! Unable to update address information.");
                            ViewData["StatusMessage"] = "danger|Something went wrong! Unable to update address information.";
                        }
                        else
                        {
                            ViewData["StatusMessage"] = "success|Address Information was saved successfully.";
                        }
                    }
                    else
                    {
                        address = new AddressEntity
                        {
                            UserId = user.Id,
                            AddressLine_1 = viewModel.AddressInfo.Addressline_1,
                            AddressLine_2 = viewModel.AddressInfo.Addressline_2,
                            PostalCode = viewModel.AddressInfo.PostalCode,
                            City = viewModel.AddressInfo.City
                        };

                        var result = await _addressManager.CreateAddressAsync(address);
                        if (!result)
                        {
                            ModelState.AddModelError("IncorrectValues", "Something went wrong! Unable to update address information.");
                            ViewData["StatusMessage"] = "danger|Something went wrong! Unable to update address information.";
                        }
                        else
                        {
                            ViewData["StatusMessage"] = "success|Address Information was saved successfully.";
                        }
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

    #region UploadProfileImage

    [HttpPost]
    public async Task<IActionResult> UploadProfileImage(IFormFile file)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user != null && file != null && file.Length != 0)
        {
            var fileName = $"p_{user.Id}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/uploads/profiles", fileName);

            using var fs = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fs);

            user.ProfileImageUrl = fileName;
            await _userManager.UpdateAsync(user);
        }
        else
        {
            ViewData["StatusMessage"] = "danger|Something went wrong! Unable to update profile picture.";
        }

        return RedirectToAction("Details", "Account");
    }
    #endregion

    #region Populate Info
    private async Task<ProfileInfoViewModel> PopulateProfileInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        return new ProfileInfoViewModel
        {
            FirstName = user!.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            ProfileImageUrl = user.ProfileImageUrl,
            IsExternalAccount = user.IsExternalAccount
        };
    }

    private async Task<BasicInfoFormViewModel> PopulateBasicInfoAsync()
    {
        var user = await _userManager.GetUserAsync(User);

        return new BasicInfoFormViewModel
        {
            UserId = user!.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            Phone = user.PhoneNumber,
            Biography = user.Bio,
        };
    }

    private async Task<AddressInfoFormViewModel> PopulateAddressInfoAsync()
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var address = await _addressManager.GetAddressAsync(user.Id);
                if (address != null)
                {
                    return new AddressInfoFormViewModel
                    {
                        Addressline_1 = address.AddressLine_1,
                        Addressline_2 = address.AddressLine_2,
                        PostalCode = address.PostalCode,
                        City = address.City
                    };
                }
                return new AddressInfoFormViewModel();
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return null!;
    }
    #endregion


    #region Security
    [HttpGet]
    [Route("/account/security")]
    public async Task<IActionResult> Security()
    {
        var viewModel = new AccountSecurityViewModel
        {
            AccountDetailsInfo = new AccountDetailsViewModel
            {
                ProfileInfo = await PopulateProfileInfoAsync()
            }
        };

        return View(viewModel);
    }
    #endregion


    #region [HttpPost] Security
    [HttpPost]
    [Route("/account/security")]
    public async Task<IActionResult> Security(AccountSecurityViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);

            var currentPassword = viewModel!.CurrentPassword;
            var newPassword = viewModel.NewPassword;


            var userEntity = new UserEntity();

            var result = await _userManager.ChangePasswordAsync(userEntity, currentPassword, newPassword);
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn", "Auth");
            }
        }

        return View(viewModel);
    }
    #endregion


    #region Saved Courses
    [HttpGet]
    [Route("/account/savedcourses")]
    public async Task<IActionResult> SavedCourses()
    {
        var viewModel = new AccountDetailsViewModel
        {
            ProfileInfo = await PopulateProfileInfoAsync()
        };

        return View(viewModel);
    }
    #endregion
}