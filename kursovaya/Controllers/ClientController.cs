using Data;
using Kursovaya.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;


namespace Kursovaya.Controllers
{
    [Authorize(Roles = "Admin, CoreAdmin")]
    public class ClientController(
        ApplicationDbContext ctx
        ) : Controller
    {

        public IActionResult Secret()
        {
            return View();
        }
        public IActionResult Index()
        {
            var Clients = ctx.Clients.ToList();
            var clientViewModels = new List<ClientViewModel>();

            foreach (var client in Clients)
            {
                var thisViewModel = new ClientViewModel
                {
                    Id = client.Id,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Email = client.Email,
                    Phone = client.Phone,
                    DateOfBirth = client.DateOfBirth,
                    Gender = client.Gender,
                    MembershipType = client.MembershipType,
                    MembershipStartDate = client.MembershipStartDate,
                    MembershipEndDate = client.MembershipEndDate
                };

                clientViewModels.Add(thisViewModel);
            }
            return View(clientViewModels);
        }

       
        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Insert(ClientViewModel clientView)
        {
            var client = new Client
            {
                FirstName = clientView.FirstName,
                LastName = clientView.LastName,
                Email = clientView.Email,
                Phone = clientView.Phone,
                DateOfBirth = clientView.DateOfBirth,
                Gender = clientView.Gender,
                MembershipType = clientView.MembershipType,
                MembershipStartDate = clientView.MembershipStartDate,
                MembershipEndDate = clientView.MembershipEndDate
            };

            if (ModelState.IsValid)
            {
                ctx.Clients.Add(client);
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clientView);
        }

       
        public IActionResult Remove(int id)
        {
            var model = ctx.Clients.Find(id);
            ctx.Clients.Remove(model!);
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

