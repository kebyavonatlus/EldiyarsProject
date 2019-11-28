using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskTest.Models
{
    public class RequestEmploye
    {
        [Key, Column(Order = 1)]
        [Display(Name = "Номер заявки")]
        public int RequestId { get; set; }


        [Key, Column(Order = 2)]
        [Display(Name = "Фио сотрудника")]
        public int EmployeId { get; set; }

     
    }
}