namespace VIT.DataTransferObject.HealthCare
{
    using System;

    [Serializable]
    public class ChargeInstrumentDto
    {
        public int Id { get; set; }
        public Nullable<int> ChargeId { get; set; }
        public int InstrumentId { get; set; }
        public string InstrumentName { get; set; }
        public int PatientId { get; set; }
        public Nullable<int> Quality { get; set; }
        public string Note { get; set; }
    }
}
