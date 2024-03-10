using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using College.Models;
using College.ViewModels;

namespace College.Controllers
{
    public class ProfessorController : Controller
    {
        private ApplicationDbContext _context;

        public ProfessorController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var professors = _context.Professors.Include(c => c.Department).Include(c => c.Post).ToList();
            if (User.IsInRole("CanManageData"))
            {
                return View(professors);
            }
            return View("IndexReadOnly", professors);
        }

        public ActionResult ProfessorDetails(short id)
        {
            var professor = _context.Professors.Include(c => c.Department).Include(c => c.Post)
                .SingleOrDefault(c => c.Id == id);
            return View(professor);
        }

        [Authorize(Roles = "CanManageData")]
        public ActionResult AddPost()
        {
            return View("PostForm");
        }

        public ActionResult ManagePost()
                {
                    var posts = _context.Posts.ToList();
                    return View(posts);
                }

        [Authorize(Roles = "CanManageData")]
        public ActionResult EditPost(byte id)
        {
            var post = _context.Posts.Single(c => c.Id == id);
            return View("PostForm", post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePost(Post post)
        {
            if (post.Id == 0)
            {
                _context.Posts.Add(post);
            }
            else
            {
                var postInDb = _context.Posts.Single(c => c.Id == post.Id);
                postInDb.Name = post.Name;
            }
            _context.SaveChanges();
            return RedirectToAction("ManagePost");
        }

        [Authorize(Roles = "CanManageData")]
        public ActionResult AddProfessor()
        {
            var viewModel = new ProfessorFormViewModel
            {
                Departments = _context.Departments.ToList(),
                Posts = _context.Posts.ToList()
            };
            return View("ProfessorForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveProfessor(Professor professor)
        {
            if (professor.Id == 0)
            {
                _context.Professors.Add(professor);
            }
            else
            {
                var professorInDb = _context.Professors.Single(c => c.Id == professor.Id);
                professorInDb.Name = professor.Name;
                professorInDb.ExperienceInYears = professor.ExperienceInYears;
                professorInDb.DepartmentId = professor.DepartmentId;
                professorInDb.PostId = professor.PostId;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "CanManageData")]
        public ActionResult EditProfessor(short id)
        {
            var viewModel = new ProfessorFormViewModel
            {
                Professor = _context.Professors.Single(c => c.Id == id),
                Departments = _context.Departments.ToList(),
                Posts = _context.Posts.ToList()
            };
            return View("ProfessorForm", viewModel);
        }

        [Authorize(Roles = "CanManageData")]
        public ActionResult DeleteProfessor(short id)
        {
            var professor = _context.Professors.Single(c => c.Id == id);
            _context.Professors.Remove(professor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "CanManageData")]
        public ActionResult DeletePost(byte id)
        {
            var post = _context.Posts.Single(c => c.Id == id);
            if (_context.Professors.Any(c => c.PostId == id))
            {
                return RedirectToAction("ManagePost");
            }
            _context.Posts.Remove(post);
            _context.SaveChanges();
            return RedirectToAction("ManagePost");
        }
    }
}