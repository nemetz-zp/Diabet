using Diabet.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Diabet.Models;
using Diabet.Helpers;
using Diabet.View;
using System.Data.Entity;

namespace Diabet
{
    public partial class MedAssignChangeForm : Form
    {
        BindingList<YearMedicamentNeed> medicamentsList;

        bool formIsLoaded;

        public MedAssignChangeForm()
        {
            formIsLoaded = false;
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            medicamentsList = new BindingList<YearMedicamentNeed>();

            using (DiabetContext dc = new DiabetContext())
            {
                List<Commune> communesList = dc.Communes.ToList();
                communesList.Insert(0, new Commune { Id = -1, Name = "Усі громади" });

                comboBox1.DataSource = communesList;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "Id";
            }

            updateList();
            formIsLoaded = true;
        }

        private void updateList()
        {
            switchWaitingElements();
            medicamentsList.Clear();
            backgroundWorker1.RunWorkerAsync(comboBox1.SelectedValue);
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (formIsLoaded)
            {
                updateList();
            }
        }

        private void switchWaitingElements()
        {
            waitImg.Visible = waitLabel.Visible = !(waitLabel.Visible);
            dataGridView1.Enabled = !(dataGridView1.Enabled);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int selectedCommuneId = Convert.ToInt32(e.Argument);

            using(DiabetContext dc = new DiabetContext())
	        {
		        var allAssignments = dc.MedicamentAssigantions
                          .Include(t => t.AssignMedicament)
                          .Include(t => t.AssignMedicament.FullName)
                          .Include(t => t.AssignMedicament.AgentDozages)
                          .Include(t => t.AssignMedicament.AgentDozages.Select(m => m.Agent))
                          .Include(t => t.AssignMedicament.AgentDozages.Select(m => m.DozageMeter))
                          .Include(t => t.AssignMedicament.MedicamentType)
                          .ToList();

                if(selectedCommuneId > 0)
                {
                    allAssignments = allAssignments.Where(t => t.Patient.CommuneId == selectedCommuneId).ToList();
                }

                medicamentsList = new BindingList<YearMedicamentNeed>(allAssignments.GroupBy(t => new { t.AssignMedicament, t.Dozage, t.Days })
                          .Select(t => new YearMedicamentNeed
                          {
                              MedName = t.Key.AssignMedicament,
                              Dozage = t.Key.Dozage,
                              OldDozageValue = t.Key.Dozage,
                              Days = t.Key.Days,
                              OldDaysValue = t.Key.Days,
                              PatientsNum = t.Count()
                          }).ToList());
	        }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switchWaitingElements();
            dataGridView1.DataSource = medicamentsList;
            dataGridView1.Refresh();
            button1.Enabled = (medicamentsList.Count > 0);
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = e.RowIndex; i <= (e.RowIndex + e.RowCount - 1); i++)
                dataGridView1.Rows[i].Cells[0].Value = (i + 1).ToString();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                YearMedicamentNeed selectedItem = dataGridView1.Rows[e.RowIndex].DataBoundItem as YearMedicamentNeed;
                
                MedicamentSelectForm mf = new MedicamentSelectForm(new List<int> { selectedItem.MedName.Id }, false);
                mf.ShowDialog();

                if(mf.SelectedMedicament != null)
                {
                    selectedItem.OldMedNameValue = selectedItem.MedName;
                    selectedItem.MedName = mf.SelectedMedicament;
                    dataGridView1.Refresh();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int selectedCommuneId = Convert.ToInt32(comboBox1.SelectedValue);

            if (medicamentsList.Count == 0)
                return;

            try
            {
                this.Enabled = false;

                using (DiabetContext dc = new DiabetContext())
                {
                    var listOfAssignments = dc.MedicamentAssigantions.ToList();
                    if (selectedCommuneId > 0)
                    {
                        listOfAssignments = listOfAssignments.Where(t => t.Patient.CommuneId == selectedCommuneId).ToList();
                    }

                    foreach (var element in medicamentsList)
                    {
                        Medicament medicamentForSearch;
                        if (element.OldMedNameValue != null)
                        {
                            medicamentForSearch = element.OldMedNameValue;
                        }
                        else
                        {
                            medicamentForSearch = element.MedName;
                        }

                        var searchList = listOfAssignments.Where(t => (t.AssignMedicament.Id == medicamentForSearch.Id)
                            && (t.Dozage == element.OldDozageValue)
                            && (t.Days == element.OldDaysValue)).ToList();

                        foreach (var assMedItem in searchList)
                        {
                            assMedItem.MedicamentId = element.MedName.Id;
                            assMedItem.Dozage = element.Dozage;
                            assMedItem.Days = element.Days;
                            dc.Entry<MedicamentAssignation>(assMedItem).State = EntityState.Modified;
                            dc.SaveChanges();
                        }
                    }
                }
                Notificator.ShowInfo("Зміни успішно внесено!");
            }
            catch (Exception ex)
            {
                Notificator.ShowError(ex.Message);
            }
            finally
            {
                this.Enabled = true;
            }

        }

    }
}