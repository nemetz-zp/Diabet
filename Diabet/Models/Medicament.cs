using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Diabet.Models
{
    /// <summary>
    /// Медикамент
    /// </summary>
    public class Medicament
    {
        public int Id { get; set; }

        public int MedicamentNameId { get; set; }
        public virtual MedicamentName FullName { get; set; }

        public int NumInPack { get; set; }

        public decimal Price { get; set; }

        public decimal MinNumOnHand { get; set; }

        public int MedicamentTypeId { get; set; }
        public virtual MedicamentType MedicamentType { get; set; }

        public virtual ICollection<AgentDozage> AgentDozages { get; set; }

        public Medicament()
        {
            AgentDozages = new List<AgentDozage>();
        }

        [NotMapped]
        public string LongName
        {
            get
            {
                // Получаем список дозировок действующих веществ
                StringBuilder sb = new StringBuilder();
                foreach (var item in AgentDozages)
                    sb.Append(item.Dozage + "" + item.DozageMeter + "/");
                string agentsDoz = sb.ToString();

                if(sb.Length > 0)
                    agentsDoz = agentsDoz.Substring(0, agentsDoz.Length - 1);

                return string.Format("{0} {1} №{2} {3}", FullName, agentsDoz, NumInPack, MedicamentType);
            }
        }

        [NotMapped]
        public string ListOfAgents
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in AgentDozages)
                    sb.Append(item.Agent + "\n");

                if (sb.ToString().Length > 0)
                    return sb.ToString().Substring(0, sb.ToString().Length - 1);
                else
                    return sb.ToString();
            }
        }

        public override string ToString()
        {
            return LongName;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            Medicament elem = obj as Medicament;

            if (elem == null) return false;

            return elem.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
