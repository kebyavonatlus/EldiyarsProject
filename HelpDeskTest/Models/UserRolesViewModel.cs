using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDeskTest.Models
{
    public class UserRolesViewModel
    {
        internal string UserID;

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string EmployeName { get; set; }
    }
}