using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using semissssloan.Entities;
using semissssloan.ViewModels;

namespace semissssloan.Controllers
{
    public class ClientController : Controller
    {
        private readonly PelayosemisContext _context;

        public ClientController(PelayosemisContext client)
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

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClientInfos == null)
            {
                return NotFound();
            }

            var clientInfos = await _context.ClientInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientInfos == null)
            {
                return NotFound();
            }

            return View(clientInfos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var userTypes = _context.UserTypes.ToList();
            var clientInfo = new ClientInfo();
            ViewData["UserTypes"] = userTypes;
            return View(clientInfo);
        }

        [HttpPost]
        public IActionResult Create(ClientInfo c)
        {
            _context.ClientInfos.Add(c);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var client = _context.ClientInfos.FirstOrDefault(q => q.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            var userTypes = _context.UserTypes.ToList();
            var selectedUserType = client.UserType;

            ViewData["UserTypes"] = userTypes;
            ViewData["SelectedUserType"] = selectedUserType;

            return View(client);
        }

        [HttpPost]
        public IActionResult Update(ClientInfo c)
        {
            _context.ClientInfos.Update(c);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var client = _context.ClientInfos.Where(q => q.Id == id).FirstOrDefault();

            if (client == null)
            {
                return NotFound();
            }
            _context.ClientInfos.Remove(client);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}