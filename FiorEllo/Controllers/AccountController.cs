using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FiorEllo.Models;
using FiorEllo.Services.Utilities;
using FiorEllo.ViewModel.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace FiorEllo.Controllers
{
     
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;

        private SignInManager<ApplicationUser> _signInManager;
        // private ILogger<ApplicationUser> _logger ;


        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
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
            var ConfirmationLink = Url.Action("ConfirmEmail", "Account",
                new {userId = newUser.Id, token = Token}, Request.Scheme);


            EmailHelper.EmailContentBuilder(register.Email, ConfirmationLink, "Confirm Email");

            await _signInManager.SignInAsync(newUser, isPersistent: false);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login,string ReturnUrl)
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
                await _signInManager.SignInAsync(user, true);
            }

            return Redirect(ReturnUrl);
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

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ResetVM resetVm)
        {
            if (!ModelState.IsValid) return View(resetVm);
            var user = await _userManager.FindByEmailAsync(resetVm.Email);
            if (user == null)
            {
                ModelState.AddModelError(String.Empty, "Bele user movcud deyil");
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var Resetlink = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);
            EmailHelper.EmailContentBuilder(resetVm.Email,Resetlink,"Reset Password","parolu sıfırlamaq üçün təstiq et buttonuna klik edin");
            // return RedirectToAction("Index","Home");
            return Content("Please Check Your Email");
        }
        [AllowAnonymous]
        public IActionResult ResetPassword( string token,string email)
        {
            var model = new ResetPassword { Token = token, Email = email };
            return View(model);
             
        }
        [HttpPost]
        public  async Task<IActionResult>  ResetPassword( ResetPassword resetPassword)
        {
            if (!ModelState.IsValid) return View(resetPassword);
 
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
                RedirectToAction("ResetPasswordConfirmation");
 
            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
                return View();
            }
 
            return RedirectToAction("ResetPasswordConfirmation","Account");

        }
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        // private IActionResult CheckAuthenticated()
        // {
        //     if (User.Identity.IsAuthenticated)
        //     {
        //         return View();
        //     }
        // }
    }
}