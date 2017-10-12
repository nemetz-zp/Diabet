using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diabet.Models;

namespace Diabet.View
{
    public class YearMedicamentNeed
    {
        public Medicament MedName { get; set; }
        public Medicament OldMedNameValue { get; set; }
        public decimal Dozage { get; set; }
        public decimal OldDozageValue { get; set; }
        public int Days { get; set; }
        public int OldDaysValue { get; set; }
        public int PatientsNum { get; set; }
    }
}
