namespace VIT.DataTransferObject.HealthCare
{
    using System;

    [Serializable]
    public class UserLoginDto
    {
        public int UserId { get; set; }
        public int FacilityId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
}
