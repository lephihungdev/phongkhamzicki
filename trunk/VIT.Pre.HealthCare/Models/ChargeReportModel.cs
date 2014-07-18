namespace VIT.Pre.HealthCare.Models
{
    using System;
    using System.Collections.Generic;
    using VIT.DataTransferObject.HealthCare;
    using System.ComponentModel.DataAnnotations;

    public class ChargeReportModel
    {
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime FromDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ToDate { get; set; }

        public IList<ChargeReportDto> Charges { get; set; }
    }
}
