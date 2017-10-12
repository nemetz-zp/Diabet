using Diabet.DAL;
using Diabet.Helpers;
using Diabet.Models;
using Diabet.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;

namespace Diabet
{
    public partial class ExcelMedicamentNeedForm : Form
    {
        private const int EXCEL_START_ROW = 5;
        private List<YearMedicamentNeed> medicamentsList;
        private int reportYear;
        private volatile bool workWasCanceled;

        public ExcelMedicamentNeedForm(int reportYear, List<YearMedicamentNeed> medicamentsList)
        {
            InitializeComponent();
            this.reportYear = reportYear;
            this.medicamentsList = medicamentsList;
            this.workWasCanceled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string templateFilePath = System.IO.Path.Combine(PathFinder.GetProgramPath(),
              "template",
              "template_yearneed.xls");

            if (!System.IO.File.Exists(templateFilePath))
            {
                Notificator.ShowError("Файл шаблону відсутній! Формування звіту неможливе!");
                return;
            }

            Excel.Application xlApp = new Excel.Application();
            if (xlApp == null)
            {
                Notificator.ShowError("Помилка при завантаженні екземпляру Excel");
                return;
            }
            backgroundWorker1.ReportProgress(2);

            xlApp.DisplayAlerts = false;

            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(templateFilePath);
            if (xlWorkbook == null)
            {
                xlApp.Quit();
                xlApp = null;
                Notificator.ShowError("Помилка при завантаженні файлу-шаблону");
                return;
            }
            backgroundWorker1.ReportProgress(4);

            Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkbook.Worksheets[1];
            if (xlWorkSheet == null)
            {
                xlWorkbook.Close(0);
                xlApp.Quit();
                xlApp = null;
                Notificator.ShowError("Помилка при завантаженні файлу-шаблону");
                return;
            }
            backgroundWorker1.ReportProgress(6);

            List<MedicamentAssignation> originalAssignation = new List<MedicamentAssignation>();
            using (DiabetContext dc = new DiabetContext())
            {
                originalAssignation = dc.MedicamentAssigantions
                    .Include(t => t.AssignMedicament)
                    .Include(t => t.AssignMedicament.AgentDozages)
                    .Include(t => t.AssignMedicament.AgentDozages.Select(m => m.Agent))
                    .Include(t => t.AssignMedicament.AgentDozages.Select(m => m.DozageMeter))
                    .Include(t => t.Patient)
                    .Include(t => t.Patient.PatientCommune)
                    .Include(t => t.AssignMedicament.FullName)
                    .Include(t => t.AssignMedicament.MedicamentType)
                    .ToList();
            }
            backgroundWorker1.ReportProgress(8);

            // Заменяем медикаменты, который требуется изменить
            List<YearMedicamentNeed> medicamentsToReplace = medicamentsList.Where(t => t.OldMedNameValue != null).ToList();
            if (medicamentsToReplace.Count > 0)
            {
                foreach (var item in originalAssignation)
                {
                    foreach (var item2 in medicamentsToReplace)
                        if (item.AssignMedicament.Equals(item2.OldMedNameValue))
                        {
                            item.AssignMedicament = item2.MedName;
                        }
                }
            }
            backgroundWorker1.ReportProgress(10);

            List<ExcelYearMedicamentNeedItem> medicamentsRightList = originalAssignation
                .GroupBy(t => new { t.Patient.PatientCommune, t.AssignMedicament })
                .Select(t => new ExcelYearMedicamentNeedItem
                {
                    Commune = t.Key.PatientCommune,
                    Medicament = t.Key.AssignMedicament,
                    NumOfTablets = t.Sum(m => m.YearDozage)
                }).ToList();
            backgroundWorker1.ReportProgress(12);

            var medicamentsGroupedByCommuneList = medicamentsRightList
                .GroupBy(t => t.Commune)
                .Select(t => new
                {
                    Commune = t.Key,
                    Medicament = t.Select(m => new ExcelYearMedicamentNeedItem
                    {
                        Commune = t.Key,
                        Medicament = m.Medicament,
                        NumOfTablets = m.NumOfTablets
                    }).ToList()
                }).ToList();
            backgroundWorker1.ReportProgress(14);

            int communeGroupsCount = medicamentsGroupedByCommuneList.Count();

            int currentWritePosition = EXCEL_START_ROW;
            string numColumnLetter = "A";
            string nameColumnLetter = "B";
            string tabletsNumLetter = "C";
            string packageNumColumnLetter = "D";
            string priceColumnLetter = "E";
            string sumColumnLetter = "F";

            try
            {
                xlWorkSheet.get_Range("A2").Value = "РІЧНА ПОТРЕБА В МЕДИКАМЕНТАХ НА " + reportYear + " РІК";
                Dictionary<Medicament, MedicamentTotalsForExcel> medicamentTotals = new Dictionary<Medicament, MedicamentTotalsForExcel>();
                int progressRest = 90 - Convert.ToInt32(progressBar1.Value);
                double progressStepForCommune = (progressRest * 1.0) / communeGroupsCount;
                for (int i = 0; i < communeGroupsCount; i++, currentWritePosition++)
                {
                    if(workWasCanceled)
                    {
                        throw new Exception("Формування звіту було відмінене користувачем!");
                    }

                    // Выводи данные (название) громады
                    xlWorkSheet.get_Range(numColumnLetter + currentWritePosition, sumColumnLetter + currentWritePosition).Merge();
                    xlWorkSheet.get_Range(numColumnLetter + currentWritePosition).Font.Bold = true;
                    xlWorkSheet.get_Range(numColumnLetter + currentWritePosition).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    xlWorkSheet.get_Range(numColumnLetter + currentWritePosition).Value = medicamentsGroupedByCommuneList[i].Commune.ToString();

                    // Выводим список используемых медикаментов пациентами-жителями громады
                    int medicamentsCount = medicamentsGroupedByCommuneList[i].Medicament.Count();
                    currentWritePosition++;
                    for (int j = 0; j < medicamentsCount; j++, currentWritePosition++)
                    {
                        if (workWasCanceled)
                        {
                            throw new Exception("Формування звіту було відмінене користувачем!");
                        }

                        xlWorkSheet.get_Range(numColumnLetter + currentWritePosition).Value = (j + 1).ToString();
                        xlWorkSheet.get_Range(nameColumnLetter + currentWritePosition).Value = medicamentsGroupedByCommuneList[i].Medicament[j].Medicament.ToString();
                        xlWorkSheet.get_Range(tabletsNumLetter + currentWritePosition).Value = medicamentsGroupedByCommuneList[i].Medicament[j].NumOfTablets.ToString();
                        xlWorkSheet.get_Range(packageNumColumnLetter + currentWritePosition).Value = medicamentsGroupedByCommuneList[i].Medicament[j].numOfPackages.ToString();
                        xlWorkSheet.get_Range(priceColumnLetter + currentWritePosition).Value = medicamentsGroupedByCommuneList[i].Medicament[j].Medicament.Price.ToString();
                        xlWorkSheet.get_Range(sumColumnLetter + currentWritePosition).Value = "=" + packageNumColumnLetter + currentWritePosition + "*" + priceColumnLetter + currentWritePosition;

                        // Записываем адреса ячеек с суммами по медикаментам
                        if (!medicamentTotals.ContainsKey(medicamentsGroupedByCommuneList[i].Medicament[j].Medicament))
                        {
                            medicamentTotals.Add(medicamentsGroupedByCommuneList[i].Medicament[j].Medicament,
                                new MedicamentTotalsForExcel());
                        }

                        medicamentTotals[medicamentsGroupedByCommuneList[i].Medicament[j].Medicament]
                            .MedicamentTabletCells.Add(tabletsNumLetter + currentWritePosition);
                        medicamentTotals[medicamentsGroupedByCommuneList[i].Medicament[j].Medicament]
                            .MedicamentPackageCells.Add(packageNumColumnLetter + currentWritePosition);
                        medicamentTotals[medicamentsGroupedByCommuneList[i].Medicament[j].Medicament]
                            .Price = medicamentsGroupedByCommuneList[i].Medicament[j].Medicament.Price;
                    }

                    StringBuilder sb = new StringBuilder();
                    sb.Append("=");
                    for (int k = medicamentsCount; k > 0; k--)
                    {
                        sb.Append((sumColumnLetter + (currentWritePosition - k)) + "+");
                    }

                    xlWorkSheet.get_Range(nameColumnLetter + currentWritePosition ).Value = "ВСЬОГО";
                    xlWorkSheet.get_Range(nameColumnLetter + currentWritePosition).Font.Bold = true;
                    xlWorkSheet.get_Range(sumColumnLetter + currentWritePosition).Value = sb.ToString().Substring(0, sb.ToString().Count() - 1);
                    xlWorkSheet.get_Range(sumColumnLetter + currentWritePosition).Font.Bold = true;

                    int progressChange = Convert.ToInt32(progressRest + progressStepForCommune);
                    backgroundWorker1.ReportProgress(progressChange);
                }

                // Выводим итоги
                currentWritePosition++;
                xlWorkSheet.get_Range(numColumnLetter + currentWritePosition, sumColumnLetter + currentWritePosition).Merge();
                xlWorkSheet.get_Range(numColumnLetter + currentWritePosition).Font.Bold = true;
                xlWorkSheet.get_Range(numColumnLetter + currentWritePosition).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                xlWorkSheet.get_Range(numColumnLetter + currentWritePosition).Value = "ЗАГАЛЬНА ПОТРЕБА";
                currentWritePosition++;
                foreach (Medicament key in medicamentTotals.Keys)
                {
                    if (workWasCanceled)
                    {
                        throw new Exception("Формування звіту було відмінене користувачем!");
                    }

                    xlWorkSheet.get_Range(nameColumnLetter + currentWritePosition).Value = key.ToString();
                    xlWorkSheet.get_Range(nameColumnLetter + currentWritePosition).Font.Bold = true;
                    xlWorkSheet.get_Range(tabletsNumLetter + currentWritePosition).Value = medicamentTotals[key].GetTabletsSumFormula();
                    xlWorkSheet.get_Range(tabletsNumLetter + currentWritePosition).Font.Bold = true;
                    xlWorkSheet.get_Range(packageNumColumnLetter + currentWritePosition).Value = medicamentTotals[key].GetPackagesSumFormula();
                    xlWorkSheet.get_Range(packageNumColumnLetter + currentWritePosition).Font.Bold = true;
                    xlWorkSheet.get_Range(priceColumnLetter + currentWritePosition).Value = medicamentTotals[key].Price;
                    xlWorkSheet.get_Range(priceColumnLetter + currentWritePosition).Font.Bold = true;
                    xlWorkSheet.get_Range(sumColumnLetter + currentWritePosition).Value = "=" + (priceColumnLetter + currentWritePosition) + "*" + (packageNumColumnLetter + currentWritePosition);
                    xlWorkSheet.get_Range(sumColumnLetter + currentWritePosition).Font.Bold = true;
                    currentWritePosition++;
                }

                // Рисуем внешние границы таблицы отчета
                xlWorkSheet.get_Range(numColumnLetter + EXCEL_START_ROW, sumColumnLetter + currentWritePosition)
                    .Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight =
                     xlWorkSheet.get_Range(numColumnLetter + EXCEL_START_ROW, sumColumnLetter + currentWritePosition)
                    .Borders[Excel.XlBordersIndex.xlEdgeTop].Weight =
                     xlWorkSheet.get_Range(numColumnLetter + EXCEL_START_ROW, sumColumnLetter + currentWritePosition)
                    .Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight =
                     xlWorkSheet.get_Range(numColumnLetter + EXCEL_START_ROW, sumColumnLetter + currentWritePosition)
                    .Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = 3d;

                // Рисуем внутренние границы таблицы отчёта
                xlWorkSheet.get_Range(numColumnLetter + EXCEL_START_ROW, sumColumnLetter + currentWritePosition)
                    .Borders[Excel.XlBordersIndex.xlInsideHorizontal].Weight =
                     xlWorkSheet.get_Range(numColumnLetter + EXCEL_START_ROW, sumColumnLetter + currentWritePosition)
                    .Borders[Excel.XlBordersIndex.xlInsideVertical].Weight = 2d;

                // Подписываемся под отчётом
                currentWritePosition += 2;

                string doctorPosition = string.Empty;
                string doctorFirstName = string.Empty;
                string doctorLastName = string.Empty;
                string doctorMiddleName = string.Empty;

                using (DiabetContext dc = new DiabetContext())
                {
                    doctorPosition = dc.Settings.First().DoctorPosition;
                    doctorFirstName = dc.Settings.First().DoctorFirstName;
                    doctorLastName = dc.Settings.First().DoctorLastName;
                    doctorMiddleName = dc.Settings.First().DoctorMiddleName;
                }

                // Составитель
                xlWorkSheet.get_Range(numColumnLetter + currentWritePosition, nameColumnLetter + currentWritePosition).Merge();
                xlWorkSheet.get_Range(numColumnLetter + currentWritePosition).Value = "Склав:";
                xlWorkSheet.get_Range(numColumnLetter + currentWritePosition).Font.Bold = true;
                xlWorkSheet.get_Range(numColumnLetter + currentWritePosition).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

                currentWritePosition++;
                xlWorkSheet.get_Range(numColumnLetter + currentWritePosition, nameColumnLetter + currentWritePosition).Merge();
                xlWorkSheet.get_Range(numColumnLetter + currentWritePosition).Value = doctorPosition;
                xlWorkSheet.get_Range(numColumnLetter + currentWritePosition).Font.Bold = true;
                xlWorkSheet.get_Range(numColumnLetter + currentWritePosition).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

                xlWorkSheet.get_Range(priceColumnLetter + currentWritePosition, sumColumnLetter + currentWritePosition).Merge();
                xlWorkSheet.get_Range(priceColumnLetter + currentWritePosition).Value = string.Format("{1}.{2}. {0}", doctorLastName, doctorFirstName.Substring(0, 1), doctorMiddleName.Substring(0, 1));
                xlWorkSheet.get_Range(priceColumnLetter + currentWritePosition).Font.Bold = true;
                xlWorkSheet.get_Range(priceColumnLetter + currentWritePosition).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

                // Дата составления
                currentWritePosition += 2;
                xlWorkSheet.get_Range(numColumnLetter + currentWritePosition, nameColumnLetter + currentWritePosition).Merge();
                xlWorkSheet.get_Range(numColumnLetter + currentWritePosition).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                xlWorkSheet.get_Range(numColumnLetter + currentWritePosition).Value = string.Format("Дата складення: {0}", DateTime.Now.ToShortDateString());

                string filenameToSave = "Потреба медикаменти на " + reportYear + " рік.xls";
                string saveFilePath = System.Configuration.ConfigurationManager.AppSettings["ReportsSaveDirectory"];
                xlWorkbook.SaveAs(System.IO.Path.Combine(saveFilePath, filenameToSave));
                backgroundWorker1.ReportProgress(100);
                workWasCanceled = false;
            }
            catch (Exception ex)
            {
                Notificator.ShowError(ex.Message);
            }
            finally
            {
                xlWorkbook.Close(0);
                xlApp.Quit();
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Increment(e.ProgressPercentage);
            label1.Text = e.ProgressPercentage + "%";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(!workWasCanceled)
                Notificator.ShowInfo("Формування звіту завершено!");
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            workWasCanceled = true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                workWasCanceled = true;
                e.Cancel = true;
                return;
            }

            base.OnFormClosing(e);
        }
    }
}
