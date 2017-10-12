using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diabet.Models
{
    public class MedicamentName : IEquatable<MedicamentName>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            MedicamentName elem = obj as MedicamentName;

            if (elem == null) return false;

            return elem.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        bool IEquatable<MedicamentName>.Equals(MedicamentName other)
        {
            if (other == null)
                return false;

            return (Id == other.Id);
        }
    }
}
