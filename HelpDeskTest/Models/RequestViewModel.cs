using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDeskTest.Models
{
    public class RequestViewModel
    {
        public int RequestId { get; set; }
      
        public string DocumentType { get; set; }
        public string Status { get; set; }
        public string Author { get; set; }
        public DateTime DateOfRegistration { get; set; } = DateTime.Now;
        public DateTime? DateOfExit { get; set; }
       // public int ExecuterId { get; set; }

    }
}