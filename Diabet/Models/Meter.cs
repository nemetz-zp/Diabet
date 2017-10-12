using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diabet.Models
{
    public enum MeterType
    {
        MedicamentDozage,
        Analize
    }

    /// <summary>
    /// Единица измерения
    /// </summary>
    public class Meter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MeterType MType { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            Meter elem = obj as Meter;

            if (elem == null) return false;

            return elem.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
