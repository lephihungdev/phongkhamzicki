namespace VIT.Pre.HealthCare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VIT.DataTransferObject.HealthCare;

    public class DoctorModel
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

        [Display(Name = "Năm sinh")]
        public int? BirthYear { get; set; }

        [Display(Name = "Điện thoại")]
        public string Phone { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Sử dụng")]
        public bool Active { get; set; }

        public string Email { get; set; }

        public IList<SexDto> Sexs { get; set; }
        public IList<DoctorDto> Doctors { get; set; }
    }
}
