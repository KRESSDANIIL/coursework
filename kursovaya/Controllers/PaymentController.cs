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
    public class PaymentController(
        ApplicationDbContext ctx
        ) : Controller
    {

        public IActionResult Index()
        {
            var payments = ctx.Payments.ToList();
            var paymentViewModels = new List<PaymentViewModel>();

            foreach (var payment in payments)
            {
                var thisViewModel = new PaymentViewModel
                {
                    Id = payment.Id,
                    MembershipId = payment.MembershipId,
                    PaymentDate = payment.PaymentDate,
                    PaymentAmount = payment.PaymentAmount
                };

                paymentViewModels.Add(thisViewModel);
            }
            return View(paymentViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var payment = ctx.Payments.Find(id);
            if (payment == null)
            {
                return NotFound();
            }

            ctx.Payments.Remove(payment);

            await ctx.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}

