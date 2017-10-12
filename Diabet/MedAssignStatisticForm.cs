using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;
using Diabet.DAL;
using Diabet.View;
using Diabet.Models;
using System.Threading;

namespace Diabet
{
    public partial class MedAssignStatisticForm : Form
    {
        BindingList<MedicamentGroupPatients> medGoupsList;
        BindingList<Patient> patientsList;

        bool formIsLoaded;

        public MedAssignStatisticForm()
        {
            formIsLoaded = false;
            InitializeComponent();
            medGroupsTable.AutoGenerateColumns = patientsTable.AutoGenerateColumns = false;
            patientsList = new BindingList<Patient>();
            patientsTable.DataSource = patientsList;

            using(DiabetContext dc = new DiabetContext())
            {
                medGoupsList = new BindingList<MedicamentGroupPatients>(
                    dc.MedicamentAssigantions
                    .Include(t => t.AssignMedicament.FullName)
                    .Where(t => !t.Patient.IsDead)
                    .GroupBy(t => t.AssignMedicament.FullName)
                    .Select(t => new MedicamentGroupPatients { Name = t.Key, PatientsNum = t.Count() }).ToList());
                medGroupsTable.DataSource = medGoupsList;
            }

            formIsLoaded = true;
        }

        private void switchLoadingElements()
        {
            patientsTable.Enabled = !patientsTable.Enabled;
            waitImg.Visible = waitLabel.Visible = !waitLabel.Visible;
        }

        private void medGroupsTable_SelectionChanged(object sender, EventArgs e)
        {
            if (formIsLoaded)
            {
                if (medGroupsTable.SelectedRows.Count == 0)
                    return;

                MedicamentGroupPatients rec = medGroupsTable.SelectedRows[0].DataBoundItem as MedicamentGroupPatients;
                
                if(!backgroundWorker1.IsBusy)
                {
                    patientsList.Clear();
                    backgroundWorker1.RunWorkerAsync(rec);
                    switchLoadingElements();
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            MedicamentGroupPatients rec = e.Argument as MedicamentGroupPatients;
            using (DiabetContext dc = new DiabetContext())
            {
                patientsList = new BindingList<Patient>(dc.MedicamentAssigantions
                            .Where(t => (t.AssignMedicament.FullName.Id == rec.Name.Id) && (!t.Patient.IsDead))
                            .Select(t => t.Patient).Include(t => t.PatientCommune).ToList());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            patientsTable.DataSource = patientsList;
            patientsTable.Refresh();
            switchLoadingElements();
        }

        private void patientsTable_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = e.RowIndex; i <= (e.RowIndex + e.RowCount - 1); i++)
                patientsTable.Rows[i].Cells[0].Value = (i + 1).ToString();
        }

        private void patientsTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Patient selectedRecord = patientsTable.SelectedRows[0].DataBoundItem as Patient;
            PatientInfoForm pif = new PatientInfoForm(selectedRecord.Id);
            pif.ShowDialog();

            if (pif.Result != null)
            {
                selectedRecord.CopyPatient(pif.Result);
                patientsTable.Refresh();
            }
        }


    }
}
