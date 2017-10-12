using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Diabet.Models;
using Diabet.DAL;
using System.Data.Entity;

namespace Diabet
{
    public partial class MedicamentSelectForm : Form
    {
        public Medicament SelectedMedicament = null;

        public MedicamentSelectForm(List<int> medFilter, bool isIncludeSearch)
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;

            using (DiabetContext dc = new DiabetContext())
            {
                var medList = dc.Medicaments
                    .Include(p => p.AgentDozages)
                    .Include(p => p.AgentDozages.Select(k => k.Agent))
                    .Include(p => p.AgentDozages.Select(k => k.DozageMeter))
                    .Include(p => p.FullName)
                    .Include(p => p.MedicamentType)
                    .Where(p => medFilter.Contains(p.Id) == isIncludeSearch).ToList();
                dataGridView1.DataSource = medList;
            }
        }



        private void chooseItem()
        {
            SelectedMedicament = dataGridView1.SelectedRows[0].DataBoundItem as Medicament;
            Close();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            chooseItem();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            button1.Enabled = (dataGridView1.SelectedRows.Count > 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chooseItem();
        }
    }
}
