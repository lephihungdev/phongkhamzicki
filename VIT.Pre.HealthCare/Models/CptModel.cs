namespace VIT.Pre.HealthCare.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VIT.DataTransferObject.HealthCare;

    public class CptModel
    {
        public int Id { get; set; }

        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Sử dụng")]
        public bool Active { get; set; }

        [Display(Name = "Phí")]
        public decimal Fee { get; set; }

        [Display(Name = "Số lượng")]
        public int? Stock { get; set; }

        public IList<CptDto> Cpts { get; set; }
    }
}
