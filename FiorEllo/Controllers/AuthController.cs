using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FiorEllo.Models;
using FiorEllo.ViewModel.Auth;
using Microsoft.AspNetCore.Identity;

namespace FiorEllo.Controllers
{
    public class AuthController : Controller
    {
        private UserManager<ApplicationUser> _userManager;

        public AuthController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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

            return Json("ok");
        }
    }
}