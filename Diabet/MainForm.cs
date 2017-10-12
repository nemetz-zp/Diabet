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
using Diabet.Helpers;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Threading;

namespace Diabet
{
    public partial class MainForm : Form
    {

        enum LiveStatus
        {
            All,
            OnlyAlive,
            OnlyDead
        }

        private BindingList<Patient> allPatientsList;
        private BindingList<Patient> filteredPatientList;

        private List<MedicamentName> filterMedicamentNames;

        private bool formIsReady;

        public MainForm()
        {
            formIsReady = false;
            InitializeComponent();
            filterMedicamentNames = new List<MedicamentName>();
            StartPosition = FormStartPosition.CenterScreen;

            allPatientsList = new BindingList<Patient>();
            filteredPatientList = new BindingList<Patient>();
            patientsList.AutoGenerateColumns = false;

            // Создаём папку с фотографиями пациентов, если ее нет
            if(!System.IO.Directory.Exists(PathFinder.GetImgPath()))
            {
                System.IO.Directory.CreateDirectory(PathFinder.GetImgPath());
            }

            // Типы диабета
            List<EnumVisualizer> diabetTypesList = new List<EnumVisualizer>();
            diabetTypesList.Add(new EnumVisualizer { Value = -1, Text = "- Усі типи -" });
            diabetTypesList.Add(new EnumVisualizer { Value = (int)DiabetType.FirstType, Text = "I тип" });
            diabetTypesList.Add(new EnumVisualizer { Value = (int)DiabetType.SecondType, Text = "II тип" });
            diabetTypesList.Add(new EnumVisualizer { Value = (int)DiabetType.Gestac, Text = "Гестаційний" });
            diabetTypesList.Add(new EnumVisualizer { Value = (int)DiabetType.Secondary, Text = "Вторинний" });

            comboBox3.DataSource = diabetTypesList;
            comboBox3.ValueMember = "Value";
            comboBox3.DisplayMember = "Text";

            // Пол
            List<EnumVisualizer> sexesList = new List<EnumVisualizer>();
            sexesList.Add(new EnumVisualizer { Value = -1, Text = "- Усі статі -" });
            sexesList.Add(new EnumVisualizer { Value = (int)Sex.Male, Text = "Чоловічий" });
            sexesList.Add(new EnumVisualizer { Value = (int)Sex.Female, Text = "Жіночий" });

            comboBox2.DataSource = sexesList;
            comboBox2.ValueMember = "Value";
            comboBox2.DisplayMember = "Text";

            // Живые/умершие
            List<EnumVisualizer> liveStatusList = new List<EnumVisualizer>();
            liveStatusList.Add(new EnumVisualizer { Value = (int)(LiveStatus.All), Text = "- Усі -" });
            liveStatusList.Add(new EnumVisualizer { Value = (int)(LiveStatus.OnlyAlive), Text = "Тільки живі" });
            liveStatusList.Add(new EnumVisualizer { Value = (int)(LiveStatus.OnlyDead), Text = "Тільки померлі" });

            comboBox4.DataSource = liveStatusList;
            comboBox4.ValueMember = "Value";
            comboBox4.DisplayMember = "Text";

            using(DiabetContext dc = new DiabetContext())
            {
                List<Commune> communesList = dc.Communes.ToList();
                communesList.Insert(0, new Commune { Id = -1, Name = "- Усі громади -" });
                comboBox1.DataSource = communesList;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "Id";
            }
            
            switchWaiting();
            backgroundWorker1.RunWorkerAsync();

            formIsReady = true;
        }

