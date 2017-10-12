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
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using Diabet.View;

namespace Diabet
{
    public partial class MedicamentListForm : Form
    {
        BindingList<MedicamentAgent> agentList;
        BindingList<MedicamentName> medNamesList;
        BindingList<Medicament> medicamentsList;

        bool formIsLoaded;

        public MedicamentListForm()
        {
            formIsLoaded = false;

            InitializeComponent();
            agentsTable.AutoGenerateColumns = medicamentTable.AutoGenerateColumns = concreteMedTable.AutoGenerateColumns = false;
            
            using(DiabetContext dc = new DiabetContext())
            {
                agentList = new BindingList<MedicamentAgent>(dc.MedicamentGroups.ToList());
                agentsTable.DataSource = agentList;

                medNamesList = new BindingList<MedicamentName>(dc.MedicamentNames.ToList());
                medicamentTable.DataSource = medNamesList;

                medicamentsList = new BindingList<Medicament>(dc.Medicaments.Include(p => p.AgentDozages)
                    .Include(p => p.AgentDozages.Select(k => k.Agent))
                    .Include(p => p.AgentDozages.Select(k => k.DozageMeter))
                    .Include(p => p.FullName)
                    .Include(p => p.MedicamentType).ToList());
                concreteMedTable.DataSource = medicamentsList;
            }

            formIsLoaded = true;
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            deleteConcreteMedLink.Enabled = addFromCopyConcreteMedLink.Enabled = (concreteMedTable.SelectedRows.Count > 0);
        }

        private void deleteConcreteMedLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Medicament selectedItem = concreteMedTable.Rows[concreteMedTable.SelectedCells[0].RowIndex].DataBoundItem as Medicament;

            if (!TryDeleteRecord(selectedItem))
            {
                Notificator.ShowError("Неможливо видалити медикамент, тому що на нього э посилання в записах пацієнтів");
                return;
            }

            concreteMedTable.Rows.RemoveAt(concreteMedTable.SelectedCells[0].RowIndex);
        }

        private void addFromCopyConcreteMedLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Medicament selectedMedicament = concreteMedTable.SelectedRows[0].DataBoundItem as Medicament;
            MedicamentForm mf = new MedicamentForm(selectedMedicament.Id, true);
            mf.ShowDialog();

