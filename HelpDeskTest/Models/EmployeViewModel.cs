using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDeskTest.Models
{
    public class EmployeViewModel
    {
        public int EmployeID { get; set; }
        public string Name { get; set; }
        public string PositionName { get; set; }
        public string DepartmentID { get; set; }
        public string UserLog { get; set; }

    }
}