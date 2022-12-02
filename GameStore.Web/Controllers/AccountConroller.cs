using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.DomainCore.Identity;
using GameStore.Web.Models;
using GameStore.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace GameStore.Web.Controllers
{

    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Console.WriteLine(JsonConvert.SerializeObject(model));
            var result = 
                    await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Register", "Account");
                }
        }
       
        [HttpGet]
        public IActionResult Register()
        {
            return View(); //return
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            
            
            Console.WriteLine(JsonConvert.SerializeObject(register));

            var user = new AppUser
            {
                Email = register.Email,
                UserName = register.UserName,
                Membership = "Admin",
                UserRoles = new List<AppRole>(new []{new AppRole
                {
                    Name = "Admin",
                }})
                
            };
            
            
            
            var result = await _userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            
            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
