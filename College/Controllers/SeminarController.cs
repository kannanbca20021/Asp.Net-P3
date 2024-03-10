using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using College.Models;
using College.ViewModels;

namespace College.Controllers
{
    public class SeminarController : Controller
    {
        private ApplicationDbContext _context;

        public SeminarController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Seminar
        public ActionResult Index()
        {
            var seminars = _context.Seminars.Include(c => c.Department).ToList();
            if (User.IsInRole("CanManageData"))
            {
                return View(seminars);
            }
            return View("IndexReadOnly", seminars);
        }

        public ActionResult SeminarDetails(int id)
        {
            var seminar = _context.Seminars.Include(c => c.Department).SingleOrDefault(c => c.Id == id);
            return View(seminar);
        }

        [Authorize(Roles = "CanManageData")]
        public ActionResult AddSeminar()
        {
            var viewModel = new SeminarFormViewModel
            {
                Departments = _context.Departments.ToList()
            };
            return View("Seminarform", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSeminar(Seminar seminar)
        {
            if (seminar.Id == 0)
            {
                _context.Seminars.Add(seminar);
            }
            else
            {
                var seminarInDb = _context.Seminars.Single(c => c.Id == seminar.Id);
                seminarInDb.Name = seminar.Name;
                seminarInDb.Description = seminar.Description;
                seminarInDb.Date = seminar.Date;
                seminarInDb.DepartmentId = seminar.DepartmentId;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "CanManageData")]
        public ActionResult EditSeminar(int id)
        {
            var viewModel = new SeminarFormViewModel
            {
                Seminar = _context.Seminars.Single(c => c.Id == id),
                Departments = _context.Departments.ToList()
            };
            return View("SeminarForm", viewModel);
        }

        [Authorize(Roles = "CanManageData")]
        public ActionResult DeleteSeminar(int id)
        {
            var seminar = _context.Seminars.Single(c => c.Id == id);
            _context.Seminars.Remove(seminar);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}