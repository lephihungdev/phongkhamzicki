namespace VIT.DataTransferObject.HealthCare
{
    using System;
    using System.Collections.Generic;

    public class ChargeDto
    {
        public int Id { get; set; }
        public Nullable<int> DoctorId { get; set; }
        public string Diagnostic { get; set; }
        public string ICDCode1 { get; set; }
        public string ICDCode2 { get; set; }
        public string ICDCode3 { get; set; }
        public string ICDCode4 { get; set; }
        public string Treatments { get; set; }
        public int Days { get; set; }
        public string Note { get; set; }
        public Nullable<System.DateTime> DateService { get; set; }
        public Nullable<System.DateTime> DateOnset { get; set; }

        public string Drugs { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string DiagnosticDisplay { get; set; }
    }
}
