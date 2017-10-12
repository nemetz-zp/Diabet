using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Diabet.Models
{
    public class MedicamentAssignation
    {
        public long Id { get; set; }
        public int MedicamentId { get; set; }
        public virtual Medicament AssignMedicament { get; set; }

        public decimal Dozage { get; set; }
        public int Days { get; set; }

        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        [NotMapped]
        public decimal YearDozage
        {
            get
            {
                return Dozage * Days;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            MedicamentAssignation elem = obj as MedicamentAssignation;

            if (elem == null) return false;

            return elem.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
