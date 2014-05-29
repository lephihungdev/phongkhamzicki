﻿namespace VIT.Pre.HealthCare.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VIT.DataTransferObject.HealthCare;

    public class CptModel
    {
        [Display(Name = "Mã")]
        [Required(ErrorMessage = "Không được để trống.")]
        public string Code { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Sử dụng")]
        public bool Active { get; set; }

        [Display(Name = "Phí")]
        public decimal Fee { get; set; }

        public IList<CptDto> Cpts { get; set; }
    }
}
