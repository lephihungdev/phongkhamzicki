﻿namespace VIT.DataTransferObject.HealthCare
{
    using System;

    [Serializable]
    public class DrugDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Fee { get; set; }
    }
}
