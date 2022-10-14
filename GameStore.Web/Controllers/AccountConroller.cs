using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP;
using GameStore.DomainCore.Identity;
using GameStore.Web.Models;
using Microsoft.AspNetCore.Identity;

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


        [HttpPost]
        public IActionResult Login(string name)
        {
            // var result = await _signInManager.SignInAsync(new AppUser{  })

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string name)
        {
            
            
            
            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult LogOff()
        {
            return View();
        }
    }
}
