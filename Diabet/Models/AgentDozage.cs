using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diabet.Models
{
    public class AgentDozage
    {
        public int Id { get; set; }
        public decimal Dozage { get; set; }

        public int MedicamentAgentId { get; set; }
        public virtual MedicamentAgent Agent { get; set; }

        public int MedicamentId { get; set; }
        public virtual Medicament Medicament { get; set; }

        public int MeterId { get; set; }
        public virtual Meter DozageMeter { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            AgentDozage elem = obj as AgentDozage;

            if (elem == null) return false;

            return elem.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
