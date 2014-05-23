namespace VIT.DataTransferObject.HealthCare
{
    using System;

    [Serializable]
    public class ChargeDrugDto
    {
        public int Id { get; set; }
        public Nullable<int> ChargeId { get; set; }
        public int DrugId { get; set; }
        public Nullable<int> Quality { get; set; }
        public string Note { get; set; }
    }
}
