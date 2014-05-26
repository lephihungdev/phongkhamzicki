namespace VIT.Pre.HealthCare.Models
{
    using System.Collections.Generic;

    using VIT.DataTransferObject.HealthCare;

    public class ChargesPrintModel
    {
        public int FacilityId { get; set; }
        public string FacilityName { get; set; }

        public IList<ChargeDto> ListCharges { get; set; }
    }
}
