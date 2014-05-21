namespace VIT.DataTransferObject.HealthCare
{
    using System;

    [Serializable]
    public class ChargeDto
    {
        public int Id { get; set; }
        public string Diagnostic { get; set; }
        public string ICDCode1 { get; set; }
        public string ICDCode2 { get; set; }
        public string ICDCode3 { get; set; }
        public string ICDCode4 { get; set; }
        public string CPTCode { get; set; }
        public Nullable<int> Quality { get; set; }
        public string Drugs { get; set; }
        public string Note { get; set; }
    }
}
