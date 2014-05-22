using System;

namespace MvcApplication2.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Class1
    {

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Birthday { get; set; }
    }
}