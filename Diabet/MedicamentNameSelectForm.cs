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

namespace Diabet
{
    public partial class MedicamentNameSelectForm : Form
    {
        public List<MedicamentName> SelectedMedicaments
        {
            get
            {
                return selectedMedicaments;
            }
        }

        private List<MedicamentName> selectedMedicaments;

        public MedicamentNameSelectForm()
        {
            InitializeComponent();
            medicamentTable.AutoGenerateColumns = false;
            selectedMedicaments = new List<MedicamentName>();

            using(DiabetContext dc = new DiabetContext())
            {
                medicamentTable.DataSource = dc.MedicamentNames.ToList();
            }
        }

        private void medicamentTable_SelectionChanged(object sender, EventArgs e)
        {
            button1.Enabled = (medicamentTable.SelectedCells.Count > 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int selectedRowsNum = medicamentTable.SelectedCells.Count;
            for(int i = 0; i < selectedRowsNum; i++)
            {
                selectedMedicaments.Add(medicamentTable.Rows[medicamentTable.SelectedCells[i].RowIndex].DataBoundItem 
                    as MedicamentName);
            }

            Close();
        }

        private void medicamentTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedMedicaments.Add(medicamentTable.Rows[e.RowIndex].DataBoundItem as MedicamentName);
            Close();
        }
    }
}
