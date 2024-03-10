using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using College.Models;

namespace College.ViewModels
{
    public class CourseFormViewModel
    {
        public Course Course { get; set; }
        public IEnumerable<Department> Departments { get; set; }
    }
}