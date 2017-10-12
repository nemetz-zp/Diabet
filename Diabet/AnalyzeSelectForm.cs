using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Diabet.DAL;
using Diabet.Models;

namespace Diabet
{
    public partial class AnalyzeSelectForm : Form
    {
        public Analyze SelectedAnalyze = null;

        public AnalyzeSelectForm()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;

            using(DiabetContext dc = new DiabetContext())
            {
                dataGridView1.DataSource = dc.Analyze.Include("AnalizeMeter").ToList();
            }
        }

        private void chooseItem()
        {
            SelectedAnalyze = dataGridView1.SelectedRows[0].DataBoundItem as Analyze;
            Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            chooseItem();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chooseItem();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            button1.Enabled = (dataGridView1.SelectedRows.Count > 0);
        }

    }
}
