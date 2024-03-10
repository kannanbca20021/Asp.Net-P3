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
    public class CourseController : Controller
    {
        private ApplicationDbContext _context;

        public CourseController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Course
        public ActionResult Index()
        {
            var courses = _context.Courses.Include(c => c.Department).ToList();
            if (User.IsInRole("CanManageData"))
            {
                return View(courses);
            }
            return View("IndexReadOnly", courses);
        }

        public ActionResult CourseDetails(byte id)
        {
            var course = _context.Courses.Include(c => c.Department).SingleOrDefault(c => c.Id == id);
            return View(course);
        }

        [Authorize(Roles = "CanManageData")]
        public ActionResult AddCourse()
        {
            var viewModel = new CourseFormViewModel
            {
                Departments = _context.Departments.ToList()
            };
            return View("CourseForm",viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCourse(Course course)
        {
            if (course.Id == 0)
            {
                _context.Courses.Add(course);
            }
            else
            {
                var courseInDb = _context.Courses.Single(c => c.Id == course.Id);
                courseInDb.Name = course.Name;
                courseInDb.Description = course.Description;
                courseInDb.DepartmentId = course.DepartmentId;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "CanManageData")]
        public ActionResult EditCourse(byte id)
        {
            var viewModel = new CourseFormViewModel
            {
                Course = _context.Courses.Single(c => c.Id == id),
                Departments = _context.Departments.ToList()
            };
            return View("CourseForm", viewModel);
        }

        [Authorize(Roles = "CanManageData")]
        public ActionResult DeleteCourse(byte id)
        {
            var course = _context.Courses.Single(c => c.Id == id);
            _context.Courses.Remove(course);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}