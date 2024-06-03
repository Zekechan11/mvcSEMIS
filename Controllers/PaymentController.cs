using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using semissssloan.Entities;

namespace semissssloan.Controllers
{
    public class PaymentController : Controller
    {
        private readonly PelayosemisContext _context;

        public PaymentController(PelayosemisContext payments)
        {
            _context = payments;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var payment = _context.Payments.Where(e => e.LoanId == id).ToList();
            if (payment == null)
            {
                return NotFound();
            }

            ViewData["ClientId"] = payment.FirstOrDefault()?.ClientId;

            return View(payment);
        }

        [HttpPost]
        public IActionResult Index(int lid, int pid, decimal amount)
        {
            Loan loan = _context.Loans.FirstOrDefault(l => l.Id == lid);
            Payment payment = _context.Payments.FirstOrDefault(l => l.PaymentId == pid);

            if (loan == null)
            {
                return NotFound();
            }

            loan.Collected += amount;

            loan.Collectable = Math.Max(loan.Collectable - amount, 0);

             payment.Collectable = Math.Max(payment.Collectable - amount, 0);

            _context.Loans.Update(loan);
            _context.Payments.Update(payment);
            _context.SaveChanges();

             return RedirectToAction("Index", new { id = lid });
        }

    }
}