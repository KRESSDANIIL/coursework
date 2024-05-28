﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Kursovaya.Models;
using System.Diagnostics;
public class LoginController : Controller
{
    private readonly ApplicationDbContext _context;

    public LoginController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var CoreAdminlogin = await _context.Users.FirstOrDefaultAsync(l => l.UserName == model.AdminLogin && l.Password == model.AdminPassword && l.Role == "CoreAdmin");
            var Adminlogin = await _context.Users.FirstOrDefaultAsync(l => l.UserName == model.AdminLogin && l.Password == model.AdminPassword && l.Role == "Admin");
            if (CoreAdminlogin != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, CoreAdminlogin.UserName),
                new Claim(ClaimTypes.Role, "CoreAdmin")
            };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return View("SeccesCoreAdmin");
            }
            else if (Adminlogin != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Adminlogin.UserName),
                new Claim(ClaimTypes.Role, "Admin")
            };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return View("SeccesAdmin");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login or password.");
            }
        }

        return View(model);
    }

    //[HttpPost]
    //public async Task<IActionResult> Login(LoginViewModel model)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        var CoreAdminlogin = await _context.Users.FirstOrDefaultAsync(l => l.UserName == model.AdminLogin && l.Password == model.AdminPassword && l.Role == "CoreAdmin");
    //        var Adminlogin = await _context.Users.FirstOrDefaultAsync(l => l.UserName == model.AdminLogin && l.Password == model.AdminPassword && l.Role == "Admin");
    //        if (CoreAdminlogin != null)
    //        {
    //            //var loginViewModel = new LoginViewModel
    //            //{
    //            //    AdminLogin = CoreAdminlogin.Username,
    //            //    AdminPassword = CoreAdminlogin.Password
    //            //};
    //            //return View("SeccesCoreAdmin", loginViewModel);
    //            return View("SeccesCoreAdmin");
    //        }
    //        else if (Adminlogin != null)
    //        {
    //            //var loginViewModel = new LoginViewModel
    //            //{
    //            //    AdminLogin = Adminlogin.Username,
    //            //    AdminPassword = Adminlogin.Password
    //            //};
    //            //return View("SeccesAdmin", loginViewModel);
    //            return View("SeccesAdmin");
    //        }
    //        else
    //        {
    //            ModelState.AddModelError("", "Invalid login or password.");
    //        }
    //    }

    //    return View(model);
    //}

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Login");
    }

}