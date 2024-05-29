using Data;
using Kursovaya.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;


namespace Kursovaya.Controllers
{
    [Authorize(Roles = "Admin, CoreAdmin")]
    public class SessionController(
        ApplicationDbContext ctx
        ) : Controller
    {
        public IActionResult Index()
        {
            var Sessions = ctx.Sessions.ToList();
            var sessionViewModels = new List<SessionViewModel>();

            foreach (var sessuin in Sessions)
            {
                var thisViewModel = new SessionViewModel
                {
                    Id = sessuin.Id,
                    TrainerId = sessuin.TrainerId,
                    SessionDate = sessuin.SessionDate,
                    Duration = sessuin.Duration,
                    SessionType = sessuin.SessionType,
                    MembershipId = sessuin.MembershipId,
                };

                sessionViewModels.Add(thisViewModel);
            }
            return View(sessionViewModels);
        }

       
        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Insert(SessionViewModel sessionView)
        {
            var session = new Session
            {
                TrainerId = sessionView.TrainerId,
                SessionDate = sessionView.SessionDate,
                Duration = sessionView.Duration,
                SessionType = sessionView.SessionType,
                MembershipId = sessionView.MembershipId,
            };



            if (ModelState.IsValid)
            {
                var membership = ctx.Memberships.Find(session.MembershipId);
                if (membership == null)
                {
                    // Handle error: membership not found
                    ModelState.AddModelError("", "Membership not found");
                    return View(sessionView);
                }

                var trainer = ctx.Trainers.Find(session.TrainerId);
                if (trainer == null)
                {
                    // Handle error: membership not found
                    ModelState.AddModelError("", "Trainer not found");
                    return View(sessionView);
                }
                ctx.Sessions.Add(session);
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sessionView);
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

