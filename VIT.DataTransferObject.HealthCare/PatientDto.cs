﻿namespace VIT.DataTransferObject.HealthCare
{
    using System;

    [Serializable]
    public class PatientDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<bool> Sex { get; set; }
        public int? BirthYear { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
