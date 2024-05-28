using Data;
using Kursovaya.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;


namespace Kursovaya.Users
{
    [Authorize(Roles = "CoreAdmin")]
    public class UserController(
        ApplicationDbContext ctx
        ) : Controller
    {
        public IActionResult Index()
        {
            var Users = ctx.Users.Where(u => u.Role != "CoreAdmin").ToList();
            var UserViewModel = new List<UserViewModel>();

            foreach (var user in Users)
            {
                var thisViewModel = new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Password = user.Password,
                    Role = user.Role

                };
                UserViewModel.Add(thisViewModel);
            }
            return View(UserViewModel);
        }
        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(UserViewModel userView)
        {
            var existingUser = ctx.Users.FirstOrDefault(u => u.UserName == userView.UserName);
            if (existingUser != null)
            {
                ViewBag.ErrorMessage = "The user with this username already exists.";
                return View();
            }

            var user = new User
            {
                UserName = userView.UserName,
                Password = userView.Password,
                Role = "Admin"
            };

            if (user.UserName != null && user.Password != null) {
                ctx.Users.Add(user);
                ctx.SaveChanges();
                return RedirectToAction("Index");
                return View(userView);
            }
            return View();
        }

        public IActionResult Remove(int id)
        {
            var model = ctx.Users.Find(id);
            ctx.Users.Remove(model!);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

