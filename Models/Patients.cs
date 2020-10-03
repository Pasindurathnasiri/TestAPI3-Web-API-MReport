using System;
using System.Collections.Generic;

namespace TestAPI3.Models
{
    public partial class Patients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
    }
}
