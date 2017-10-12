using Diabet.View;
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
using Diabet.Helpers;
using System.Data.Entity;

namespace Diabet
{
    public partial class PatientInfoForm : Form
    {
        BindingList<MedicamentMovement> medicamentMovementList;
        BindingList<MedicamentMovement> filteredMedicamentMovementList;
        BindingList<MedicamentAssignation> medicamentAssignationList;
        BindingList<PatientAnalyze> analizesList;
        BindingList<MedicamentYearSumDozage> medicamentsSumDozageList;
        BindingList<PatientHistoryRecord> patientHistoryRecordsList;

        // Список идентификаторов назначеных действующих веществ
        List<int> agentsList;

        //---------------------------------------------------------------
        // Очереди на добавления, изменения и удаления записей из таблиц
        //---------------------------------------------------------------
        List<MedicamentMovement> addMedMovList;
        List<MedicamentMovement> editMedMovList;
        List<MedicamentMovement> remMedMovList;

        List<MedicamentAssignation> addMedAssignList;
        List<MedicamentAssignation> editMedAssignList;
        List<MedicamentAssignation> remMedAssignList;

        List<PatientAnalyze> addPatAnList;
        List<PatientAnalyze> editPatAnList;
        List<PatientAnalyze> remPatAnList;

        List<PatientHistoryRecord> addPatHistList;
        List<PatientHistoryRecord> editPatHistList;
        List<PatientHistoryRecord> remPatHistList;
        //---------------------------------------------------------------

        // Сохранены ли внесённые изменения
        private bool patientDataIsSaved;

        // Форма загружена?
        bool formIsInialized = false;

        // Путь к старому фото
        string oldPhotoFile = null;

        // Связаная с формой запись о пациенте
        Patient dbRecord = null;

        // Дней в этом году (нужно для определения годовой потребности в медикаменте)
        int daysInYear = 0;

        public Patient Result
        {
            get
            {
                if (patientDataIsSaved)
                {
                    return dbRecord;
                }
                else
                {
                    return null;
                }
            }
        }

        public PatientInfoForm()
        {
            DateTime firstDayOfYear = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
            DateTime lastDayOfYear = new DateTime(DateTime.Now.Year, 12, 31, 23, 59, 59);
            daysInYear = Convert.ToInt32((lastDayOfYear - firstDayOfYear).TotalDays);

            dbRecord = new Patient();

            patientDataIsSaved = false;

            agentsList = new List<int>();

            addMedMovList = new List<MedicamentMovement>();
            editMedMovList = new List<MedicamentMovement>();
            remMedMovList = new List<MedicamentMovement>();

            addMedAssignList = new List<MedicamentAssignation>();
            editMedAssignList = new List<MedicamentAssignation>();
            remMedAssignList = new List<MedicamentAssignation>();

            addPatAnList = new List<PatientAnalyze>();
            editPatAnList = new List<PatientAnalyze>();
            remPatAnList = new List<PatientAnalyze>();

            addPatHistList = new List<PatientHistoryRecord>();
            editPatHistList = new List<PatientHistoryRecord>();
            remPatHistList = new List<PatientHistoryRecord>();

            InitializeComponent();
            deletePhotoButton.Enabled = (patientPhoto.Image != null);

            medicamentsTable.AutoGenerateColumns = medicamentsSumDozageTable.AutoGenerateColumns 
                = medicamentMovementTable.AutoGenerateColumns = analizesTable.AutoGenerateColumns 
                = dataGridView1.AutoGenerateColumns = false;

            // Инициализируем таблицы со связаными данными (назначения, анализы и т.п.)
            medicamentAssignationList = new BindingList<MedicamentAssignation>();
            analizesList = new BindingList<PatientAnalyze>();
            medicamentsSumDozageList = new BindingList<MedicamentYearSumDozage>();
            medicamentMovementList = new BindingList<MedicamentMovement>();
            filteredMedicamentMovementList = new BindingList<MedicamentMovement>();
            patientHistoryRecordsList = new BindingList<PatientHistoryRecord>();
            updateTablesSources();

            // Список полов
            List<EnumVisualizer> sexList = new List<EnumVisualizer>();
            sexList.Add(new EnumVisualizer { Value = (int)Sex.Male, Text = "Чоловічий" });
            sexList.Add(new EnumVisualizer { Value = (int)Sex.Female, Text = "Жіночий" });
            sexBox.DataSource = sexList;
            sexBox.DisplayMember = "Text";
            sexBox.ValueMember = "Value";

            // Список типов диабета
            List<EnumVisualizer> diabetTypes = new List<EnumVisualizer>();
            diabetTypes.Add(new EnumVisualizer { Value = (int)DiabetType.FirstType, Text = "I тип" });
            diabetTypes.Add(new EnumVisualizer { Value = (int)DiabetType.SecondType, Text = "II тип" });
            diabetTypes.Add(new EnumVisualizer { Value = (int)DiabetType.Gestac, Text = "Гестаційний" });
            diabetTypes.Add(new EnumVisualizer { Value = (int)DiabetType.Secondary, Text = "Вторинний" });
            diabetTypeBox.DataSource = diabetTypes;
            diabetTypeBox.DisplayMember = "Text";
            diabetTypeBox.ValueMember = "Value";

            // Инициализируем выподающий список отчетных годов (начальный год - 2015)
            List<EnumVisualizer> reportYears = new List<EnumVisualizer>();
            for (int i = DateTime.Now.Year; i >= 2015; i-- )
            {
                reportYears.Add(new EnumVisualizer { Text = i.ToString(), Value = i });
            }
            medicamentsReportYearBox.DataSource = reportYears;
            medicamentsReportYearBox.DisplayMember = "Text";
            medicamentsReportYearBox.ValueMember = "Value";
            // Фильтруем список
            applyYearFilterToMovements();

            // Вытягиваем список громад, медикаментов, действующих веществ и т.п.
            using (DiabetContext dc = new DiabetContext())
            {
                List<Commune> communesList = dc.Communes.ToList();
                communeBox.DataSource = communesList;
                communeBox.DisplayMember = "Name";
                communeBox.ValueMember = "Id";
            }

            formIsInialized = true;
        }

