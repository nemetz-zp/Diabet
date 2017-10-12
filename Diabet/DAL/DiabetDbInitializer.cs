using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Diabet.Models;

namespace Diabet.DAL
{
    class DiabetDbInitializer : CreateDatabaseIfNotExists<DiabetContext>
    {
        protected override void Seed(DiabetContext context)
        {
            using(DiabetContext dc = new DiabetContext())
            {
                dc.Settings.Add(new ProgramSettings 
                {
                    DoctorLastName = "Падафа",
                    DoctorFirstName = "Валерія",
                    DoctorMiddleName = "Едуардівна",
                    DoctorPosition = "Лікар-ендокринолог",
                    HospitalFullName = "Комунальний заклад \"Запорізька центральна районна лікарня\" Запорізької районної ради",
                    HospitalAdress = "69089, Запорізька область, місто Запоріжжя, вулиця Лікарняна, будинок 18"
                });
                dc.SaveChanges();

                dc.Communes.Add(new Commune { Name = "Біленьківська" });
                dc.Communes.Add(new Commune { Name = "Долинська" });
                dc.Communes.Add(new Commune { Name = "Запорізький район" });
                dc.SaveChanges();

                MedicamentType mt1 = new MedicamentType { Name = "Таблетки" };
                MedicamentType mt2 = new MedicamentType { Name = "Шприц-ручка" };
                dc.MedicamentTypes.Add(mt1);
                dc.MedicamentTypes.Add(mt2);
                dc.MedicamentTypes.Add(new MedicamentType { Name = "Ампула" });
                dc.SaveChanges();

                Meter mmol = new Meter { MType = MeterType.Analize, Name = "ммоль/л" };
                Meter met1 = new Meter { MType = MeterType.MedicamentDozage, Name = "мг" };
                Meter met2 = new Meter { MType = MeterType.MedicamentDozage, Name = "мл" };
                dc.Meters.Add(met1);
                dc.Meters.Add(met2);
                dc.Meters.Add(mmol);
                dc.SaveChanges();

                dc.Analyze.Add(new Analyze { Name = "Рівень глюкози в крові", AnalizeMeter = mmol });
                dc.SaveChanges();

                MedicamentAgent ma1 = new MedicamentAgent { Name = "Метформін гідрохлорид" };
                MedicamentAgent ma2 = new MedicamentAgent { Name = "Глімепірид" };
                MedicamentAgent ma3 = new MedicamentAgent { Name = "Гліклазид" };
                MedicamentAgent ma4 = new MedicamentAgent { Name = "Гліпізид" };
                MedicamentAgent ma5 = new MedicamentAgent { Name = "Глюкагон" };
                dc.MedicamentGroups.Add(ma1);
                dc.MedicamentGroups.Add(ma2);
                dc.MedicamentGroups.Add(ma3);
                dc.MedicamentGroups.Add(ma4);
                dc.SaveChanges();

                MedicamentName mn1 = new MedicamentName { Name = "Глюкофаж" };
                MedicamentName mn2 = new MedicamentName { Name = "Діабетон МR" };
                MedicamentName mn3 = new MedicamentName { Name = "Діаглізид МR" };
                MedicamentName mn4 = new MedicamentName { Name = "Діапірид" };
                MedicamentName mn5 = new MedicamentName { Name = "Метамін SR" };
                MedicamentName mn6 = new MedicamentName { Name = "Метамін" };
                MedicamentName mn7 = new MedicamentName { Name = "Дуглимакс" };
                MedicamentName mn8 = new MedicamentName { Name = "Діаформін SR" };
                MedicamentName mn9 = new MedicamentName { Name = "Дибізид М" };
                MedicamentName mn10 = new MedicamentName { Name = "Амарил МСР" };
                MedicamentName mn11 = new MedicamentName { Name = "ГЛЮКАГЕН ГІПОКІТ" };
                dc.MedicamentNames.Add(mn1);
                dc.MedicamentNames.Add(mn2);
                dc.MedicamentNames.Add(mn3);
                dc.MedicamentNames.Add(mn4);
                dc.MedicamentNames.Add(mn5);
                dc.MedicamentNames.Add(mn6);
                dc.MedicamentNames.Add(mn7);
                dc.MedicamentNames.Add(mn8);
                dc.MedicamentNames.Add(mn9);
                dc.MedicamentNames.Add(mn10);
                dc.MedicamentNames.Add(mn11);
                dc.SaveChanges();

                // Глюкофаж
                Medicament m1 = new Medicament
                {
                    FullName = mn1,
                    NumInPack = 60,
                    MedicamentType = mt1
                };
                m1.AgentDozages.Add(new AgentDozage { Agent = ma1, DozageMeter = met1, Dozage = 500 });
                Medicament m2 = new Medicament
                {
                    FullName = mn1,
                    NumInPack = 60,
                    MedicamentType = mt1
                };
                m2.AgentDozages.Add(new AgentDozage { Agent = ma1, DozageMeter = met1, Dozage = 850 });
                Medicament m3 = new Medicament
                {
                    FullName = mn1,
                    NumInPack = 60,
                    MedicamentType = mt1
                };
                m3.AgentDozages.Add(new AgentDozage { Agent = ma1, DozageMeter = met1, Dozage = 1000 });

                // Дибетон
                Medicament m4 = new Medicament
                {
                    FullName = mn2,
                    NumInPack = 30,
                    MedicamentType = mt1
                };
                m4.AgentDozages.Add(new AgentDozage { Agent = ma3, DozageMeter = met1, Dozage = 60 });

                // Діаглізид МR
                Medicament m5 = new Medicament
                {
                    FullName = mn3,
                    NumInPack = 30,
                    MedicamentType = mt1
                };
                m5.AgentDozages.Add(new AgentDozage { Agent = ma3, Dozage = 60, DozageMeter = met1 });

                // Діапірид
                Medicament m6 = new Medicament
                {
                    FullName = mn4,
                    NumInPack = 30,
                    MedicamentType = mt1
                };
                m6.AgentDozages.Add(new AgentDozage { Agent = ma2, DozageMeter = met1, Dozage = 4 });

                // Метамін SR
                Medicament m7 = new Medicament
                {
                    FullName = mn5,
                    NumInPack = 30,
                    MedicamentType = mt1
                };
                m7.AgentDozages.Add(new AgentDozage { Agent = ma1, DozageMeter = met1, Dozage = 1000 });

                // Метамін
                Medicament m8 = new Medicament
                {
                    FullName = mn6,
                    NumInPack = 90,
                    MedicamentType = mt1
                };
                m8.AgentDozages.Add(new AgentDozage { Agent = ma1, DozageMeter = met1, Dozage = 1000 });

                // Дуглимакс 
                Medicament m9 = new Medicament
                {
                    FullName = mn7,
                    NumInPack = 30,
                    MedicamentType = mt1
                };
                m9.AgentDozages.Add(new AgentDozage { Agent = ma2, Dozage = 2, DozageMeter = met1 });
                m9.AgentDozages.Add(new AgentDozage { Agent = ma1, Dozage = 500, DozageMeter = met1 });

                // Діаформін SR
                Medicament m10 = new Medicament
                {
                    FullName = mn6,
                    NumInPack = 60,
                    MedicamentType = mt1
                };
                m10.AgentDozages.Add(new AgentDozage { Agent = ma1, DozageMeter = met1, Dozage = 1000 });

                // Дибізид М
                Medicament m11 = new Medicament
                {
                    FullName = mn9,
                    NumInPack = 60,
                    MedicamentType = mt1
                };
                m11.AgentDozages.Add(new AgentDozage { Agent = ma1, Dozage = 500, DozageMeter = met1 });
                m11.AgentDozages.Add(new AgentDozage { Agent = ma4, Dozage = 5, DozageMeter = met1 });

                // Амарил МСР
                Medicament m12 = new Medicament
                {
                    FullName = mn9,
                    NumInPack = 30,
                    MedicamentType = mt1
                };
                m12.AgentDozages.Add(new AgentDozage { Agent = ma2, Dozage = 2, DozageMeter = met1 });
                m12.AgentDozages.Add(new AgentDozage { Agent = ma1, Dozage = 500, DozageMeter = met1 });

                // ГЛЮКАГЕН ГІПОКІТ
                Medicament m13 = new Medicament
                {
                    FullName = mn11,
                    NumInPack = 60,
                    MedicamentType = mt2
                };
                m13.AgentDozages.Add(new AgentDozage { Agent = ma5, Dozage = 1, DozageMeter = met2 });

                dc.Medicaments.Add(m1);
                dc.Medicaments.Add(m2);
                dc.Medicaments.Add(m3);
                dc.Medicaments.Add(m4);
                dc.Medicaments.Add(m5);
                dc.Medicaments.Add(m6);
                dc.Medicaments.Add(m7);
                dc.Medicaments.Add(m8);
                dc.Medicaments.Add(m9);
                dc.Medicaments.Add(m10);
                dc.Medicaments.Add(m11);
                dc.Medicaments.Add(m12);
                dc.Medicaments.Add(m13);
                dc.SaveChanges();
            }
        }
    }
}
