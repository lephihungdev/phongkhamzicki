namespace VIT.Pre.HealthCare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using VIT.DataTransferObject.HealthCare;

    public class ChargeListModel
    {
        [Display(Name = "Mã bệnh nhân")]
        public int PatientId { get; set; }

        [Display(Name = "Tên bệnh nhân")]
        public string PatientName { get; set; }

        [Display(Name = "Ngày điều trị")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> DateService { get; set; }

        public IList<ChargeDto> ListCharges { get; set; }
    }
}
