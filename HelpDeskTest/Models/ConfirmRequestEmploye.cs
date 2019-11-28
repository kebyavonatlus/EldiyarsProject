using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HelpDeskTest.Models
{
    public class ConfirmRequestEmploye
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Номер заявки")]
        public int RequestId { get; set; }


      
        [Display(Name = "Автор документа")]
        public int EmployeId { get; set; }

        [Display(Name = "Дата соглосования")]
        public DateTime? DateOfExit { get; set; }

        [Display(Name = "Комменарии")]
        public string Comment { get; set; }

        [Display(Name = "Потверждения")]
        public bool Confirmed { get; set; }

        public int ExecutorId { get; set; }
    }
}