using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpDeskTest.Models
{
    public class ShowAccessRequestViewModel
    {
        public int RequestId { get; set; }

        [Display(Name = "Дата регистрации")]
        public DateTime DateOfRegistration { get; set; }

        [Display(Name = "Примечания")]
        public string FIOEmploye { get; set; }

        [Required]
        [Display(Name = "Обоснование")]
        public string Rationale { get; set; }

        [Display(Name ="Ресурс")]
        public string Resource { get; set; }

        [Display(Name = "Автор")]
        public string EmployeId { get; set; }

        [Display(Name = "Период")]
        public DateTime? Period { get; set; }

        [Display(Name = "Спарк")]
        public bool Spark { get; set; }

        [Display(Name = "Системная папка ВНД")]
        public bool NetworkFolder { get; set; }

        [Display(Name = "Почта")]
        public bool PostOffice { get; set; }
    }
}