        private void updateCommunesList()
        {
            if(!backgroundWorker1.IsBusy)
            {
                using (DiabetContext dc = new DiabetContext())
                {
                    List<Commune> communesList = dc.Communes.ToList();
                    communesList.Insert(0, new Commune { Id = -1, Name = "- Усі громади -" });
                    comboBox1.DataSource = communesList;
                }

                switchWaiting();
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void applyFilter()
        {
            if(!formIsReady)
            {
                return;
            }

            IEnumerable<Patient> result = allPatientsList;

            // Фильтр по громадам
            if(Convert.ToInt32(comboBox1.SelectedValue) != -1)
            {
                result = result.Where(p => p.CommuneId == Convert.ToInt32(comboBox1.SelectedValue));
            }

            // Фильтр по живым/умершим
            if ((LiveStatus)(comboBox4.SelectedValue) != LiveStatus.All)
            {
                if ((LiveStatus)(comboBox4.SelectedValue) == LiveStatus.OnlyAlive)
                {
                    result = result.Where(p => !p.IsDead);
                }
                else
                {
                    result = result.Where(p => p.IsDead);
                }
            }

            // Фильтр по полу
            if ((int)(comboBox2.SelectedValue) != -1)
            {
                result = result.Where(p => p.Sex == (Sex)(comboBox2.SelectedValue));
            }

            // Фильтр по типам диабета
            if((int)(comboBox3.SelectedValue) != -1)
            {
                result = result.Where(p => p.DType == (DiabetType)(comboBox3.SelectedValue));
            }

            if(!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                result = result.Where(p => p.LastName.ToLower().Contains(textBox1.Text.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                result = result.Where(p => p.Adress.ToLower().Contains(textBox2.Text.ToLower()));
            }

            DateTime startDate = DateTime.Now.AddYears(-(int)numericUpDown1.Value);
            DateTime endDate = DateTime.Now.AddYears(-(int)numericUpDown2.Value);
            result = result.Where(p => (p.BirthDate <= startDate) && (p.BirthDate >= endDate));
            filteredPatientList = new BindingList<Patient>(result.ToList());
            
            patientsList.DataSource = filteredPatientList;
            patientsList.Refresh();

            printReportToolStripMenuItem.Enabled = safePrintReportToolStripMenuItem.Enabled = (filteredPatientList.Count > 0);
        }

        private void patientsList_SelectionChanged(object sender, EventArgs e)
        {
            editPatientButton.Enabled = deletePatientButton.Enabled = (patientsList.SelectedRows.Count > 0);
        }

        private void addPatientButton_Click(object sender, EventArgs e)
        {
            PatientInfoForm pf = new PatientInfoForm();
            pf.ShowDialog();

            if(pf.Result != null)
            {
                allPatientsList.Add(pf.Result);
                filteredPatientList.Add(pf.Result);
                patientsList.Refresh();
            }
        }

        private void medicamentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MedicamentListForm mlf = new MedicamentListForm();
            mlf.ShowDialog();
        }

        private void metersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MeterForm mf = new MeterForm();
            mf.ShowDialog();
        }

        private void communesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommunesForm cf = new CommunesForm();
            cf.ShowDialog();

            if(cf.CommunesListIsChange)
            {
                updateCommunesList();
            }
        }

        private void analizesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalizeListForm af = new AnalizeListForm();
            af.ShowDialog();
        }

        private void medRestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MedAssignChangeForm mf = new MedAssignChangeForm();
            mf.ShowDialog();
        }

