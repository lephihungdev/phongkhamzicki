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
    public partial class CPT
    {
        public int Id { get; set; }
        public int FacilityId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Fee { get; set; }
        public bool Active { get; set; }
    
        public virtual Facility Facility { get; set; }
    }
    
}
