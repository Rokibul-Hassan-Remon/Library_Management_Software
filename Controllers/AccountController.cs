using System;
using LibraryManagementSoftwareModels.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSoftwareRepository.DbConfigure;
using Microsoft.AspNetCore.Identity;
using LibraryManagementSoftwareModels.Business_Model;


namespace LibraryManagementSoftware.Controllers
{

    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationViewModel reg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = reg.Email,
                        Email = reg.Email

                    };

                    var result = await _userManager.CreateAsync(user, reg.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                        await _signInManager.SignInAsync(user, isPersistent: false);

                       
                    }
                   

                    var msg = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        msg = error.Description + "\n";
                    }
                    TempData["ErrorMessage"] = msg;

                    return RedirectToAction("LogIn");
                }

                return View(reg);
            }

            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View( ex );
            }
            //_LibraryDbContext.

        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LogInViewModel model, string returnUrl = "Home/Index")
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        TempData["SuccessMessage"] = "Successfully Logged In";
                        return RedirectToAction("Index", "Home");
                    }

                }
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
                TempData["Success Message"] = " Sucessfuly LogOut .";
                return RedirectToAction("Register", "Account");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View("NotFound");
            }

        }
    }
}
