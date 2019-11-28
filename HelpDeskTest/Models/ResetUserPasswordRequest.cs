using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskTest.Models
{
    public class ResetUserPasswordRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RequestId { get; set; }

        [Required]
        [Display(Name = "Указать наименование работ")]
        public string NameOfWorks { get; set; }

        [Required]
        [Display(Name = "Причина смены пароля")]
        public string Cause { get; set; }
    }
}