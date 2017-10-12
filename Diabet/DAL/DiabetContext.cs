using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Diabet.Models;

namespace Diabet.DAL
{
    public class DiabetContext : DbContext
    {
        public DiabetContext() : base("DefaultConnectionStr")
        {

        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<PatientHistoryRecord> PatientHistoryRecords { get; set; }
        public DbSet<MedicamentMovement> MedicamentMovements { get; set; }
        public DbSet<MedicamentAgent> MedicamentGroups { get; set; }
        public DbSet<Meter> Meters { get; set; }
        public DbSet<MedicamentType> MedicamentTypes { get; set; }
        public DbSet<PatientAnalyze> PatientAnalyzes { get; set; }
        public DbSet<Analyze> Analyze { get; set; }
        public DbSet<Commune> Communes { get; set; }
        public DbSet<MedicamentAssignation> MedicamentAssigantions { get; set; }
        public DbSet<MedicamentName> MedicamentNames { get; set; }

        public DbSet<ProgramSettings> Settings { get; set; }

        public DbSet<AgentDozage> AgentDozages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Patient>()
                .HasMany<MedicamentAssignation>(t => t.Medicaments)
                .WithRequired().WillCascadeOnDelete();
            modelBuilder.Entity<Patient>()
                .HasMany<PatientHistoryRecord>(t => t.HistoryRecords)
                .WithRequired().WillCascadeOnDelete();
            modelBuilder.Entity<Patient>()
                .HasMany<PatientAnalyze>(t => t.PatientAnalyzes)
                .WithRequired().WillCascadeOnDelete();
            modelBuilder.Entity<Patient>()
                .HasMany<MedicamentMovement>(t => t.MedMovements)
                .WithRequired().WillCascadeOnDelete();
        }
    }
}
