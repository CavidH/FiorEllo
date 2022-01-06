﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FiorEllo.Models;
using FiorEllo.ViewModel.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FiorEllo.Controllers
{
    public class AuthController : Controller
    {
        private UserManager<ApplicationUser> _userManager;

        private SignInManager<ApplicationUser> _signInManager;
        // private ILogger<ApplicationUser> _logger ;


        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            ILogger<ApplicationUser> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            // _logger = logger;
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View(register);
            ApplicationUser newUser = new ApplicationUser
            {
                FullName = register.FullName,
                UserName = register.UserName,
                Email = register.Email
            };
            var IdentityResult = await _userManager.CreateAsync(newUser, register.Password);
            if (!IdentityResult.Succeeded)
            {
                foreach (var error in IdentityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    //return Json(error.Description);
                }

                return View(register);
            }

            var Token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var ConfirmationLink = Url.Action("ConfirmEmail", "Auth",
                new {userId = newUser.Id, token = Token}, Request.Scheme);


            // msgSender(newUser.Email, ConfirmationLink);
            //mail send
            await _signInManager.SignInAsync(newUser, isPersistent: false);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (login == null) return BadRequest();
            var user = await _userManager.FindByEmailAsync(login.EMail);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email or Password wrong");
                return View(login);
            }

            if (!user.IsActived)
            {
                ModelState.AddModelError(string.Empty, "Please Active Your Account");
                return View(login);
            }

            var signResult = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);
            if (signResult.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Please Try After 10 minutes");
                return View(login);
            }

            if (signResult.Succeeded)
            {
                await _signInManager.SignInAsync(user,true);
            }

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                // ViewBag.ErrorMessage = $"bele user id:{userId} movcud deyil";
                string ErrorMsg = $"bele user id:{userId} movcud deyil";
                ViewData["msg"] = ErrorMsg;
                return View();
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                string msg = "testiqleme uchun tesekkur edirik";
                ViewData["msg"] = msg;
                return View();
            }

            string msgerror = "testiqleme de problem oldu";
            ViewData["msg"] = msgerror;
            return View();
        }

        // private void sendMsgEmail(string From ,string password,string ToEmail,string msgBody,string msgTitle="Email Confirm FiorElla")
        // {
        //     var smtpClient = new System.Net.Mail.SmtpClient("smtp.mail.ru", 587);
        //     smtpClient.Credentials = new System.Net.NetworkCredential(From, password);
        //     smtpClient.EnableSsl = true;
        //     smtpClient.Send(new System.Net.Mail.MailMessage(From,ToEmail,msgTitle,msgBody));
        // }
        // private  void  msgSender(string toEmail, string link)
        // {
        //     string msgBody = " sizin testiq linkiniz "+link;
        //     sendMsgEmail("  toEmail, msgBody);
        // }
    }
}