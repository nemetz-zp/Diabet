using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diabet.Models
{
    public class PatientAnalyze
    {
        public long Id { get; set; }

        public DateTime AnalizeDate { get; set; }

        public int AnalyzeId { get; set; }
        public virtual Analyze Analyze { get; set; }

        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        
        public decimal Value { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            PatientAnalyze elem = obj as PatientAnalyze;

            if (elem == null) return false;

            return elem.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
