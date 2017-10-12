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
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using Diabet.View;

namespace Diabet
{
    public partial class MeterForm : Form
    {
        BindingList<MedicamentType> packageMeterList;
        BindingList<Meter> medDozageMeterList;
        BindingList<Meter> analizeMeterList;

        public MeterForm()
        {
            InitializeComponent();
            medDozageTable.AutoGenerateColumns 
                = medPackageTable.AutoGenerateColumns = analizeMeterTable.AutoGenerateColumns = false;

            using(DiabetContext dc = new DiabetContext())
            {
                packageMeterList = new BindingList<MedicamentType>(dc.MedicamentTypes.ToList());
                medDozageMeterList = new BindingList<Meter>(dc.Meters.Where(t => t.MType == MeterType.MedicamentDozage).ToList());
                analizeMeterList = new BindingList<Meter>(dc.Meters.Where(t => t.MType == MeterType.Analize).ToList());

                medPackageTable.DataSource = packageMeterList;
                medDozageTable.DataSource = medDozageMeterList;
                analizeMeterTable.DataSource = analizeMeterList;
            }
        }

        private void analizeMeterList_SelectionChanged(object sender, EventArgs e)
        {
            deleteAnalizeDozageButton.Enabled = (analizeMeterTable.SelectedCells.Count > 0);
        }

        private void medPackageList_SelectionChanged(object sender, EventArgs e)
        {
            deletePackageButton.Enabled = (medPackageTable.SelectedCells.Count > 0);
        }

        private void medDozageTable_SelectionChanged(object sender, EventArgs e)
        {
            deleteDozageButton.Enabled = (medDozageTable.SelectedCells.Count > 0);
        }

        private void addDozageButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Meter newMeter = new Meter { Name = " - НОВА ОДИНИЦЯ - " };
            medDozageMeterList.Add(newMeter);
            medDozageTable.Refresh();
            new Task(() =>
            {
                using (DiabetContext dc = new DiabetContext())
                {
                    dc.Meters.Add(newMeter);
                    dc.SaveChanges();
                }
            }).Start();
        }

        private void addPackageButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MedicamentType newType = new MedicamentType { Name = " - НОВА ОДИНИЦЯ - " };
            packageMeterList.Add(newType);
            medPackageTable.Refresh();
            new Task(() =>
            {
                using (DiabetContext dc = new DiabetContext())
                {
                    dc.MedicamentTypes.Add(newType);
                    dc.SaveChanges();
                }
            }).Start();
        }

        private void addAnalizeDozageButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Meter newMeter = new Meter { Name = " - НОВА ОДИНИЦЯ - " };
            analizeMeterList.Add(newMeter);
            analizeMeterTable.Refresh();
            new Task(() =>
            {
                using (DiabetContext dc = new DiabetContext())
                {
                    dc.Meters.Add(newMeter);
                    dc.SaveChanges();
                }
            }).Start();
        }

        private void medDozageTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            new Task(() => 
            {
                Meter selectedItem = medDozageTable.Rows[medDozageTable.SelectedCells[0].RowIndex].DataBoundItem as Meter;
                using(DiabetContext dc = new DiabetContext())
                {
                    dc.Meters.Attach(selectedItem);
                    dc.Entry<Meter>(selectedItem).State = System.Data.Entity.EntityState.Modified;
                    dc.SaveChanges();
                }
            }).Start();
        }

        private void medPackageTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            new Task(() =>
            {
                MedicamentType selectedItem = medPackageTable.Rows[medPackageTable.SelectedCells[0].RowIndex].DataBoundItem as MedicamentType;
                using (DiabetContext dc = new DiabetContext())
                {
                    dc.MedicamentTypes.Attach(selectedItem);
                    dc.Entry<MedicamentType>(selectedItem).State = System.Data.Entity.EntityState.Modified;
                    dc.SaveChanges();
                }
            }).Start();
        }

        private void analizeMeterTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            new Task(() =>
            {
                Meter selectedItem = analizeMeterTable.Rows[analizeMeterTable.SelectedCells[0].RowIndex].DataBoundItem as Meter;
                using (DiabetContext dc = new DiabetContext())
                {
                    dc.Meters.Attach(selectedItem);
                    dc.Entry<Meter>(selectedItem).State = System.Data.Entity.EntityState.Modified;
                    dc.SaveChanges();
                }
            }).Start();
        }

        private void deleteDozageButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Meter selectedItem = medDozageTable.Rows[medDozageTable.SelectedCells[0].RowIndex].DataBoundItem as Meter;
            
            if(!TryDeleteRecord(selectedItem))
            {
                Notificator.ShowError("Неможливо видалити цю одиницю виміру так як вона використовується в інших записах БД");
                return;
            }

            medDozageTable.Rows.RemoveAt(medDozageTable.SelectedCells[0].RowIndex);
        }


        private void deleteAnalizeDozageButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Meter selectedItem = analizeMeterTable.Rows[analizeMeterTable.SelectedCells[0].RowIndex].DataBoundItem as Meter;

            if (!TryDeleteRecord(selectedItem))
            {
                Notificator.ShowError("Неможливо видалити цю одиницю виміру так як вона використовується в інших записах БД");
                return;
            }

            analizeMeterTable.Rows.RemoveAt(analizeMeterTable.SelectedCells[0].RowIndex);
        }

        private void deletePackageButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MedicamentType selectedItem = medPackageTable.Rows[medPackageTable.SelectedCells[0].RowIndex].DataBoundItem as MedicamentType;

            if (!TryDeleteRecord(selectedItem))
            {
                Notificator.ShowError("Неможливо видалити цю одиницю виміру так як вона використовується в інших записах БД");
                return;
            }

            medPackageTable.Rows.RemoveAt(medPackageTable.SelectedCells[0].RowIndex);
        }

        private bool TryDeleteRecord(object obj)
        {
            try
            { 
                using(DiabetContext dc = new DiabetContext())
                {
                    if(obj is Meter)
                    {
                        Meter entity = obj as Meter;
                        dc.Meters.Attach(entity);
                        dc.Meters.Remove(entity);
                    }
                    else if (obj is MedicamentType)
                    {
                        MedicamentType entity = obj as MedicamentType;
                        dc.MedicamentTypes.Attach(entity);
                        dc.MedicamentTypes.Remove(entity);
                    }
                    else
                    {
                        throw new Exception("UKNOWN OBJECT TO DELETE!");
                    }
                    dc.SaveChanges();
                }

                return true;
            }
            catch (DbUpdateException ex)
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
    }
}
