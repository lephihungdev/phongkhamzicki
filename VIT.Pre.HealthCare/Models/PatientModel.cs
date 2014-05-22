namespace VIT.Pre.HealthCare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VIT.DataTransferObject.HealthCare;

    public class PatientModel
    {
        [Display(Name = "Mã")]
        public int Id { get; set; }

        [Display(Name = "Họ")]
        public string FirstName { get; set; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Không được để trống.")]
        public string LastName { get; set; }

        [Display(Name = "Giới tính")]
        public Nullable<bool> Sex { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<System.DateTime> Birthday { get; set; }

        [Display(Name = "Ngày bắt đầu bệnh")]
        public Nullable<System.DateTime> DateOnSet { get; set; }

        [Display(Name = "Điện thoại")]
        public string Phone { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        public string Email { get; set; }

        public IDictionary<int, string> Sexs { get; set; }
        public IList<PatientDto> Patients { get; set; }
    }
}
