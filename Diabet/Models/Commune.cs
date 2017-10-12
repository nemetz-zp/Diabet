using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diabet.Models
{
    public class Commune
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

            Commune elem = obj as Commune;

            if (elem == null) return false;

            return elem.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
