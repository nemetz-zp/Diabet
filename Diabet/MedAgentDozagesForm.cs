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
    public partial class MedAgentDozagesForm : Form
    {
        public MedAgentDozagesForm(List<AgentDozage> dozages)
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dozages;
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = e.RowIndex; i <= (e.RowIndex + e.RowCount - 1); i++)
                dataGridView1.Rows[i].Cells[0].Value = (i + 1).ToString();
        }
    }
}
