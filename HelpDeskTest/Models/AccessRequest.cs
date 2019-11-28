using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskTest.Models
{
    public class AccessRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RequestId { get; set; }
        
        
        [Display(Name = "Примечания")]
        public string FioEmploye { get; set; }
 
        [Display(Name = "Обоснование")]
        [Required(ErrorMessage = "Пожалуйста введите обоснование. Без обоснование заявка не будет создано")]
        public string Rationale { get; set; }  //обоснование

        [Display(Name = "Ресурс")]
        public string Resource { get; set; }

        [Display(Name = "Spark")]
        public bool Spark { get; set; }

        [Display(Name = "Почта")]
        public bool PostOffice { get; set; }

        [Display(Name = "Сетевая папка ВНД")]
        public bool NetworkFolder { get; set; }

        [Display(Name = "Период")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Period { get; set; }
       
    }
}