        private void patientsList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = e.RowIndex; i <= (e.RowIndex + e.RowCount - 1); i++)
                patientsList.Rows[i].Cells[0].Value = (i + 1).ToString();
        }

        private void updateRowsNumberColumn()
        {
            for(int i = 0; i < patientsList.Rows.Count; i++)
            {
                patientsList.Rows[i].Cells[0].Value =  (i + 1).ToString();
            }
        }

        private void editPatientButton_Click(object sender, EventArgs e)
        {
            editPatientAction();
        }

        private void deletePatientButton_Click(object sender, EventArgs e)
        {
            Patient selectedRecord = patientsList.SelectedRows[0].DataBoundItem as Patient;
            if(Notificator.ShowActionConfirmation(string.Format("Ви впевнені, що хочете видали дані пацієнта '{0}'?", selectedRecord.FullName)) != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }
            patientsList.Rows.RemoveAt(patientsList.SelectedRows[0].Index);
            allPatientsList.Remove(selectedRecord);
            filteredPatientList.Remove(selectedRecord);
            updateRowsNumberColumn();

            new Task(() =>
            {
                if (selectedRecord.PhotoFile != null)
                {
                    if (System.IO.File.Exists(System.IO.Path.Combine(PathFinder.GetImgPath(), selectedRecord.PhotoFile)))
                    {
                        System.IO.File.Delete(System.IO.Path.Combine(PathFinder.GetImgPath(), selectedRecord.PhotoFile));
                    }
                }

                using (DiabetContext dc = new DiabetContext())
                {
                    dc.Patients.Attach(selectedRecord);
                    dc.Patients.Remove(selectedRecord);
                    dc.SaveChanges();
                }
            }).Start();
        }

        private void patientsList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            editPatientAction();
        }

        private void editPatientAction()
        {
            Patient selectedRecord = patientsList.SelectedRows[0].DataBoundItem as Patient;
            PatientInfoForm pif = new PatientInfoForm(selectedRecord.Id);
            pif.ShowDialog();
            
            if(pif.Result != null)
            {
                selectedRecord.CopyPatient(pif.Result);
                patientsList.Refresh();
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            applyFilter();
        }

        private void mainSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm sf = new SettingsForm();
            sf.ShowDialog();
        }

        private void medReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MedAssignStatisticForm mf = new MedAssignStatisticForm();
            mf.ShowDialog();
        }

        private void yearNeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YearMedicamentsNeedForm yf = new YearMedicamentsNeedForm();
            yf.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                MedicamentNameSelectForm mf = new MedicamentNameSelectForm();
                mf.ShowDialog();

                if (mf.SelectedMedicaments.Count > 0)
                {
                    filterMedicamentNames = mf.SelectedMedicaments;

                    StringBuilder sb = new StringBuilder();
                    foreach (var item in mf.SelectedMedicaments)
                        sb.Append(item + ", ");

                    textBox3.Text = sb.ToString().Substring(0, sb.ToString().Length - 2);
                    
                    switchWaiting();
                    allPatientsList.Clear();
                    backgroundWorker1.RunWorkerAsync();
                } 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (filterMedicamentNames.Count == 0)
                return;

            if(!backgroundWorker1.IsBusy)
            {
                textBox3.Text = string.Empty;
                filterMedicamentNames.Clear();
                switchWaiting();
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void switchWaiting()
        {
            waitImg.Visible = waitLabel.Visible = !(waitLabel.Visible);
            patientsList.Enabled = !(patientsList.Enabled);
            groupBox1.Enabled = !(groupBox1.Enabled);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            using (DiabetContext dc = new DiabetContext())
            {
                if (filterMedicamentNames.Count == 0)
                {
                    allPatientsList = new BindingList<Patient>(dc.Patients.Include(t => t.PatientCommune).OrderBy(t => t.LastName).ToList());
                }
                else
                {

                    List<int> medIDs = filterMedicamentNames.Select(t => t.Id).ToList();
                    allPatientsList = new BindingList<Patient>(dc.MedicamentAssigantions
                                    .Where(t => medIDs.Contains(t.AssignMedicament.FullName.Id))
                                    .Include(t => t.Patient)
                                    .Include(t => t.Patient.PatientCommune)
                                    .ToList().GroupBy(t => t.Patient).Select(t => t.Key).OrderBy(t => t.LastName).ToList());

                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            applyFilter();
            patientsList.Refresh();
            switchWaiting();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if(formIsReady)
            {
                applyFilter();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (formIsReady)
            {
                applyFilter();
            }
        }

        private void printReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcelReportProcessForm ef = new ExcelReportProcessForm(filteredPatientList.ToList(), false);
            ef.ShowDialog();
        }

        private void safePrintReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcelReportProcessForm ef = new ExcelReportProcessForm(filteredPatientList.ToList(), true);
            ef.ShowDialog();
        }
    }
}
