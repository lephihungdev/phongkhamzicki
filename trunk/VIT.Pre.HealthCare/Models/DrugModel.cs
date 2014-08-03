namespace VIT.Pre.HealthCare.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VIT.DataTransferObject.HealthCare;
    using System.Web.Mvc;

    public class DrugModel
    {
        public int Id { get; set; }

        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Không được để trống.")]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Phí")]
        public decimal Fee { get; set; }

        [Display(Name = "Sử dụng")]
        public bool Active { get; set; }

        [Display(Name = "Số lượng")]
        public int? Stock { get; set; }

        public IList<DrugDto> Drugs { get; set; }
    }
}
