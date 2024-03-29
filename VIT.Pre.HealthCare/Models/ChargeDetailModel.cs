﻿namespace VIT.Pre.HealthCare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using VIT.DataTransferObject.HealthCare;

    public class ChargeDetailModel
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
        public string Diagnostic { get; set; }

        [Display(Name = "ICD 1")]
        [UIHint("autoComplete"), AllowHtml]
        [AutoComplete("Settings", "IcdComplete", "IcdCompleteById")]
        public string ICDCode1 { get; set; }

        [Display(Name = "ICD 2")]
        [UIHint("autoComplete"), AllowHtml]
        [AutoComplete("Settings", "IcdComplete", "IcdCompleteById")]
        public string ICDCode2 { get; set; }

        [Display(Name = "ICD 3")]
        [UIHint("autoComplete"), AllowHtml]
        [AutoComplete("Settings", "IcdComplete", "IcdCompleteById")]
        public string ICDCode3 { get; set; }

        [Display(Name = "ICD 4")]
        [UIHint("autoComplete"), AllowHtml]
        [AutoComplete("Settings", "IcdComplete", "IcdCompleteById")]
        public string ICDCode4 { get; set; }

        [Display(Name = "Hướng điều trị")]
        public string Treatments { get; set; }

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

        [Display(Name = "Thuốc")]
        [UIHint("autoComplete"), AllowHtml]
        [AutoComplete("Settings", "DrugComplete")]
        public int DrugId { get; set; }
        public Nullable<int> DrugQuality { get; set; }
        public string DrugNote { get; set; }

        [Display(Name = "Thuốc")]
        [UIHint("autoComplete"), AllowHtml]
        [AutoComplete("Settings", "InstrumentComplete")]
        public int InstrumentId { get; set; }
        public Nullable<int> InstrumentQuality { get; set; }
        public string InstrumentNote { get; set; }

        public ClinicalDto Clinical { get; set; }
        public IList<DoctorDto> ListDoctors { get; set; }
        public IList<ChargeDrugDto> ListChargeDrugs { get; set; }
        public IList<ChargeInstrumentDto> ListChargeInstruments { get; set; }
    }
}
