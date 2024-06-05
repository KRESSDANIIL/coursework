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
    public class MembershipController(
        ApplicationDbContext ctx
        ) : Controller
    {
        [Authorize(Roles = "Admin, CoreAdmin")]
        public IActionResult Index()
        {
            var memberships = ctx.Memberships.ToList();
            var viewModel = new List<MembershipViewModel>();

            foreach (var membership in memberships)
            {
                viewModel.Add(new MembershipViewModel
                {
                    Id = membership.Id,
                    MembershipType = membership.MembershipType,
                    Cost = membership.Cost
                });
            }

            return View(viewModel);
        }

        [Authorize(Roles = "CoreAdmin")]
        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [Authorize(Roles = "CoreAdmin")]
        [HttpPost]
        public IActionResult Insert(MembershipViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                var membership = new Membership
                {
                    MembershipType = viewModel.MembershipType,
                    Cost = viewModel.Cost
                };

                ctx.Memberships.Add(membership);
                ctx.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [Authorize(Roles = "CoreAdmin")]
        public IActionResult Remove(int id)
        {
            var model = ctx.Memberships.Find(id);
            ctx.Memberships.Remove(model!);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }

}