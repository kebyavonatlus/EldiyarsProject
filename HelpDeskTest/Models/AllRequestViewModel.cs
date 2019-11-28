using HelpDeskTest.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDeskTest.Models
{
    public class AllRequestViewModel
    {
        public int RequestId { get; set; }

        public RequestType RequestType { get; set; }

        public StatusType StatusType { get; set; }

        public string EmployeId { get; set; }
        public string ExecutorId { get; set; }

        public DateTime DateOfRegistration { get; set; }

        public DateTime? DateOfExit { get; set; }
    }
}