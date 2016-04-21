using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIUniversities.Models
{
    public class CareerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UniversityCode { get; set; }

        public UniversityModel University { get; set; }
    }
}