        // Обновление привязки источников данных таблиц
        private void updateTablesSources()
        {
            medicamentsTable.DataSource = medicamentAssignationList;
            analizesTable.DataSource = analizesList;
            medicamentsSumDozageTable.DataSource = medicamentsSumDozageList;
            medicamentMovementTable.DataSource = medicamentMovementList;
            dataGridView1.DataSource = patientHistoryRecordsList;
        }

        public PatientInfoForm(int patientId) : this()
        {
            formIsInialized = false;

            using(DiabetContext dc = new DiabetContext())
            {
                dbRecord = dc.Patients.Where(t => (t.Id == patientId)).First();

                // Список назначений
                medicamentAssignationList = new BindingList<MedicamentAssignation>(dc.MedicamentAssigantions
                    .Include(t => t.AssignMedicament)
                    .Include(t => t.AssignMedicament.MedicamentType)
                    .Include(t => t.AssignMedicament.FullName)
                    .Include(t => t.AssignMedicament.AgentDozages)
                    .Include(t => t.AssignMedicament.AgentDozages.Select(m => m.Agent))
                    .Include(t => t.AssignMedicament.AgentDozages.Select(m => m.DozageMeter))
                    .Where(t => t.PatientId == dbRecord.Id).ToList());
                foreach(var item in medicamentAssignationList)
                    agentsList.Add(item.AssignMedicament.Id);

                // Список анализов пациента
                analizesList = new BindingList<PatientAnalyze>(dc.PatientAnalyzes
                    .Where(t => t.PatientId == dbRecord.Id)
                    .OrderBy(t => t.AnalizeDate)
                    .Include(t => t.Analyze)
                    .Include(t => t.Analyze.AnalizeMeter)
                    .ToList());

                // Список выдачи медикаментов
                medicamentMovementList = new BindingList<MedicamentMovement>(dc.MedicamentMovements
                    .Where(t => t.PatientId == dbRecord.Id)
                    .OrderBy(t => t.MovementOperationDate)
                    .Include(t => t.Medicament)
                    .Include(t => t.Medicament.FullName)
                    .Include(t => t.Medicament.MedicamentType)
                    .Include(t => t.Medicament.AgentDozages)
                    .Include(t => t.Medicament.AgentDozages.Select(m => m.DozageMeter))
                    .ToList());

                // История пациента
                patientHistoryRecordsList = new BindingList<PatientHistoryRecord>(dc.PatientHistoryRecords
                    .Where(t => t.PatientId == dbRecord.Id)
                    .OrderBy(t => t.RecordDate)
                    .ToList());
                updateTablesSources();
                applyYearFilterToMovements();

                firstNameBox.Text = dbRecord.FirstName;
                lastNameBox.Text = dbRecord.LastName;
                middleNameBox.Text = dbRecord.MiddleName;
                birthDatePicker.Value = dbRecord.BirthDate;
                sexBox.SelectedValue = dbRecord.Sex;
                diabetTypeBox.SelectedValue = dbRecord.DType;
                communeBox.SelectedValue = dbRecord.CommuneId;
                adress.Text = dbRecord.Adress;
                otherPatientInfo.Text = dbRecord.OtherInfo;
                isDeadcheckBox.Checked = dbRecord.IsDead;
                sexBox.SelectedValue = (int)dbRecord.Sex;
                diabetTypeBox.SelectedValue = (int)dbRecord.DType;
                dateOfDeath.Value = dbRecord.DeathDate ?? DateTime.Now;
                if(dbRecord.PhotoFile != null)
                {
                    try
                    {
                        using (Bitmap bm = new Bitmap(System.IO.Path.Combine(PathFinder.GetImgPath(), dbRecord.PhotoFile)))
                        {
                            patientPhoto.Image = new Bitmap(bm);
                        }
                        deletePhotoButton.Enabled = true;
                    }
                    catch { }
                }
            }

            formIsInialized = true;

            medicamentsTable.Refresh();
            analizesTable.Refresh();
            medicamentMovementTable.Refresh();
            medicamentsSumDozageTable.Refresh();
            dataGridView1.Refresh();
        }

