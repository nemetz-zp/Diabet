using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diabet.Models
{
    /// <summary>
    /// Группа медикаментов
    /// </summary>
    public class MedicamentAgent
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Medicament> Medicaments { get; set; }

        public MedicamentAgent()
        {
            Medicaments = new List<Medicament>();
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            MedicamentAgent elem = obj as MedicamentAgent;

            if (elem == null) return false;

            return elem.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
