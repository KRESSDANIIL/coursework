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
    public class TrainerController(
        ApplicationDbContext ctx
        ) : Controller
    {
        [Authorize(Roles = "Admin, CoreAdmin")]
        public IActionResult Index()
        {
            var Trainers = ctx.Trainers.ToList();
            var trainserViewModels = new List<TrainerViewModel>();

            foreach (var trainer in Trainers)
            {
                var thisViewModel = new TrainerViewModel
                {
                    Id = trainer.Id,
                    FirstName = trainer.FirstName,
                    LastName = trainer.LastName,
                    Email = trainer.Email,
                    Phone = trainer.Phone,
                    Specialization = trainer.Specialization
                };

                trainserViewModels.Add(thisViewModel);
            }
            return View(trainserViewModels);
        }

        [Authorize(Roles = "CoreAdmin")]
        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }


        [Authorize(Roles = "CoreAdmin")]
        [HttpPost]
        public IActionResult Insert(TrainerViewModel trainerView)
        {
            var trainer = new Trainer
            {
                Id = trainerView.Id,
                FirstName = trainerView.FirstName,
                LastName = trainerView.LastName,
                Email = trainerView.Email,
                Phone = trainerView.Phone,
                Specialization = trainerView.Specialization
            };

            if (ModelState.IsValid)
            {
                ctx.Trainers.Add(trainer);
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trainerView);
        }

        [Authorize(Roles = "CoreAdmin")]
        public IActionResult Remove(int id)
        {
            var model = ctx.Trainers.Find(id);
            ctx.Trainers.Remove(model!);
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

