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
    public partial class ChargeInstrument
    {
        public int Id { get; set; }
        public Nullable<int> ChargeId { get; set; }
        public int PatientId { get; set; }
        public int InstrumentsId { get; set; }
        public Nullable<int> Quality { get; set; }
        public string Note { get; set; }
    
        public virtual Charge Charge { get; set; }
        public virtual Instrument Instrument { get; set; }
        public virtual Patient Patient { get; set; }
    }
    
}
