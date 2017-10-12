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
using System.Data.Entity;
using System.Threading.Tasks;
using Diabet.View;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace Diabet
{
    public partial class AnalizeListForm : Form
    {
        BindingList<Analyze> analizesList;
        Meter defaultMeter;

        bool formIsLoaded;

        public AnalizeListForm()
        {
            formIsLoaded = false;
            InitializeComponent();
            analizesTable.AutoGenerateColumns = false;

            using(DiabetContext dc = new DiabetContext())
            {
                analizesList = new BindingList<Analyze>(dc.Analyze.Include(t => t.AnalizeMeter).ToList());
                analizesTable.DataSource = analizesList;

                defaultMeter = dc.Meters.Where(t => t.MType == MeterType.Analize).First();
            }

            formIsLoaded = true;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            deleteAnalizeLink.Enabled = (analizesTable.SelectedCells.Count > 0);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Analyze newAnalize = new Analyze { Name = "- НОВИЙ АНАЛІЗ -", AnalizeMeter = defaultMeter };
            analizesList.Add(newAnalize);
            analizesTable.Refresh();

            new Task(() => 
            {
                using(DiabetContext dc = new DiabetContext())
                {
                    dc.Meters.Attach(newAnalize.AnalizeMeter);
                    dc.Analyze.Add(newAnalize);
                    dc.SaveChanges();
                }
            }).Start();
        }

        private bool TryDelete(Analyze item)
        {
            try
            {
                using(DiabetContext dc = new DiabetContext())
                {
                    dc.Analyze.Attach(item);
                    dc.Entry<Analyze>(item).State = EntityState.Deleted;
                    dc.SaveChanges();
                }

                return true;
            }
            catch(DbUpdateException ex)
            {
                var sqlExc = ex.GetBaseException() as SqlException;
                if (sqlExc != null && sqlExc.Number == 547)
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Analyze selectedItem = analizesTable.Rows[analizesTable.SelectedCells[0].RowIndex].DataBoundItem as Analyze;

            if (TryDelete(selectedItem))
            {
                analizesList.Remove(selectedItem);
                analizesTable.Refresh();

                new Task(() =>
                {
                    using (DiabetContext dc = new DiabetContext())
                    {
                        dc.Meters.Attach(selectedItem.AnalizeMeter);
                        dc.Entry<Analyze>(selectedItem).State = EntityState.Deleted;
                        dc.SaveChanges();
                    }
                }).Start(); 
            }
            else
            {
                Notificator.ShowError("Неможливо видалити аналіз так як на нього є посилання в записах пацієнтів");
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (formIsLoaded)
            {
                if (e.RowIndex > 0 && e.ColumnIndex == 0)
                {
                    Analyze selectedItem = analizesTable.Rows[e.RowIndex].DataBoundItem as Analyze;

                    new Task(() =>
                    {
                        using (DiabetContext dc = new DiabetContext())
                        {
                            dc.Meters.Attach(selectedItem.AnalizeMeter);
                            dc.Entry<Analyze>(selectedItem).State = EntityState.Modified;
                            dc.SaveChanges();
                        }
                    }).Start();
                } 
            }
        }

        private void analizesTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(formIsLoaded)
            {
                if(e.RowIndex > 0 && e.ColumnIndex == 1)
                {
                    Analyze selectedItem = analizesTable.Rows[e.RowIndex].DataBoundItem as Analyze;
                    MeterSelectForm mf = new MeterSelectForm(MeterType.Analize);
                    mf.ShowDialog();

                    if(mf.SelectedMeter != null)
                    {
                        selectedItem.AnalizeMeter = mf.SelectedMeter;
                        selectedItem.MeterId = mf.SelectedMeter.Id;
                        analizesTable.Refresh();
                    }
                }
            }
        }
    }
}