            if(mf.CurrentMedicament != null)
            {
                medicamentsList.Add(mf.CurrentMedicament);
                concreteMedTable.Refresh();
            }
        }

        private void addConcreteMedLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MedicamentForm mf = new MedicamentForm();
            mf.ShowDialog();

            if (mf.CurrentMedicament != null)
            {
                medicamentsList.Add(mf.CurrentMedicament);
                concreteMedTable.Refresh();
            }
        }

        private void addAgentLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MedicamentAgent newAg = new MedicamentAgent { Name = "- НОВА ДІЮЧА РЕЧОВИНА -" };
            agentList.Add(newAg);

            agentsTable.CurrentCell = agentsTable.Rows[agentsTable.Rows.Count - 1].Cells[0];

            new Task(() => 
            {
                using(DiabetContext dc = new DiabetContext())
                {
                    dc.MedicamentGroups.Add(newAg);
                    dc.SaveChanges();
                }
            }).Start();
        }

        private void addMedicamentLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MedicamentName newName = new MedicamentName { Name = "- НОВА ДІЮЧА РЕЧОВИНА -" };
            medNamesList.Add(newName);

            medicamentTable.CurrentCell = medicamentTable.Rows[medicamentTable.Rows.Count - 1].Cells[0];

            new Task(() =>
            {
                using (DiabetContext dc = new DiabetContext())
                {
                    dc.MedicamentNames.Add(newName);
                    dc.SaveChanges();
                }
            }).Start();
        }

        private void deleteMedicamentLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MedicamentName selectedItem = medicamentTable.Rows[medicamentTable.SelectedCells[0].RowIndex].DataBoundItem as MedicamentName;

            if(!TryDeleteRecord(selectedItem))
            {
                Notificator.ShowError("Неможливо видалити медикамент, тому що він присутній у записах пацієнтів");
                return;
            }

            medicamentTable.Rows.RemoveAt(medicamentTable.SelectedCells[0].RowIndex);
        }

        private void deleteAgentLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MedicamentAgent selectedItem = agentsTable.Rows[agentsTable.SelectedCells[0].RowIndex].DataBoundItem as MedicamentAgent;

            if (!TryDeleteRecord(selectedItem))
            {
                Notificator.ShowError("Неможливо видалити діючу речовину, тому що під неї створені записи про медикаменти");
                return;
            }

            agentsTable.Rows.RemoveAt(agentsTable.SelectedCells[0].RowIndex);
        }

        private bool TryDeleteRecord(object obj)
        {
            try
            {
                using (DiabetContext dc = new DiabetContext())
                {
                    if (obj is MedicamentAgent)
                    {
                        MedicamentAgent entity = obj as MedicamentAgent;
                        dc.MedicamentGroups.Attach(entity);
                        dc.MedicamentGroups.Remove(entity);
                    }
                    else if (obj is MedicamentName)
                    {
                        MedicamentName entity = obj as MedicamentName;
                        dc.MedicamentNames.Attach(entity);
                        dc.MedicamentNames.Remove(entity);
                    }
                    else if(obj is Medicament)
                    {
                        Medicament entity = obj as Medicament;
                        dc.Medicaments.Attach(entity);
                        dc.AgentDozages.RemoveRange(dc.AgentDozages.Where(t => t.MedicamentId == entity.Id));
                        entity.AgentDozages.Clear();

                        dc.Medicaments.Remove(entity);
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
            catch(Exception ex)
            {
                throw;
            }
        }

        private void agentsTable_SelectionChanged(object sender, EventArgs e)
        {
            deleteAgentLink.Enabled = (agentsTable.SelectedCells.Count > 0);
        }

        private void medicamentTable_SelectionChanged(object sender, EventArgs e)
        {
            deleteMedicamentLink.Enabled = (medicamentTable.SelectedCells.Count > 0);
        }

        private void concreteMedTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Medicament selectedMedicament = concreteMedTable.SelectedRows[0].DataBoundItem as Medicament;
            MedicamentForm mf = new MedicamentForm(selectedMedicament.Id, false);
            mf.ShowDialog();

            if (mf.CurrentMedicament != null)
            {
                selectedMedicament.AgentDozages = mf.CurrentMedicament.AgentDozages;
                selectedMedicament.FullName = mf.CurrentMedicament.FullName;
                selectedMedicament.MedicamentNameId = mf.CurrentMedicament.MedicamentNameId;
                selectedMedicament.MedicamentType = mf.CurrentMedicament.MedicamentType;
                selectedMedicament.MedicamentTypeId = mf.CurrentMedicament.MedicamentTypeId;
                selectedMedicament.NumInPack = mf.CurrentMedicament.NumInPack;
                selectedMedicament.Price = mf.CurrentMedicament.Price;
            }
        }

        private void agentsTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (formIsLoaded)
            {
                MedicamentAgent selectedItem = agentsTable.Rows[e.RowIndex].DataBoundItem as MedicamentAgent;

                new Task(() =>
                {
                    using (DiabetContext dc = new DiabetContext())
                    {
                        dc.MedicamentGroups.Attach(selectedItem);
                        dc.Entry<MedicamentAgent>(selectedItem).State = EntityState.Modified;
                        dc.SaveChanges();
                    }
                }).Start(); 
            }
        }

        private void medicamentTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (formIsLoaded)
            {
                MedicamentName selectedItem = medicamentTable.Rows[e.RowIndex].DataBoundItem as MedicamentName;

                new Task(() =>
                {
                    using (DiabetContext dc = new DiabetContext())
                    {
                        dc.MedicamentNames.Attach(selectedItem);
                        dc.Entry<MedicamentName>(selectedItem).State = EntityState.Modified;
                        dc.SaveChanges();
                    }
                }).Start(); 
            }
        }


    }
}
