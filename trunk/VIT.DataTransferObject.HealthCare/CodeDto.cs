namespace VIT.DataTransferObject.HealthCare
{
    using System;

    [Serializable]
    public class CodeDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
