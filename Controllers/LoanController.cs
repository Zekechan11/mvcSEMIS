using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using semissssloan.Entities;
using semissssloan.ViewModels;

namespace semissssloan.Controllers
{
    [Authorize]
    public class LoanController : Controller
    {
        private readonly PelayosemisContext _context;

        public LoanController(PelayosemisContext client)
        {
            _context = client;
        }

        public IActionResult Index()
        {
            var clientInfos = (
                from clientInfo in _context.ClientInfos
                join usertype in _context.UserTypes
                on clientInfo.UserType equals usertype.Id
                select new ClientInfoViewModel
                {
                    Id = clientInfo.Id,
                    UserType = clientInfo.UserType,
                    UserTypeName = usertype.Name,
                    FirstName = clientInfo.FirstName,
                    MiddleName = clientInfo.MiddleName,
                    LastName = clientInfo.LastName,
                    Address = clientInfo.Address,
                    ZipCode = clientInfo.ZipCode,
                    Birthdate = clientInfo.Birthday,
                    Age = clientInfo.Age,
                    NameofFather = clientInfo.NameOfFather,
                    NameofMother = clientInfo.NameOfMother,
                    CivilStatus = clientInfo.CivilStatus,
                    Religion = clientInfo.Religion,
                    Occupation = clientInfo.Occupation,
                }).ToList();
            return View(clientInfos);
        }

        [HttpGet]
        public IActionResult ViewLoan(int id) {
            var loan = _context.Loans.Where(e => e.ClientId == id).ToList();
            if (loan == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = id;
            return View(loan);
        }

        [HttpPost]
        public IActionResult ViewLoan(Loan l) {
            l.Collectable = l.Amount + l.InterestAmount;
            l.TotalPayable = l.Collectable;

            _context.Loans.Add(l);
            _context.SaveChanges();

             GenerateSchedule(l);

            return RedirectToAction("ViewLoan", new { id = l.ClientId });
        }

        private void GenerateSchedule(Loan loan) {
            int numberOfSchedules = loan.NoOfPayment;
            var intervalDays = loan.Type.ToLower() switch
            {
                "daily" => 1,
                "weekly" => 7,
                "monthly" => 30,
                _ => throw new ArgumentException("Loan is bonk"),
            };
            for (int i = 0; i < numberOfSchedules; i++) {
                var schedule = new Payment {
                    LoanId = loan.Id,
                    ClientId = loan.ClientId,
                    Date = loan.DateCreated.AddDays(intervalDays * (i + 1)),
                    Collectable = loan.TotalPayable / numberOfSchedules,
                };
                _context.Payments.Add(schedule);
                _context.SaveChanges();
            }
        }

    }
}