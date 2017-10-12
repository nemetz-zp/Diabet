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
using Diabet.View;

namespace Diabet
{
    public partial class MedicamentForm : Form
    {
        BindingList<AgentDozage> dozages;
        List<int> selectedAgents;
        bool formIsInitialized;
        bool idDataChanged;

        private Medicament currentMedicament;
        private Meter deafaultAgentMeter;

        public Medicament CurrentMedicament
        {
            get
            {
                if (idDataChanged)
                {
                    return currentMedicament;
                }
                else
                {
                    return null;
                }
            }
        }

        public MedicamentForm()
        {
            formIsInitialized = false;
            idDataChanged = false;

            selectedAgents = new List<int>();
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            dozages = new BindingList<AgentDozage>();
            dataGridView1.DataSource = dozages;
            dataGridView1.Refresh();

            using(DiabetContext dc = new DiabetContext())
            {
                medNameBox.DataSource = dc.MedicamentNames.ToList();
                medNameBox.DisplayMember = "Name";
                medNameBox.ValueMember = "Id";

                packageType.DataSource = dc.MedicamentTypes.ToList();
                packageType.DisplayMember = "Name";
                packageType.ValueMember = "Id";

                deafaultAgentMeter = dc.Meters.Where(t => t.MType == MeterType.MedicamentDozage).First();
            }

            formIsInitialized = true;
        }

        /// <summary>
        /// Форма редактирования медикамента
        /// </summary>
        /// <param name="id">Идентификатор существующего медикамента</param>
        /// <param name="isCopyMode">True если создаётся новый медикамент на основе существующего</param>
        public MedicamentForm(int id, bool isCopyMode) : this()
        {
            formIsInitialized = false;

            using(DiabetContext dc = new DiabetContext())
            {
                currentMedicament = dc.Medicaments.Where(t => (t.Id == id)).Include(t => t.AgentDozages).First();
                medNameBox.SelectedValue = currentMedicament.MedicamentNameId;
                packageType.SelectedValue = currentMedicament.MedicamentTypeId;
                numInPackage.Value = currentMedicament.NumInPack;
                priceField.Value = currentMedicament.Price;

                dozages = new BindingList<AgentDozage>(dc.AgentDozages.Include(t => t.DozageMeter)
                    .Include(t => t.Agent).Where(t => (t.MedicamentId == id)).ToList());

                foreach (var item in dozages)
                    selectedAgents.Add(item.Agent.Id);

                if (isCopyMode)
                    currentMedicament.Id = 0;

                dataGridView1.DataSource = dozages;
                dataGridView1.Refresh();
            }

            formIsInitialized = true;
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = e.RowIndex; i <= (e.RowIndex + e.RowCount - 1); i++)
                dataGridView1.Rows[i].Cells[0].Value = (i + 1).ToString();
        }

        private void updateTableNumRowsColumn()
        {
            int rowsNum = dataGridView1.Rows.Count;
            for(int i = 0; i < rowsNum; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = (i + 1).ToString();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            deleteAgentLink.Enabled = (dataGridView1.SelectedCells.Count > 0);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (formIsInitialized)
            {
                if ((e.ColumnIndex == 1) && (e.RowIndex >= 0))
                {
                    AgentSelectForm asf = new AgentSelectForm(selectedAgents);
                    asf.ShowDialog();

                    if(asf.SelectedAgent != null)
                    {
                        AgentDozage selectedAgDoz = dataGridView1.Rows[e.RowIndex].DataBoundItem as AgentDozage;
                        selectedAgDoz.Agent = asf.SelectedAgent;
                        dataGridView1.Refresh();
                        selectedAgents.Add(selectedAgDoz.Agent.Id);
                    }
                }
                if ((e.ColumnIndex == 2) && (e.RowIndex >= 0))
                {
                    MeterSelectForm msf = new MeterSelectForm(MeterType.MedicamentDozage);
                    msf.ShowDialog();

                    if(msf.SelectedMeter != null)
                    {
                        AgentDozage selectedAgDoz = dataGridView1.Rows[e.RowIndex].DataBoundItem as AgentDozage;
                        selectedAgDoz.DozageMeter = msf.SelectedMeter;
                        selectedAgDoz.MeterId = msf.SelectedMeter.Id;
                        dataGridView1.Refresh();
                    }
                }
            }
        }

        private void deleteAgentLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
            AgentDozage selectedAgDoz = dataGridView1.Rows[selectedRowIndex].DataBoundItem as AgentDozage;
            selectedAgDoz.Id = -1;
            
            if (selectedAgDoz.Agent != null)
            {
                selectedAgents.Remove(selectedAgDoz.Agent.Id);
            }

            dataGridView1.Rows.RemoveAt(selectedRowIndex);
            dozages.Remove(selectedAgDoz);
            updateTableNumRowsColumn();
            dataGridView1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(numInPackage.Value == 0)
            {
                Notificator.ShowError("В упаковці не може бути 0 од.");
                return;
            }

            if(dozages.Where(t => t.Agent == null).Count() > 0)
            {
                Notificator.ShowError("Ви не вибрали діючу речовину");
                return;
            }

            using(DiabetContext dc = new DiabetContext())
            {
                if(currentMedicament == null)
                {
                    currentMedicament = new Medicament();
                }

                // Обнуляем навигационные свойства для избежания ошибок
                currentMedicament.FullName = null;
                currentMedicament.MedicamentType = null;
                currentMedicament.AgentDozages.Clear();

                currentMedicament.MedicamentNameId = (int)medNameBox.SelectedValue;
                currentMedicament.MedicamentTypeId = (int)packageType.SelectedValue;
                currentMedicament.NumInPack = (int)numInPackage.Value;
                currentMedicament.Price = priceField.Value;

                if(currentMedicament.Id > 0)
                {
                    dc.Medicaments.Attach(currentMedicament);
                    dc.Entry<Medicament>(currentMedicament).State = EntityState.Modified;
                }
                else
                {
                    dc.Medicaments.Add(currentMedicament);
                }
                dc.SaveChanges();

                foreach(var item in dozages)
                {
                    item.DozageMeter = null;
                    item.MedicamentId = currentMedicament.Id;
                    dc.MedicamentGroups.Attach(item.Agent);
                    if(item.Id > 0)
                    {
                        dc.AgentDozages.Attach(item);
                        dc.Entry<AgentDozage>(item).State = EntityState.Modified;
                    }
                    else
                    {
                        dc.AgentDozages.Add(item);
                    }
                    dc.SaveChanges();
                }

                // Эти изменения не попадут в БД, а нужны лишь для отображения изменения по медикаменту в интерфейсе
                currentMedicament.FullName = medNameBox.SelectedItem as MedicamentName;
                currentMedicament.MedicamentType = packageType.SelectedItem as MedicamentType;

                idDataChanged = true;

                Notificator.ShowInfo("Дані успішно збережені!");
            }
        }

        private void addAgentLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AgentDozage newAgDoz = new AgentDozage();
            newAgDoz.DozageMeter = deafaultAgentMeter;
            newAgDoz.MeterId = deafaultAgentMeter.Id;
            if (currentMedicament != null)
            {
                newAgDoz.MedicamentId = currentMedicament.Id;
                dozages.Add(newAgDoz);
            }
            else
            {
                dozages.Add(newAgDoz);
            }
        }

    }
}
