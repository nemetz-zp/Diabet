using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Diabet.View;
using Diabet.DAL;
using System.Data.Entity;
using Diabet.Models;
using Excel = Microsoft.Office.Interop.Excel;
using Diabet.Helpers;

namespace Diabet
{
    public partial class YearMedicamentsNeedForm : Form
    {
        BindingList<YearMedicamentNeed> medicamentsList;

        public YearMedicamentsNeedForm()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;

            numericUpDown1.Value = DateTime.Now.Year;

            using(DiabetContext dc = new DiabetContext())
            {
                medicamentsList = new BindingList<YearMedicamentNeed>(dc.MedicamentAssigantions
                    .Include(t => t.AssignMedicament)
                    .Include(t => t.AssignMedicament.FullName)
                    .Include(t => t.AssignMedicament.AgentDozages)
                    .Include(t => t.AssignMedicament.AgentDozages.Select(m => m.Agent))
                    .Include(t => t.AssignMedicament.AgentDozages.Select(m => m.DozageMeter))
                    .Include(t => t.AssignMedicament.MedicamentType)
                    .ToList()
                    .Where(t => !t.Patient.IsDead)
                    .GroupBy(t => new { t.AssignMedicament, t.Dozage, t.Days })
                    .Select(t => new YearMedicamentNeed 
                    { 
                        MedName = t.Key.AssignMedicament, Dozage = t.Key.Dozage, Days = t.Key.Days, PatientsNum = t.Count()
                    }).ToList());
                dataGridView1.DataSource = medicamentsList;
            }

            button1.Enabled = (medicamentsList.Count > 0);
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = e.RowIndex; i <= (e.RowIndex + e.RowCount - 1); i++)
                dataGridView1.Rows[i].Cells[0].Value = (i + 1).ToString();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 1 && e.RowIndex > 0)
            {
                YearMedicamentNeed selectedItem = dataGridView1.Rows[e.RowIndex].DataBoundItem as YearMedicamentNeed;

                MedicamentSelectForm msf = new MedicamentSelectForm(new List<int> { selectedItem.MedName.Id }, false);
                msf.ShowDialog();
                if(msf.SelectedMedicament != null)
                {
                    selectedItem.OldMedNameValue = selectedItem.MedName;
                    selectedItem.MedName = msf.SelectedMedicament;
                    dataGridView1.Refresh();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExcelMedicamentNeedForm enf = new ExcelMedicamentNeedForm(Convert.ToInt32(numericUpDown1.Value), medicamentsList.ToList());
            enf.ShowDialog();
        }
    }
}
