using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using College.Models;
using College.ViewModels;

namespace College.Controllers
{
    public class DepartmentController : Controller
    {
        private ApplicationDbContext _context;

        public DepartmentController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Department
        public ActionResult Index()
        {
            var departments = _context.Departments.Include(c => c.Event).ToList();
            if (User.IsInRole("CanManageData"))
            {
                return View(departments);
            }
            return View("IndexReadOnly", departments);
        }

        public ActionResult DepartmentDetails(byte id)
        {
            var department = _context.Departments.SingleOrDefault(d => d.Id == id);
            return View(department);
        }

        public ActionResult EventDetails(byte id)
        {
            var @event = _context.Events.SingleOrDefault(c => c.Id == id);
            if (User.IsInRole("CanManageData"))
            {
                return View(@event);
            }
            return View("EventDetailsReadOnly", @event);
        }

       // [Authorize(Roles = "CanManageData")]
        public ActionResult AddDepartment()
        {
            var viewModel = new DepartmentFormViewModel
            {
                Events = _context.Events.ToList(),
                Department = new Department()
                 
            };
            return View("DepartmentForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDepartment(Department department)
        {
            if (department.Id == 0)
            {
                _context.Departments.Add(department);
            }
            else
            {
                var departmentInDb = _context.Departments.Single(c => c.Id == department.Id);
                departmentInDb.Name = department.Name;
                departmentInDb.Description = department.Description;
                departmentInDb.EventId = department.EventId;
            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }

            return RedirectToAction("Index");
        }

      //  [Authorize(Roles = "CanManageData")]
        public ActionResult EditDepartment(byte id)
                {
                    var viewModel = new DepartmentFormViewModel
                    {
                        Department = _context.Departments.Single(c => c.Id == id),
                        Events = _context.Events.ToList()
                    };
                    return View("DepartmentForm", viewModel);
                }

      //  [Authorize(Roles = "CanManageData")]
        public ActionResult AddEvent()
        {
            return View("EventForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEvent(Event @event)
        {
            if (@event.Id == 0)
            {
                _context.Events.Add(@event);
            }
            else
            {
                var eventInDb = _context.Events.Single(c => c.Id == @event.Id);
                eventInDb.Name = @event.Name;
                eventInDb.Description = @event.Description;
            }
            _context.SaveChanges();
            return RedirectToAction("EventDetails", new { id = @event.Id });
        }

      //  [Authorize(Roles = "CanManageData")]
        public ActionResult EditEvent(byte id)
        {
            return View("EventForm", _context.Events.Single(c => c.Id == id));
        }

      //  [Authorize(Roles = "CanManageData")]
        public ActionResult DeleteDepartment(byte id)
        {
            var department = _context.Departments.Single(c => c.Id == id);
            if (_context.Professors.Any(c => c.DepartmentId == id)||_context.Seminars.Any(c => c.DepartmentId == id))
            {
                return RedirectToAction("Index");
            }
            _context.Departments.Remove(department);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //[Authorize(Roles = "CanManageData")]
        public ActionResult DeleteEvent(byte id)
        {
            var @event = _context.Events.Single(c => c.Id == id);
            if (_context.Departments.Any(c => c.EventId == id))
            {
                return RedirectToAction("Index");
            }
            _context.Events.Remove(@event);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}