//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VIT.Entity.HealthCare
{
    public partial class Charge
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int FacilityId { get; set; }
        public int UserId { get; set; }
        public Nullable<int> DoctorId { get; set; }
        public string Diagnostic { get; set; }
        public string ICDCode1 { get; set; }
        public string ICDCode2 { get; set; }
        public string ICDCode3 { get; set; }
        public string ICDCode4 { get; set; }
        public string CPTCode { get; set; }
        public int Quality { get; set; }
        public string Drugs { get; set; }
        public string Note { get; set; }
        public Nullable<System.DateTime> DateService { get; set; }
        public Nullable<System.DateTime> DateOnset { get; set; }
    
        public virtual Doctor Doctor { get; set; }
        public virtual Facility Facility { get; set; }
        public virtual Patient Patient { get; set; }
    }
    
}
