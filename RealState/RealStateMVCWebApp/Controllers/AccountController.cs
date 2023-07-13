using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RealStateMVCWebApp.DTO.Account;
using RealStateMVCWebApp.Models;
using RealStateMVCWebApp.Service;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Xml.Linq;

namespace RealStateMVCWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IValidator<User> _userValidator;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountController(IValidator<User> userValidator, UserManager<ApplicationUser> userManager,
                        RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager,
                        IEmailService emailService)
        {
            _userValidator = userValidator;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        [AllowAnonymous]
        public IActionResult DualSign(string returnUrl)
        {
            ViewBag.ReturnUrl = string.IsNullOrWhiteSpace(returnUrl) ? "/home/dashboard" : returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DualSign(DualSignDto dualSignDto, string q, string returnUrl)
        {
            try
            {
                if (q == "login")
                {

                    if (string.IsNullOrWhiteSpace(dualSignDto.LogInEmail))
                    {
                        ModelState.AddModelError("LogInEmail", "Email is required.");
                        return View(dualSignDto);
                    }

                    if (!IsValidEmail(dualSignDto.LogInEmail))
                    {
                        ModelState.AddModelError("LogInEmail", "Enter valid email.");
                        return View(dualSignDto);
                    }


                    if (string.IsNullOrWhiteSpace(dualSignDto.LogInPassword))
                    {
                        ModelState.AddModelError("LogInPassword", "Password is required.");
                        return View(dualSignDto);
                    }

                    ApplicationUser appUser = await _userManager.FindByEmailAsync(dualSignDto.LogInEmail);
                    if (appUser != null)
                    {
                        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, dualSignDto.LogInPassword, false, false);
                        if (result.Succeeded)
                        {
                            return Redirect(returnUrl ?? "/home/dashboard");
                        }
                    }
                    ModelState.AddModelError(dualSignDto.LogInEmail, "Login Failed: Invalid Email or Password");
                    return View();

                }
                else if (q == "signup")
                {

                    var newUser = new User()
                    {
                        ConfirmPassword = dualSignDto.ConfirmPassword,
                        Email = dualSignDto.Email,
                        Name = dualSignDto.Name,
                        Password = dualSignDto.Password,
                        PhoneNumber = dualSignDto.PhoneNumber
                    };

                    var userValidatorResult = _userValidator.Validate(newUser);

                    if (!userValidatorResult.IsValid)
                    {
                        foreach (var error in userValidatorResult.Errors)
                        {
                            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                        }
                        return View(dualSignDto);
                    }

                    ApplicationUser appUser = new()
                    {
                        FullName = newUser.Name,
                        Email = newUser.Email,
                        PhoneNumber = newUser.PhoneNumber,
                        UserName = newUser.Email
                    };

                    IdentityResult result = await _userManager.CreateAsync(appUser, newUser.Password);

                    if (result.Succeeded)
                    {
                        var roleExists = await _roleManager.RoleExistsAsync("Guest");
                        if (!roleExists)
                            await _roleManager.CreateAsync(new ApplicationRole() { Name = "Guest" });

                        await _userManager.AddToRoleAsync(appUser, "Guest");

                        // send email

                        var emailToken = appUser.Id.ToString().Replace("-", "");

                        var current_user = await _userManager.FindByIdAsync(appUser.Id.ToString());
                        current_user.EmailVarifyToken = emailToken;
                        await _userManager.UpdateAsync(current_user);

                        var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = appUser.Id, token = emailToken }, Request.Scheme);

                        await _emailService.SendEmailAsync(appUser.Email, "Confirm your email", $"Please confirm your email by clicking <a href='{confirmationLink}'>here</a>.");

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                            ModelState.AddModelError("", error.Description.Replace("Username", "Email"));
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }

        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Error");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("Error");
            }

            if (user.EmailVarifyToken == token)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);

                return RedirectToAction("Dashboard", "Home", new { msg = "varified" });
            }
            else
            {
                return RedirectToAction("Error");
            }

            //var result = await _userManager.ConfirmEmailAsync(user, token);
            //if (result.Succeeded)
            //{
            //    // Email confirmed successfully
            //    return RedirectToAction("/Home/Dashboard");
            //}
            //else
            //{
            //    // Email confirmation failed
            //    return RedirectToAction("Error");
            //}
        }


        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        //public IActionResult LogIn()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult LogIn(DualSignDto logIn)
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult SignUp(DualSignDto singUp)
        //{
        //    return View();
        //}
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("DualSign", "Account");
        }
    }
}
