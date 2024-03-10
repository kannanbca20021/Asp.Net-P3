using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using College.Models;

namespace College.ViewModels
{
    public class SeminarFormViewModel
    {
        public Seminar Seminar { get; set; }
        public IEnumerable<Department> Departments { get; set; }
    }
}