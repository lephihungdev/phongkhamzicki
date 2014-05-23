namespace VIT.Pre.HealthCare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using VIT.DataTransferObject.HealthCare;

    public class ChargeModel
    {
        [Display(Name = "Mã bệnh án")]
        public int Id { get; set; }

        [Display(Name = "Mã bệnh nhân")]
        public int PatientId { get; set; }

        [Display(Name = "Tên bệnh nhân")]
        public string PatientName { get; set; }

        [Display(Name = "Bác sĩ")]
        public Nullable<int> DoctorId { get; set; }

        [Display(Name = "Bác sĩ")]
        public string DoctorName { get; set; }

        [Display(Name = "Chuẩn đoán")]
        [UIHint("autoComplete"), AllowHtml]
        [AutoComplete("Settings", "IcdDescriptionComplete")]
        public string Diagnostic { get; set; }

        [Display(Name = "ICD 1")]
        [UIHint("autoComplete"), AllowHtml]
        [AutoComplete("Settings", "IcdCodeComplete")]
        public string ICDCode1 { get; set; }

        [Display(Name = "ICD 2")]
        [UIHint("autoComplete"), AllowHtml]
        [AutoComplete("Settings", "IcdCodeComplete")]
        public string ICDCode2 { get; set; }

        [Display(Name = "ICD 3")]
        [UIHint("autoComplete"), AllowHtml]
        [AutoComplete("Settings", "IcdCodeComplete")]
        public string ICDCode3 { get; set; }

        [Display(Name = "ICD 4")]
        [UIHint("autoComplete"), AllowHtml]
        [AutoComplete("Settings", "IcdCodeComplete")]
        public string ICDCode4 { get; set; }

        [Display(Name = "Điều trị")]
        [Required(ErrorMessage = "Không được để trống.")]
        public string CPTCode { get; set; }

        [Display(Name = "Điều trị")]
        public string CPTDescription { get; set; }

        [Display(Name = "Thuốc")]
        public string Drugs { get; set; }

        [Display(Name = "Số ngày")]
        public int Days { get; set; }

        [Display(Name = "Ghi chú")]
        public string Note { get; set; }

        [Display(Name = "Ngày điều trị")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> DateService { get; set; }

        [Display(Name = "Ngày bệnh")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> DateOnset { get; set; }

        public IList<CptDto> ListCpts { get; set; }
        public IList<DoctorDto> ListDoctors { get; set; }
        public IList<DrugDto> ListDrugs { get; set; }
        public IList<IcdDto> ListIcds { get; set; }
        public IList<ChargeDto> ListCharges { get; set; }
    }
}
