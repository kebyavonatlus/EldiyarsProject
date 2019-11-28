using HelpDeskTest.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDeskTest.Models
{
    public class ShowInternetBankingingRequestViewModel
    {
        public int RequestId { get; set; }

        public RequestType RequestType { get; set; }

        public StatusType StatusType { get; set; }

        public string EmployeId { get; set; }

        public DateTime DateOfRegistration { get; set; }

        public string CustomerUsername { get; set; }

        public string InternetBankingPin { get; set; }
    }
}