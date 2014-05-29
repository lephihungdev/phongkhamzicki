﻿namespace VIT.DataTransferObject.HealthCare
{
    using System;

    [Serializable]
    public class CptDto
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Fee { get; set; }
        public bool Active { get; set; }
    }
}
