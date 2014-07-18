namespace VIT.DataTransferObject.HealthCare
{
    using System;
    using System.Collections.Generic;

    public class ChargeReportDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Diagnostic { get; set; }
        public string Treatments { get; set; }
        public Nullable<System.DateTime> DateService { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public decimal? Price { get; set; }
    }
}
