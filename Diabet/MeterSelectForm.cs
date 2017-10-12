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
    public partial class MeterSelectForm : Form
    {
        public Meter SelectedMeter = null;

        public MeterSelectForm()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;

            using(DiabetContext dc = new DiabetContext())
            {
                dataGridView1.DataSource = dc.Meters.ToList();
            }
        }

        public MeterSelectForm(MeterType mType)
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;

            using (DiabetContext dc = new DiabetContext())
            {
                dataGridView1.DataSource = dc.Meters.Where(r => (r.MType == mType)).ToList();
            }
        }

        private void chooseItem()
        {
            SelectedMeter = dataGridView1.SelectedRows[0].DataBoundItem as Meter;
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
