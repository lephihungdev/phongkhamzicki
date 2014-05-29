namespace VIT.DataTransferObject.HealthCare
{
    using System;

    [Serializable]
    public class IcdDto
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
