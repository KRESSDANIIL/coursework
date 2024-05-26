using Data;
using Kursovaya.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;


namespace Kursovaya.Users
{
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
            // Check if the user already exists in the database
            var existingUser = ctx.Users.FirstOrDefault(u => u.UserName == userView.UserName);
            if (existingUser != null)
            {
                // User already exists, return an error message
                ViewBag.ErrorMessage = "The user with this username already exists.";
                return View();
            }

            // If the user doesn't exist, create a new user and add it to the database
            var user = new User
            {
                UserName = userView.UserName,
                Password = userView.Password,
                Role = "Admin"
            };

            ctx.Users.Add(user);
            ctx.SaveChanges();
            return RedirectToAction("Index");
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

