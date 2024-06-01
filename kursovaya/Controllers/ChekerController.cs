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
        public async Task<IActionResult> Insert(ChekerViewModel ChekerView)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", ChekerView);
            }

            var client = await ctx.Clients
             .Where(c => c.FirstName == ChekerView.FirstName && c.LastName == ChekerView.LastName)
             .FirstOrDefaultAsync();

            if (client != null)
            {
                var clientMemberships = await ctx.ClientMemberships
                 .Where(cm => cm.ClientId == client.Id)
                 .Select(cm => cm.MembershipId)
                 .ToListAsync();

                if (clientMemberships != null)
                {
                    var sessions = await ctx.Sessions
                     .Where(s => clientMemberships.Contains(s.MembershipId))
                     .ToListAsync();

                    if (sessions != null)
                    {
                        var payments = await ctx.Payments
                         .Where(p => p.ClientId == client.Id)
                         .OrderByDescending(p => p.PaymentDate)
                         .FirstOrDefaultAsync();

                        if (payments != null)
                        {
                            var EndDate = client.MembershipEndDate;
                            var ThisDate = DateTime.Now;
                            var diff = EndDate - ThisDate;

                            if (diff.Days <0)
                            {
                                ModelState.AddModelError("", "Абоннимент устарел");
                                return View("Index", ChekerView);
                            }
                        }

                        return View("insert", sessions);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Не найден");
                        return View("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Не найден");
                    return View("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Не найден");
                return View("Index");
            }
        }

    }

}



