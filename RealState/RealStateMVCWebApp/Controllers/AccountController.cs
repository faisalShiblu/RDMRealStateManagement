using Microsoft.AspNetCore.Mvc;
using RealStateMVCWebApp.DTO.Account;
using RealStateMVCWebApp.Models;
using System;

namespace RealStateMVCWebApp.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult DualSign(string returnUrl)
        {
            ViewBag.ReturnUrl = string.IsNullOrWhiteSpace(returnUrl) ? "/" : returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult DualSign(DualSignDto dualSignDto, string condition, string returnUrl)
        {

            if(condition == "login")
            {

            } else if(condition == "signup")
            {

            }

            return View();
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

    }
}
