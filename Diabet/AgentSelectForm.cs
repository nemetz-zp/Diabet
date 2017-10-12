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

namespace Diabet
{
    public partial class AgentSelectForm : Form
    {
        public MedicamentAgent SelectedAgent = null;

        public AgentSelectForm(List<int> excludeAgents)
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;

            using (DiabetContext dc = new DiabetContext())
            {
                if(excludeAgents == null || excludeAgents.Count == 0)
                {
                    dataGridView1.DataSource = dc.MedicamentGroups.ToList();
                }
                else
                {
                    dataGridView1.DataSource = dc.MedicamentGroups.Where(p => !excludeAgents.Contains(p.Id)).ToList();
                }
            }
        }

        private void chooseItem()
        {
            SelectedAgent = dataGridView1.SelectedRows[0].DataBoundItem as MedicamentAgent;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chooseItem();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            chooseItem();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            button1.Enabled = (dataGridView1.SelectedRows.Count > 0);
        }
    }
}
