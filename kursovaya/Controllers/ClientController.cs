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
        public async Task<IActionResult> Insert(ClientViewModel clientView)
        {
            if (!ModelState.IsValid)
            {
                return View(clientView);
            }


            var membership = await ctx.Memberships.FirstOrDefaultAsync(m => m.MembershipType == clientView.MembershipType);
            if (membership == null)
            {
                ModelState.AddModelError("", "Абонемент не существует");
                return View(clientView);
            }

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

            if (client.MembershipEndDate <= DateTime.Now || client.DateOfBirth <= DateTime.Now)
            {
                ModelState.AddModelError("", "Ошибка в указанных датах");
                return View(clientView);
            }
            else
            {
                ctx.Clients.Add(client);
                await ctx.SaveChangesAsync();



                var payment = new Payment
                {
                    ClientId = client.Id,
                    MembershipId = membership.Id,
                    PaymentDate = DateTime.Now,
                    PaymentAmount = membership.Cost
                };
                ctx.Payments.Add(payment);
                await ctx.SaveChangesAsync();

                return RedirectToAction("Index");
            }
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