        // Удаления медикамента из списка назначеных
        private void deleteMedicamentLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int selectedRow = medicamentsTable.SelectedCells[0].RowIndex;
            MedicamentAssignation remMedAssignElem = medicamentsTable.Rows[selectedRow].DataBoundItem as MedicamentAssignation;

            if (remMedAssignElem.AssignMedicament != null)
            {
                if (Notificator.ShowActionConfirmation("Ви впевнені, що хочете цей запис?")
                    != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }
            }

            // Если мы удаляем из списка новосозданых элементов - помечам его отрицательным ID (чтобы найти)
            if (remMedAssignElem.Id == 0)
            {
                remMedAssignElem.Id = -1;
                addMedAssignList.Remove(remMedAssignElem);
            }
            else
            {
                editMedAssignList.Remove(remMedAssignElem);
                remMedAssignList.Add(remMedAssignElem);
            }

            medicamentsTable.Rows.RemoveAt(selectedRow);
            refreshDataGridViewRowNums(medicamentsTable);
            agentsList.Remove(remMedAssignElem.AssignMedicament.Id);
            medicamentsTable.Refresh();
        }

        private void refreshDataGridViewRowNums(DataGridView dgv)
        {
            for(int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv.Rows[i].Cells[0].Value = (i+1).ToString();
            }
        }

        private void addMedicamentLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MedicamentAssignation newMedAssign = new MedicamentAssignation();
            newMedAssign.Days = daysInYear;

            if(dbRecord.Id > 0)
            {
                newMedAssign.PatientId = dbRecord.Id;
            }

