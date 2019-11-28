using HelpDeskTest.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskTest.Models
{
    public class Request
    {
        [Key]
        public int RequestId { get; set; }

        [Display(Name = "Автор")]
        public int? EmployeId { get; set; }     //Автор

        [Display(Name = "Исполнитель")]
        public int? ExecutorId { get; set; }    //Исполнитель

        [Display(Name = "Статус")]
        public StatusType StatusId { get; set; }

        [Display(Name = "Тип документа")]
        public RequestType RequestTypeID { get; set; }

        [Display(Name = "Дата создания")]
        [DataType(DataType.Date)]
        // [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfRegistration { get; set; } = DateTime.Today;

        [Display(Name = "Дата закрытия")]
        [DataType(DataType.Date)]
     //   [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfExit { get; set; }

       
    }
}