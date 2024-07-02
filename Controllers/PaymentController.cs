using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using semissssloan.Entities;
using semissssloan.Models;
using semissssloan.ViewModels;

namespace semissssloan.Controllers
{
    [Authorize]
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
            // var payment = _context.Payments.Where(e => e.LoanId == id).ToList();

            var payment = GetPayments(id);

            if (payment == null)
            {
                return NotFound();
            }

            ViewData["ClientId"] = payment.FirstOrDefault()?.ClientId;

            return View(payment);
        }

        [HttpPost]
        public IActionResult Index(PaymentInputModel pvm)
        {
            Loan? loan = _context.Loans.FirstOrDefault(l => l.Id == pvm.Lid);
            // Payment? payment = _context.Payments.FirstOrDefault(l => l.PaymentId == pvm.Pid);

            if (loan == null)
            {
                return NotFound();
            }

            loan.Collected += pvm.Amnt;
            loan.Collectable = Math.Max(loan.Collectable - pvm.Amnt, 0);
            //payment.Collectable = Math.Max(payment.Collectable - pvm.Amnt, 0);

            _context.Loans.Update(loan);
            //_context.Payments.Update(payment);
            _context.SaveChanges();

            LogTransaction(pvm);

            return RedirectToAction("Index", new { id = pvm.Lid });
        }

        private void LogTransaction(PaymentInputModel pvm)
        {
            decimal tAmount = pvm.Amnt;
            var payments = GetPayments(pvm.Lid);

            foreach (var payment in payments)
            {
                if (tAmount <= 0)
                {
                    break;
                }

                decimal amountToLog = Math.Min(tAmount, payment.Collectable);

                if (amountToLog > 0)
                {
                    var transaction = new Transac
            {
                PaymentId = payment.PaymentId,
                LoanId = pvm.Lid,
                Amount = amountToLog,
                Date = DateTime.Now // Set the transaction date to the current date and time
            };

                    _context.Transacs.Add(transaction);
                    tAmount -= amountToLog;
                }
            }

            _context.SaveChanges();
        }

        private List<PaymentViewModel> GetPayments(int loanID)
        {
            var paymentList =
               from paymentz in _context.Payments.Where(e => e.LoanId == loanID)
               join transaction in _context.Transacs
               on paymentz.PaymentId equals transaction.PaymentId into pGroup
               from transaction in pGroup.DefaultIfEmpty()
               group transaction by paymentz into ptGroup
               select new PaymentViewModel
               {
                   PaymentId = ptGroup.Key.PaymentId,
                   LoanId = ptGroup.Key.LoanId,
                   ClientId = ptGroup.Key.ClientId,
                   Collectable = (decimal)(ptGroup.Key.Collectable - ptGroup.Sum(t => t.Amount)),
                   CollectableOG = ptGroup.Key.Collectable,
                   Date = ptGroup.Key.Date
               };
               

            return paymentList.ToList();
        }


    }
}