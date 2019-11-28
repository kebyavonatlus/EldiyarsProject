using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpDeskTest.Models
{
    public class Employe
    {
        [Key]
        public int EmployeID { get; set; }

        [Required]
        [Display(Name="Фио сотрудника")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Должность")]
        public string PositionName { get; set; }

        [Required]
        [Display(Name = "Отдел")]
        public int DepartmentID { get; set; }

        [Required]
        [Display(Name = "Логин")]
        public string UserId { get; set; }
  

    }
}