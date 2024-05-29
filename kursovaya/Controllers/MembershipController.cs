using Data;
using Kursovaya.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;


namespace Kursovaya.Controllers
{
    [Authorize(Roles = "CoreAdmin")]
    public class MembershipController(
        ApplicationDbContext ctx
        ) : Controller
    {
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

        public IActionResult Create()
        {
            return View(new MembershipViewModel());
        }

        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MembershipViewModel viewModel)
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

        public IActionResult Remove(int id)
        {
            var model = ctx.Memberships.Find(id);
            ctx.Memberships.Remove(model!);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
    }

}