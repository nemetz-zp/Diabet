using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Diabet.Models
{
    /// <summary>
    /// Анализ
    /// </summary>
    public class Analyze
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MeterId { get; set; }

        // Единица измерения анализа
        public virtual Meter AnalizeMeter { get; set; }

        [NotMapped]
        public string LongName
        {
            get
            {
                return string.Format("{0} ({1})", Name, AnalizeMeter);
            }
        }

        public override string ToString()
        {
            return LongName;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            Analyze elem = obj as Analyze;

            if (elem == null) return false;

            return elem.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
