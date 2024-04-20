﻿using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApp.ViewModels.Account;
using WebbApp.ViewModels.Account;

namespace WebApp.Controllers;

[Authorize]
public class AccountController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, AddressManager addressManager, CourseService courseService, DataContext context) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly AddressManager _addressManager = addressManager;
    private readonly CourseService _courseService = courseService;
    private readonly DataContext _context = context;


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
                        ModelState.AddModelError("IncorrectValues", "Something went wrong! Unable to update basic information.");
                        ViewData["StatusMessage"] = "danger|Something went wrong! Unable to update basic information.";
                    }
                    else
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
    public IActionResult Security()
    {
        var viewModel = new AccountSecurityViewModel();

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
            if (user != null)
            {
                if (viewModel.Form != null)
                {
                    var changePassword = await _userManager.ChangePasswordAsync(user, viewModel.Form!.CurrentPassword, viewModel.Form.NewPassword);
                    if (changePassword.Succeeded)
                    {
                        ViewData["SuccessMessage"] = "success|New password created";
                    }
                    else
                    {
                        ModelState.AddModelError("IncorrectValues", "Incorrect password");
                        ViewData["ErrorMessage"] = "danger|Incorrect password, try again.";
                    }
                }
                if (viewModel.DeleteAccount != null && viewModel.DeleteAccount.ConfirmDelete)
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignOutAsync();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("DeleteError", "Could not delete account");
                        ViewData["ErrorMessage"] = "danger|Something went wrong, could not delete account.";
                    }
                }
            }
        }

        return View(viewModel);
    }
    #endregion

    #region SavedCourses

    [HttpGet]
    [Route("/account/savedcourses")]
    public async Task<IActionResult> SavedCourses()
    {
        var viewModel = new SavedCoursesViewModel();


        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            viewModel.SavedCourses = await _courseService.GetCoursesByIdsAsync(user.Id);
        }

        return View(viewModel);
    }

    [HttpPost]
    [Route("/courses/removeallcourses")]
    public async Task<IActionResult> RemoveAllCourses()
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var savedCourses = await _context.SavedCourses
                .Where(sc => sc.UserId == user.Id)
                .ToListAsync();

            if (savedCourses == null || savedCourses.Count == 0)
            {
                return Json(new { success = false, message = "No courses found to remove." });
            }

            _context.SavedCourses.RemoveRange(savedCourses);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
            return Problem();
        }
    }

    [HttpPost]
    [Route("/courses/removecourse/{courseId}")]
    public async Task<IActionResult> RemoveCourse(int courseId)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var savedCourse = await _context.SavedCourses
                .FirstOrDefaultAsync(x => x.UserId == user.Id && x.CourseId == courseId);

            if (savedCourse == null)
            {
                return Json(new { success = false, message = "Course not found." });
            }

            _context.SavedCourses.Remove(savedCourse);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            Debug.WriteLine("ERROR :: " + ex.Message);
            return Problem();
        }
    }

    #endregion
}