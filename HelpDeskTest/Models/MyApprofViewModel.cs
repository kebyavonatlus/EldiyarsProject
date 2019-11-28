using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpDeskTest.Models
{
    public class MyApprofViewModel
    {
       
        public int Id { get; set; }

        [Display(Name = "ID заявки")]
        public int RequestId { get; set; }



        [Display(Name = "Автор документа")]
        public string EmployeId { get; set; }

        public DateTime? DateOfExit { get; set; }

        public string Comment { get; set; }

        [Display(Name = "Потвердить")]
        public bool Confirmed { get; set; }

      
    }
}