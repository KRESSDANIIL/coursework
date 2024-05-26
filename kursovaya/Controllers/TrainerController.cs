using Data;
using Kursovaya.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection;


namespace Kursovaya.Controllers
{
    public class TrainerController(
        ApplicationDbContext ctx
        ) : Controller
    {
        public IActionResult Index()
        {
            var Trainsers = ctx.Trainers.ToList();
            var trainserViewModels = new List<TrainerViewModel>();

            foreach (var trainser in Trainsers)
            {
                var thisViewModel = new TrainerViewModel
                {
                    Id = trainser.Id,
                    FirstName = trainser.FirstName,
                    LastName = trainser.LastName,
                    Email = trainser.Email,
                    Phone = trainser.Phone,
                    Specialization = trainser.Specialization
                };

                trainserViewModels.Add(thisViewModel);
            }
            return View(trainserViewModels);
        }

       
        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }



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

