namespace VIT.Pre.HealthCare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using VIT.DataTransferObject.HealthCare;

    public class ChargePrintModel
    {
        [Display(Name = "Mã điều trị")]
        public int Id { get; set; }

        [Display(Name = "Mã bệnh nhân")]
        public int PatientId { get; set; }

        [Display(Name = "Tên bệnh nhân")]
        public string PatientName { get; set; }

        [Display(Name = "Bác sĩ")]
        public string DoctorName { get; set; }

        [Display(Name = "Chuẩn đoán")]
        public string Diagnostic { get; set; }

        [Display(Name = "Điều trị")]
        public string CPT { get; set; }

        [Display(Name = "Số ngày")]
        public int Days { get; set; }

        [Display(Name = "Ghi chú")]
        public string Note { get; set; }

        [Display(Name = "Ngày điều trị")]
        public Nullable<System.DateTime> DateService { get; set; }

        public IList<ChargeDrugDto> Drugs { get; set; }
    }
}
