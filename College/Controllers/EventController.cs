using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using College.Models;

namespace College.Controllers
{
    public class EventController : Controller
    {
        private ApplicationDbContext _context;

        public EventController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Event
        public ActionResult Index()
        {
            var events = _context.Events;
            return View();
        }
    }
}