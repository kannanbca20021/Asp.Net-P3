using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using College.Models;

namespace College.ViewModels
{
    public class ProfessorFormViewModel
    {
        public Professor Professor { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}