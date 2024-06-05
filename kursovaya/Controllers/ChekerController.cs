using Data;
using Kursovaya.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;

namespace Kursovaya.Controllers
{
    public class ChekerController(
        ApplicationDbContext ctx
        ) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ChekerViewModel chekerView)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", chekerView);
            }

            var sessions = await ctx.Sessions
                .Include(s => s.Membership)
                .Where(s => s.Membership.MembershipType == chekerView.MembershipType)
                .ToListAsync();

            if (sessions == null || sessions.Count == 0)
            {
                ModelState.AddModelError("", "Сессии для указанного типа пропуска не найдены.");
                return View("Index", chekerView);
            }

            return View("Insert", sessions);
        }

    }

}



