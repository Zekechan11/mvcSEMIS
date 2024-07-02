using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using semissssloan.Entities;

namespace semissssloan.Controllers
{
    [Authorize]
    public class UserTypeController : Controller
    {
        private readonly PelayosemisContext _context;

        public UserTypeController(PelayosemisContext user)
        {
            _context = user;
        }

         public IActionResult Index()
        {
            var userType = _context.UserTypes.ToList();
            return View(userType);
        }

        [HttpPost]
        public ActionResult AddUserType(string name)
        {
            var userType = new UserType { Name = name };
            _context.UserTypes.Add(userType);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            return View(_context.UserTypes.Where(q=> q.Id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult UpdateUserType(int id, string name)
        {
            var userType = _context.UserTypes.Find(id);
            userType.Name = name;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var userType = _context.UserTypes.Where( q => q.Id == id).FirstOrDefault();
            _context.UserTypes.Remove(userType);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}