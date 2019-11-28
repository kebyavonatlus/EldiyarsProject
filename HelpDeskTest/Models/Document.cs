using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpDeskTest.Models
{
    public class Document
    {
        public int Id { get; set; }

        [Display(Name="Название отдела")]
        public string Name { get; set; }
    }
}