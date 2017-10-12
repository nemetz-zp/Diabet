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
using Diabet.View;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace Diabet
{
    public partial class CommunesForm : Form
    {
        BindingList<Commune> communesList;
        bool dbInfoIsLoaded = false;

        bool communesListIsChange = false;

        public bool CommunesListIsChange
        {
            get
            {
                return communesListIsChange;
            }
        }

        public CommunesForm()
        {
            InitializeComponent();
            
            using(DiabetContext dc = new DiabetContext())
            {
                communesList = new BindingList<Commune>(dc.Communes.ToList());
                communesTable.DataSource = communesList;
                communesTable.Refresh();
            }

            dbInfoIsLoaded = true;
        }

        private void addCommuneButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Commune newCommune = new Commune { Name = "- НОВА ГРОМАДА -" };
            communesList.Add(newCommune);
            communesTable.Refresh();
            communesTable.CurrentCell = communesTable.Rows[communesTable.Rows.Count - 1].Cells[1];

            new Task(() => 
            {
                using(DiabetContext dc = new DiabetContext())
                {
                    dc.Communes.Add(newCommune);
                    dc.SaveChanges();
                }
                communesListIsChange = true;
            }).Start();
        }

        private void communesTable_SelectionChanged(object sender, EventArgs e)
        {
            deleteCommuneButton.Enabled = (communesTable.SelectedCells.Count > 0);
        }

        private void communesTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dbInfoIsLoaded)
            {
                using(DiabetContext dc = new DiabetContext())
                {
                    Commune selectedCommune = communesTable.Rows[e.RowIndex].DataBoundItem as Commune;
                    dc.Communes.Attach(selectedCommune);
                    dc.Entry(selectedCommune).State = System.Data.Entity.EntityState.Modified;
                    dc.SaveChanges();

                    communesListIsChange = true;
                }
            }
        }

        private bool TryDeleteCommune(Commune rec)
        {
            try
            {
                using(DiabetContext dc = new DiabetContext())
                {
                    dc.Communes.Attach(rec);
                    dc.Communes.Remove(rec);
                    dc.SaveChanges();
                }
                return true;
            }
            catch(DbUpdateException ex)
            {
                var sqlExc = ex.GetBaseException() as SqlException;
                if(sqlExc != null && sqlExc.Number == 547)
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        private void deleteCommuneButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (DiabetContext dc = new DiabetContext())
            {
                int selectedRow = communesTable.CurrentCell.RowIndex;
                Commune selectedCommune = communesTable.Rows[selectedRow].DataBoundItem as Commune;
                
                if(TryDeleteCommune(selectedCommune))
                {
                    communesTable.Rows.RemoveAt(selectedRow);
                    communesListIsChange = true;
                }
                else
                {
                    Notificator.ShowError("Неможливо видалити громаду, так як в БД існують пацієнти записані на цю громаду");
                }
            }
        }
    }
}