            addMedAssignList.Add(newMedAssign);
            medicamentAssignationList.Add(newMedAssign);
            medicamentsTable.Refresh();
        }

        private void addEditButton_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(firstNameBox.Text))
            {
                Notificator.ShowError("Ви не ввели ім'я пацієнта");
                return;
            }
            if (string.IsNullOrWhiteSpace(lastNameBox.Text))
            {
                Notificator.ShowError("Ви не ввели прізвище пацієнта");
                return;
            }
            if (string.IsNullOrWhiteSpace(middleNameBox.Text))
            {
                Notificator.ShowError("Ви не ввели ім'я по-батькові пацієнта");
                return;
            }

            if (string.IsNullOrWhiteSpace(adress.Text))
            {
                Notificator.ShowError("Ви не ввели адресу пацієнта");
                return;
            }

            if(addMedAssignList.Any(t => t.AssignMedicament == null))
            {
                Notificator.ShowError("Серед списку призначень є пусті колонки з назвою медикаменту");
                return;
            }

            if (addPatAnList.Any(t => t.Analyze == null))
            {
                Notificator.ShowError("Серед списку аналізів є пусті колонки з назвою аналізу");
                return;
            }

            if (addMedMovList.Any(t => t.Medicament == null))
            {
                Notificator.ShowError("Серед списку видачі медикаментів є пусті колонки з назвою медикаменту");
                return;
            }

            using(DiabetContext dc = new DiabetContext())
            {
                // Если пациент существовал и у него было фото, но мы его убрали - удаляём файл с фото
                if((dbRecord.Id > 0) && (dbRecord.PhotoFile != null) && (patientPhoto.Image == null))
                {
                    if (System.IO.File.Exists(System.IO.Path.Combine(PathFinder.GetImgPath(), dbRecord.PhotoFile)))
                        System.IO.File.Delete(System.IO.Path.Combine(PathFinder.GetImgPath(), dbRecord.PhotoFile));
                    dbRecord.PhotoFile = null;
                }

                if (dbRecord.PhotoFile != null)
                {
                    // Директория файла с фото
                    string pathOfPhotoFile = System.IO.Path.GetDirectoryName(dbRecord.PhotoFile);

                    // Если директория с файлом определена - значит была произведена установка фотографии
                    if (!string.IsNullOrWhiteSpace(pathOfPhotoFile))
                    {
                        if (!string.IsNullOrWhiteSpace(oldPhotoFile))
                        {
                            if (System.IO.File.Exists(System.IO.Path.Combine(PathFinder.GetImgPath(), oldPhotoFile)))
                                System.IO.File.Delete(System.IO.Path.Combine(PathFinder.GetImgPath(), oldPhotoFile));
                        }

                        string photoFileExt = (new System.IO.FileInfo(dbRecord.PhotoFile)).Extension;
                        string photoFileNewName = string.Empty;
                        string photoFileNewPath = string.Empty;
                        do
                        {
                            photoFileNewName = Guid.NewGuid().ToString() + photoFileExt;
                            photoFileNewPath = System.IO.Path.Combine(PathFinder.GetImgPath(), photoFileNewName);
                        }
                        while (System.IO.File.Exists(photoFileNewPath));

                        System.IO.File.Copy(dbRecord.PhotoFile, photoFileNewPath);

                        dbRecord.PhotoFile = photoFileNewName;
                    }
                }

                dbRecord.FirstName = firstNameBox.Text.Trim();
                dbRecord.LastName = lastNameBox.Text.Trim();
                dbRecord.MiddleName = middleNameBox.Text.Trim();
                dbRecord.BirthDate = birthDatePicker.Value;

                dbRecord.CommuneId = (int)communeBox.SelectedValue;
                dbRecord.PatientCommune = communeBox.SelectedItem as Commune;
                dc.Communes.Attach(dbRecord.PatientCommune);

                dbRecord.Adress = adress.Text.Trim();
                dbRecord.Sex = (Sex)sexBox.SelectedValue;
                dbRecord.DType = (DiabetType)diabetTypeBox.SelectedValue;
                dbRecord.OtherInfo = otherPatientInfo.Text.Trim();
                dbRecord.IsDead = isDeadcheckBox.Checked;
                if(isDeadcheckBox.Checked)
                {
                    dbRecord.DeathDate = dateOfDeath.Value;
                }
                else
                {
                    dbRecord.DeathDate = null;
                }

                if(dbRecord.Id > 0)
                {
                    dc.Patients.Attach(dbRecord);
                    dc.Entry<Patient>(dbRecord).State = EntityState.Modified;
                }
                else
                {
                    dc.Patients.Add(dbRecord);
                }
                dc.SaveChanges();

                foreach(var item in addMedAssignList)
                {
                    item.AssignMedicament = null;
                    item.PatientId = dbRecord.Id;
                    dbRecord.Medicaments.Add(item);
                    dc.Entry<MedicamentAssignation>(item).State = System.Data.Entity.EntityState.Added;
                }
                foreach (var item in editMedAssignList)
                {
                    dc.Medicaments.Attach(item.AssignMedicament);
                    dc.Patients.Attach(item.Patient);
                    dc.MedicamentAssigantions.Attach(item);
                    item.PatientId = dbRecord.Id;
                    dc.Entry<MedicamentAssignation>(item).State = System.Data.Entity.EntityState.Modified;
                }
                foreach (var item in remMedAssignList)
                {
                    dc.Medicaments.Attach(item.AssignMedicament);
                    dc.Patients.Attach(item.Patient);
                    dc.MedicamentAssigantions.Remove(item);
                    item.PatientId = dbRecord.Id;
                    dc.Entry<MedicamentAssignation>(item).State = System.Data.Entity.EntityState.Deleted;
                }
                dc.SaveChanges();

                foreach (var item in addPatAnList)
                {
                    item.Analyze = null;
                    dbRecord.PatientAnalyzes.Add(item);
                    item.PatientId = dbRecord.Id;
                    dc.Entry<PatientAnalyze>(item).State = System.Data.Entity.EntityState.Added;
                }
                foreach (var item in editPatAnList)
                {
                    dc.Analyze.Attach(item.Analyze);
                    dc.PatientAnalyzes.Attach(item);
                    item.PatientId = dbRecord.Id;
                    dc.Entry<PatientAnalyze>(item).State = System.Data.Entity.EntityState.Modified;
                }
                foreach (var item in remPatAnList)
                {
                    dc.Analyze.Attach(item.Analyze);
                    dc.PatientAnalyzes.Attach(item);
                    item.PatientId = dbRecord.Id;
                    dc.Entry<PatientAnalyze>(item).State = System.Data.Entity.EntityState.Deleted;
                }
                dc.SaveChanges();

                foreach (var item in addMedMovList)
                {
                    item.Medicament = null;
                    dbRecord.MedMovements.Add(item);
                    item.PatientId = dbRecord.Id;
                    dc.Entry<MedicamentMovement>(item).State = System.Data.Entity.EntityState.Added;
                }
                foreach (var item in editMedMovList)
                {
                    item.PatientId = dbRecord.Id;
                    dc.Medicaments.Attach(item.Medicament);
                    dc.MedicamentMovements.Attach(item);
                    dc.Entry<MedicamentMovement>(item).State = System.Data.Entity.EntityState.Modified;
                }
                foreach (var item in remMedMovList)
                {
                    item.PatientId = dbRecord.Id;
                    dc.Medicaments.Attach(item.Medicament);
                    dc.MedicamentMovements.Attach(item);
                    dc.Entry<MedicamentMovement>(item).State = System.Data.Entity.EntityState.Deleted;
                }
                dc.SaveChanges();

                foreach (var item in addPatHistList)
                {
                    item.PatientId = dbRecord.Id;
                    dbRecord.HistoryRecords.Add(item);
                    dc.Entry<PatientHistoryRecord>(item).State = System.Data.Entity.EntityState.Added;
                }
                foreach (var item in editPatHistList)
                {
                    item.PatientId = dbRecord.Id;
                    dc.PatientHistoryRecords.Attach(item);
                    dc.Entry<PatientHistoryRecord>(item).State = System.Data.Entity.EntityState.Modified;
                }
                foreach (var item in remPatHistList)
                {
                    item.PatientId = dbRecord.Id;
                    dc.PatientHistoryRecords.Attach(item);
                    dc.Entry<PatientHistoryRecord>(item).State = System.Data.Entity.EntityState.Deleted;
                }
                dc.SaveChanges();

                // Очищаем очереди на изменения в БД
                addMedAssignList.Clear();
                editMedAssignList.Clear();
                remMedAssignList.Clear();
                addPatAnList.Clear();
                editPatAnList.Clear();
                remPatAnList.Clear();
                addMedMovList.Clear();
                editMedMovList.Clear();
                remMedMovList.Clear();
                addPatHistList.Clear();
                editPatHistList.Clear();
                remPatHistList.Clear();

                Notificator.ShowInfo("Дані успішно збережені!");
                patientDataIsSaved = true;
            }
        }

        private void medicamentsTable_SelectionChanged(object sender, EventArgs e)
        {
            deleteMedicamentLink.Enabled = agentsDozagesListLabel.Enabled = (medicamentsTable.SelectedCells.Count > 0);
        }

        private void deletePhotoButton_Click(object sender, EventArgs e)
        {
            if(Notificator.ShowActionConfirmation("Ви впевнені, що хочете видалити фото пацієнта") == System.Windows.Forms.DialogResult.Yes)
            {
                patientPhoto.Image = null;
                patientPhoto.Refresh();
                deletePhotoButton.Enabled = false;
            }
        }

        private void choosePhotoFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Файли зображення (*.jpg, *.jpeg, *.png, *.gif, *.bmp) | *.jpg; *.jpeg; *.png; *.gif; *.bmp";
            opf.Multiselect = false;
            opf.ShowDialog();

            string selectedFile = opf.FileName;
            if(!string.IsNullOrWhiteSpace(selectedFile))
            {
                using (Bitmap bm = new Bitmap(selectedFile))
                {
                    patientPhoto.Image = new Bitmap(bm);
                }
                if (dbRecord.Id > 0)
                {
                    oldPhotoFile = dbRecord.PhotoFile;
                }
                dbRecord.PhotoFile = selectedFile;
                deletePhotoButton.Enabled = true;
                patientPhoto.Refresh();
            }
        }

        private void medicamentsTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(formIsInialized)
            {
                MedicamentAssignation medAssig = medicamentsTable.Rows[e.RowIndex].DataBoundItem as MedicamentAssignation;

                if(medicamentsTable.Rows[e.RowIndex].Cells[1].Value != null)
                {
                    if (e.ColumnIndex == 2)
                    {
                        decimal dozage = Convert.ToDecimal(medicamentsTable.Rows[e.RowIndex].Cells[2].Value);
                        medAssig.Dozage = dozage;
                        int days = Convert.ToInt32(medicamentsTable.Rows[e.RowIndex].Cells[3].Value);
                        medicamentsTable.Rows[e.RowIndex].Cells[4].Value = medAssig.YearDozage;
                    }
                    if (e.ColumnIndex == 3)
                    {
                        decimal dozage = Convert.ToDecimal(medicamentsTable.Rows[e.RowIndex].Cells[2].Value);
                        int days = Convert.ToInt32(medicamentsTable.Rows[e.RowIndex].Cells[3].Value);
                        medAssig.Days = days;
                        medicamentsTable.Rows[e.RowIndex].Cells[4].Value = medAssig.YearDozage;
                    }
                    medicamentsTable.Refresh();
                }

                if(medAssig.Id > 0)
                {
                    editMedAssignList.Add(medAssig);
                }
            }
        }

        private void medicamentsTable_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Notificator.ShowError(e.Exception.Message + "\nStack trace: " + e.Exception.StackTrace);
        }

        private void medicamentMovementTable_SelectionChanged(object sender, EventArgs e)
        {
            deleteMedicamentMovementLink.Enabled = printMedicamentMovementLink.Enabled = (medicamentMovementTable.SelectedCells.Count > 0);
        }

        private void analizesTable_SelectionChanged(object sender, EventArgs e)
        {
            deleteAnalizeLink.Enabled = (analizesTable.SelectedCells.Count > 0);
        }

        private void addAnalizeLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PatientAnalyze newPatAn = new PatientAnalyze();
            newPatAn.AnalizeDate = DateTime.Now;

            if (dbRecord.Id > 0)
            {
                newPatAn.PatientId = dbRecord.Id;
            }

            analizesList.Add(newPatAn);
            addPatAnList.Add(newPatAn);
            analizesTable.Refresh();
        }

        private void addMedicamentMovementLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MedicamentMovement newMedMov = new MedicamentMovement();
            newMedMov.MovementOperationDate = DateTime.Now;

            if (dbRecord.Id > 0)
            {
                newMedMov.PatientId = dbRecord.Id;
            }

            medicamentMovementList.Add(newMedMov);
            filteredMedicamentMovementList.Add(newMedMov);
            addMedMovList.Add(newMedMov);
            medicamentMovementTable.Refresh();
        }

        private void medicamentsTable_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = e.RowIndex; i <= (e.RowIndex + e.RowCount - 1); i++)
                medicamentsTable.Rows[i].Cells[0].Value = (i + 1).ToString();
        }

        private void medicamentMovementTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (formIsInialized)
            {
                if (e.ColumnIndex == 2 && e.RowIndex >= 0)
                {
                    MedicamentMovement medMov = medicamentMovementTable.Rows[e.RowIndex].DataBoundItem as MedicamentMovement;
                    MedicamentSelectForm msf = new MedicamentSelectForm(agentsList, true);
                    msf.ShowDialog();
                    if (msf.SelectedMedicament != null)
                    {
                        medMov.MedicamentId = msf.SelectedMedicament.Id;
                        medMov.Medicament = msf.SelectedMedicament;
                        medMov.DayDozage = medicamentAssignationList.Where(p => p.AssignMedicament.Id == msf.SelectedMedicament.Id).First().Dozage;
                        medicamentMovementTable.Refresh();
                    }
                }
            }
        }

        private void medicamentsTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(formIsInialized)
            {
                if(e.ColumnIndex == 1 && e.RowIndex >= 0)
                {
                    MedicamentAssignation medAssign = medicamentsTable.Rows[e.RowIndex].DataBoundItem as MedicamentAssignation;
                    MedicamentSelectForm asf = new MedicamentSelectForm(agentsList, false);
                    asf.ShowDialog();
                    if (asf.SelectedMedicament != null)
                    {
                        if (medAssign.AssignMedicament != null)
                        {
                            agentsList.Remove(medAssign.AssignMedicament.Id);
                        }
                        medAssign.MedicamentId = asf.SelectedMedicament.Id;
                        medAssign.AssignMedicament = asf.SelectedMedicament;
                        agentsList.Add(asf.SelectedMedicament.Id);
                        medicamentsTable.Refresh();
                    }
                }
            }
        }

        private void analizesTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (formIsInialized)
            {
                if (e.ColumnIndex == 2 && e.RowIndex >= 0)
                {
                    PatientAnalyze patAnalize = analizesTable.Rows[e.RowIndex].DataBoundItem as PatientAnalyze;
                    AnalyzeSelectForm asf = new AnalyzeSelectForm();
                    asf.ShowDialog();
                    if (asf.SelectedAnalyze != null)
                    {
                        patAnalize.AnalyzeId = asf.SelectedAnalyze.Id;
                        patAnalize.Analyze = asf.SelectedAnalyze;
                        analizesTable.Refresh();
                    }
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            deleteHistoryRecord.Visible = (dataGridView1.SelectedCells.Count > 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(histRecMsg.Text))
            {
                Notificator.ShowError("Введіть текст запису");
                return;
            }

            PatientHistoryRecord newPatHistRec = new PatientHistoryRecord();
            newPatHistRec.Description = histRecMsg.Text;
            newPatHistRec.RecordDate = histRecDate.Value;

            patientHistoryRecordsList.Add(newPatHistRec);
            addPatHistList.Add(newPatHistRec);

            if (dbRecord.Id > 0)
            {
                newPatHistRec.PatientId = dbRecord.Id;
            }

            dataGridView1.Refresh();
        }

        private void medicamentMovementTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (formIsInialized)
            {
                MedicamentMovement med = medicamentMovementTable.Rows[e.RowIndex].DataBoundItem as MedicamentMovement;

                if (e.ColumnIndex == 3 && e.RowIndex >= 0 && med.Medicament != null)
                {
                    medicamentMovementTable.Refresh();
                }

                if (med.Id > 0)
                {
                    editMedMovList.Add(med);
                }

                if(med.Medicament != null)
                    recalculateYearDozage();
            }
        }

        private void applyYearFilterToMovements()
        {
            List<MedicamentMovement> fList = medicamentMovementList
                    .Where(p => p.MovementOperationDate.Year == (int)medicamentsReportYearBox.SelectedValue).ToList();
            filteredMedicamentMovementList = new BindingList<MedicamentMovement>(fList);
            medicamentMovementTable.DataSource = filteredMedicamentMovementList;
            medicamentMovementTable.Refresh();
            
            recalculateYearDozage();
        }

        private void recalculateYearDozage()
        {
            List<MedicamentYearSumDozage> sumList = medicamentMovementList
                .Where(p => p.MovementOperationDate.Year == (int)medicamentsReportYearBox.SelectedValue)
                .GroupBy(p => new { p.Medicament, p.DayDozage})
                .Select(p => new MedicamentYearSumDozage { Medicament = p.Key.Medicament, DayDoze = p.Key.DayDozage, TabsSum = p.Sum(c => c.MedicamentNum) }).ToList();
            medicamentsSumDozageList = new BindingList<MedicamentYearSumDozage>(sumList);
            medicamentsSumDozageTable.DataSource = medicamentsSumDozageList;
            medicamentsSumDozageTable.Refresh();
        }

        private void analizesTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (formIsInialized)
            {
                PatientAnalyze med = analizesTable.Rows[e.RowIndex].DataBoundItem as PatientAnalyze;

                if (med.Id > 0)
                {
                    editPatAnList.Add(med);
                }
            }
        }

        private void deleteAnalizeLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int selectedRow = analizesTable.SelectedCells[0].RowIndex;
            PatientAnalyze remPAtAnElem = analizesTable.Rows[selectedRow].DataBoundItem as PatientAnalyze;

            if (remPAtAnElem.Analyze != null)
            {
                if (Notificator.ShowActionConfirmation("Ви впевнені, що хочете цей запис?")
                    != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }
            }

            // Если мы удаляем из списка новосозданых элементов - помечам его отрицательным ID (чтобы найти)
            if (remPAtAnElem.Id == 0)
            {
                remPAtAnElem.Id = -1;
                addPatAnList.Remove(remPAtAnElem);
            }
            else
            {
                editPatAnList.Remove(remPAtAnElem);
                remPatAnList.Add(remPAtAnElem);
            }

            analizesTable.Rows.RemoveAt(selectedRow);
            analizesTable.Refresh();
        }

        private void deleteMedicamentMovementLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int selectedRow = medicamentMovementTable.SelectedCells[0].RowIndex;
            MedicamentMovement remMedMovElem = medicamentMovementTable.Rows[selectedRow].DataBoundItem as MedicamentMovement;

            if(remMedMovElem.Medicament != null)
            {
                if(Notificator.ShowActionConfirmation("Ви впевнені, що хочете цей запис?") 
                    != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }
            }

            // Если мы удаляем из списка новосозданых элементов - помечам его отрицательным ID (чтобы найти)
            if (remMedMovElem.Id == 0)
            {
                remMedMovElem.Id = -1;
                addMedMovList.Remove(remMedMovElem);
            }
            else
            {
                editMedMovList.Remove(remMedMovElem);
                remMedMovList.Add(remMedMovElem);
            }

            medicamentMovementTable.Rows.RemoveAt(selectedRow);
            medicamentMovementList.Remove(remMedMovElem);
            filteredMedicamentMovementList.Remove(remMedMovElem);

            if (remMedMovElem.MovementOperationDate.Year == (int)medicamentsReportYearBox.SelectedValue)
            {
                applyYearFilterToMovements();
            }
            else
            {
                medicamentMovementTable.Refresh();
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (formIsInialized)
            {
                PatientHistoryRecord rec = medicamentMovementTable.Rows[e.RowIndex].DataBoundItem as PatientHistoryRecord;

                if (rec.Id > 0)
                {
                    editPatHistList.Add(rec);
                }
            }
        }

        private void deleteHistoryRecord_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int selectedRow = dataGridView1.SelectedCells[0].RowIndex;
            PatientHistoryRecord remPatHistRecElem = dataGridView1.Rows[selectedRow].DataBoundItem as PatientHistoryRecord;

            if (Notificator.ShowActionConfirmation("Ви впевнені, що хочете цей запис?")
                    != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            // Если мы удаляем из списка новосозданых элементов - помечам его отрицательным ID (чтобы найти)
            if (remPatHistRecElem.Id == 0)
            {
                remPatHistRecElem.Id = -1;
                addPatHistList.Remove(remPatHistRecElem);
            }
            else
            {
                editPatHistList.Remove(remPatHistRecElem);
                remPatHistList.Add(remPatHistRecElem);
            }

            dataGridView1.Rows.RemoveAt(selectedRow);
            dataGridView1.Refresh();
        }

        private void medicamentsReportYearBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if(formIsInialized)
            {
                applyYearFilterToMovements();
            }
        }

        private void isDeadcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            label13.Enabled = dateOfDeath.Enabled = isDeadcheckBox.Checked;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            List<AgentDozage> selectedAgentDozages = new List<AgentDozage>();
            foreach (var item in medicamentAssignationList)
                foreach(var agent in item.AssignMedicament.AgentDozages)
                    selectedAgentDozages.Add(new AgentDozage 
                                                { 
                                                    Agent = agent.Agent,
                                                    Dozage = agent.Dozage * item.Dozage, 
                                                    DozageMeter = agent.DozageMeter 
                                                });

            selectedAgentDozages = selectedAgentDozages.GroupBy(t => new { t.Agent, t.DozageMeter })
                .Select(t => new AgentDozage 
                { 
                    Agent = t.Key.Agent, Dozage = t.Sum(m => m.Dozage), DozageMeter = t.Key.DozageMeter 
                }).ToList();

            MedAgentDozagesForm af = new MedAgentDozagesForm(selectedAgentDozages);
            af.ShowDialog();
        }
    }
}
