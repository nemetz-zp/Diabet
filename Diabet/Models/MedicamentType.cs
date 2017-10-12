using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diabet.Models
{
    /// <summary>
    /// Тип фасовки медикамента
    /// </summary>
    public class MedicamentType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Medicament> Medicaments { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public MedicamentType()
        {
            Medicaments = new List<Medicament>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            MedicamentType elem = obj as MedicamentType;

            if (elem == null) return false;

            return elem.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
