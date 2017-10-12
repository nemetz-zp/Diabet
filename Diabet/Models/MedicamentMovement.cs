using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Diabet.Models
{
    /// <summary>
    /// Выдача медикамента
    /// </summary>
    public class MedicamentMovement
    {
        public long Id { get; set; }

        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        public int MedicamentId { get; set; }
        public virtual Medicament Medicament { get; set; }

        public int MedicamentNum { get; set; }

        public decimal DayDozage { get; set; }
        
        [NotMapped]
        public string Period
        {
            get
            {
                if (Medicament == null)
                    return "";

                decimal result = 0;
                if (DayDozage == 0)
                    result = 0;
                else
                    result = Math.Floor(MedicamentNum / DayDozage);

                return string.Format("{0} днів\n(при добовій дозі {1} {2})", result, DayDozage, Medicament.MedicamentType);
            }
        }

        public DateTime MovementOperationDate { get; set; }

        public int MovementOperationNum { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            MedicamentMovement elem = obj as MedicamentMovement;

            if (elem == null) return false;

            return elem.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
