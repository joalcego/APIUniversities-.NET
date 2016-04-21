using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIUniversities.Models
{
    public class UniversityModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }

        public List<CareerModel> Careers { get; set; }
    }
}