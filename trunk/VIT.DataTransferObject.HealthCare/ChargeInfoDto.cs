namespace VIT.DataTransferObject.HealthCare
{
    using System;
    using System.Collections.Generic;

    public class ChargeInfoDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string Diagnostic { get; set; }
        public string Treatments { get; set; }
        public int Days { get; set; }
        public string Note { get; set; }
        public Nullable<System.DateTime> DateService { get; set; }
    }
}
