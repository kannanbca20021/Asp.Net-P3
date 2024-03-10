using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using College.Models;

namespace College.ViewModels
{
    public class DepartmentFormViewModel
    {
        public Department Department { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}