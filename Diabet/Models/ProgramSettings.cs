using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diabet.Models
{
    public class ProgramSettings
    {
        public int Id { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string DoctorMiddleName { get; set; }
        public string DoctorPosition { get; set; }

        public string HospitalFullName { get; set; }
        public string HospitalAdress { get; set; }
    }
}
