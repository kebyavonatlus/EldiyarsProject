using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDeskTest.Models
{
    public class AccessRequestViewModel
    {
        public Request Request { get; set; } = new Request();
        public AccessRequest accessRequest { get; set; } = new AccessRequest();
    }
}