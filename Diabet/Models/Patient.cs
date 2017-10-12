using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Diabet.Models
{
    public enum Sex
    {
        Male,
        Female
    }

    /// <summary>
    /// Тип диабета
    /// </summary>
    public enum DiabetType
    {
        FirstType,
        SecondType,
        Gestac,
        Secondary
    }

    /// <summary>
    /// Пациент
    /// </summary>
    public class Patient
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string Adress { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsDead { get; set; }
        public DateTime? DeathDate { get; set; }

        public int CommuneId { get; set; }
        public virtual Commune PatientCommune { get; set; }

        public DiabetType DType { get; set; }

        public Sex Sex { get; set; }
        public string PhotoFile { get; set; }

        public ICollection<MedicamentMovement> MedMovements { get; set; }
        public virtual ICollection<PatientAnalyze> PatientAnalyzes { get; set; }

        public virtual ICollection<MedicamentAssignation> Medicaments { get; set; }

        public virtual ICollection<PatientHistoryRecord> HistoryRecords { get; set; }

        public string OtherInfo { get; set; }

        public Patient()
        {
            Medicaments = new List<MedicamentAssignation>();
            HistoryRecords = new List<PatientHistoryRecord>();
            MedMovements = new List<MedicamentMovement>();
            PatientAnalyzes = new List<PatientAnalyze>();
        }

        [NotMapped]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1} {2}", LastName, FirstName, MiddleName);
            }
        }

        [NotMapped]
        public string DiabetTypeStr
        {
            get
            {
                string result = string.Empty;
                switch(DType)
                {
                    case DiabetType.FirstType:
                        result = "I тип";
                        break;
                    case DiabetType.SecondType:
                        result = "II тип";
                        break;
                    case DiabetType.Gestac:
                        result = "Гестаційний";
                        break;
                    case DiabetType.Secondary:
                        result = "Вторинний";
                        break;
                }
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            Patient elem = obj as Patient;

            if (elem == null) return false;

            return elem.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public void CopyPatient(Patient p)
        {
            this.Adress = p.Adress;
            this.BirthDate = p.BirthDate;
            this.CommuneId = p.CommuneId;
            this.DeathDate = p.DeathDate;
            this.DType = p.DType;
            this.FirstName = p.FirstName;
            this.IsDead = p.IsDead;
            this.LastName = p.LastName;
            this.MiddleName = p.MiddleName;
            this.OtherInfo = p.OtherInfo;
            this.PatientCommune = p.PatientCommune;
            this.Sex = p.Sex;
            this.PhotoFile = p.PhotoFile;
        }
    }
}
