using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diabet.Models
{
    /// <summary>
    /// История изменений по пациенту
    /// </summary>
    public class PatientHistoryRecord
    {
        public int Id { get; set; }

        public DateTime RecordDate { get; set; }
        
        public virtual Patient Patient { get; set; }
        public int PatientId { get; set; }

        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            PatientHistoryRecord elem = obj as PatientHistoryRecord;

            if (elem == null) return false;

            return elem.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
