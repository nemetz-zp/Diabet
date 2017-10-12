using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Diabet.DAL;
using Diabet.View;
using Diabet.Helpers;
using Diabet.Models;
using Excel = Microsoft.Office.Interop.Excel;

namespace Diabet
{
    public partial class ExcelReportProcessForm : Form
    {
        // Столбцы отчёта без персональных данных пациентов
        private string[] columnLettersForSafeReport = { "A", "B", "C", "D", "E", "F" };

        // Столбцы отчёта с персональными данными пациентов
        private string[] columnLettersForUnsafeReport = { "A", "B", "C", "D", "E", "F", "G", "H" };

        private const int START_ROW_TO_WRITE = 5;

        private List<Patient> patientsList;

        private bool isSafeReport;

        private volatile bool isWorkCancelled;

        public ExcelReportProcessForm(List<Patient> patientsList, bool isSafeReport)
        {
            isWorkCancelled = false;
            InitializeComponent();
            this.patientsList = patientsList;
            this.isSafeReport = isSafeReport;

            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Определяем файл-шаблон
            string templateFilePath = string.Empty;
            double currentProgress = 0.0;

            if(isSafeReport)
            {
                templateFilePath = System.IO.Path.Combine(PathFinder.GetProgramPath(),
                  "template",
                  "template_printpatientssafe.xls");
            }
            else
            {
                templateFilePath = System.IO.Path.Combine(PathFinder.GetProgramPath(),
                  "template",
                  "template_printpatientsunsafe.xls");
            }

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
            backgroundWorker1.ReportProgress(Convert.ToInt32(currentProgress += 2));

            xlApp.DisplayAlerts = false;

            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(templateFilePath);
            if (xlWorkbook == null)
            {
                xlApp.Quit();
                xlApp = null;
                Notificator.ShowError("Помилка при завантаженні файлу-шаблону");
                return;
            }
            backgroundWorker1.ReportProgress(Convert.ToInt32(currentProgress += 4));

            Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkbook.Worksheets[1];
            if (xlWorkSheet == null)
            {
                xlWorkbook.Close(0);
                xlApp.Quit();
                xlApp = null;
                Notificator.ShowError("Помилка при завантаженні файлу-шаблону");
                return;
            }

            try
            {
                int patientsCount = patientsList.Count;
                int currentWritePos = START_ROW_TO_WRITE;
                
                // Шаг изменения прогресса
                double progressStep = (100 - currentProgress) / patientsCount;

                using (DiabetContext dc = new DiabetContext())
                {
                    for (int i = 0; i < patientsCount; i++, currentWritePos++)
                    {
                        if(isWorkCancelled)
                        {
                            throw new Exception("Операція відмінена користувачем!");
                        }

                        dc.Patients.Attach(patientsList[i]);

                        string sex = (patientsList[i].Sex == Sex.Male) ? "чол." : "жін.";

                        // Формируем список назначеных пациенту медикаментов
                        string[] medicamentsList = new string[patientsList[i].Medicaments.Count];
                        for (int j = 0; j < patientsList[i].Medicaments.Count; j++)
                        {
                            medicamentsList[j] = string.Format("{0}) {1}", (j + 1),
                                patientsList[i].Medicaments.ElementAt(j).AssignMedicament.FullName);
                        }

                        if (isSafeReport)
                        {
                            xlWorkSheet.get_Range(columnLettersForSafeReport[0] + currentWritePos).Value = (i + 1).ToString();
                            xlWorkSheet.get_Range(columnLettersForSafeReport[1] + currentWritePos).Value = sex;
                            xlWorkSheet.get_Range(columnLettersForSafeReport[2] + currentWritePos).Value = patientsList[i].BirthDate.ToShortDateString();
                            xlWorkSheet.get_Range(columnLettersForSafeReport[3] + currentWritePos).Value = patientsList[i].PatientCommune.ToString();
                            xlWorkSheet.get_Range(columnLettersForSafeReport[4] + currentWritePos).Value = patientsList[i].DiabetTypeStr;
                            xlWorkSheet.get_Range(columnLettersForSafeReport[5] + currentWritePos).Value = string.Join("\n", medicamentsList);
                        }
                        else
                        {
                            xlWorkSheet.get_Range(columnLettersForUnsafeReport[0] + currentWritePos).Value = (i + 1).ToString();
                            xlWorkSheet.get_Range(columnLettersForUnsafeReport[1] + currentWritePos).Value = patientsList[i].FullName;
                            xlWorkSheet.get_Range(columnLettersForUnsafeReport[2] + currentWritePos).Value = sex;
                            xlWorkSheet.get_Range(columnLettersForUnsafeReport[3] + currentWritePos).Value = patientsList[i].BirthDate.ToShortDateString();
                            xlWorkSheet.get_Range(columnLettersForUnsafeReport[4] + currentWritePos).Value = patientsList[i].PatientCommune.ToString();
                            xlWorkSheet.get_Range(columnLettersForUnsafeReport[5] + currentWritePos).Value = patientsList[i].Adress;
                            xlWorkSheet.get_Range(columnLettersForUnsafeReport[6] + currentWritePos).Value = patientsList[i].DiabetTypeStr;
                            xlWorkSheet.get_Range(columnLettersForUnsafeReport[7] + currentWritePos).Value = string.Join("\n", medicamentsList);
                        }

                        backgroundWorker1.ReportProgress(Convert.ToInt32(currentProgress += progressStep));
                    }

                    // Определяем буквы последних двух столбцов
                    string firstRangeLetter = string.Empty;
                    string lastRangeLetter = string.Empty;
                    if (isSafeReport)
                    {
                        firstRangeLetter = columnLettersForSafeReport[columnLettersForSafeReport.Count() - 2];
                        lastRangeLetter = columnLettersForSafeReport[columnLettersForSafeReport.Count() - 1];
                    }
                    else
                    {
                        firstRangeLetter = columnLettersForUnsafeReport[columnLettersForUnsafeReport.Count() - 2];
                        lastRangeLetter = columnLettersForUnsafeReport[columnLettersForUnsafeReport.Count() - 1];
                    }

                    // Рисуем внешние границы таблицы отчета
                    currentWritePos--;
                    xlWorkSheet.get_Range(columnLettersForSafeReport[0] + START_ROW_TO_WRITE, lastRangeLetter + currentWritePos)
                        .Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight =
                         xlWorkSheet.get_Range(columnLettersForSafeReport[0] + START_ROW_TO_WRITE, lastRangeLetter + currentWritePos)
                        .Borders[Excel.XlBordersIndex.xlEdgeTop].Weight =
                         xlWorkSheet.get_Range(columnLettersForSafeReport[0] + START_ROW_TO_WRITE, lastRangeLetter + currentWritePos)
                        .Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight =
                         xlWorkSheet.get_Range(columnLettersForSafeReport[0] + START_ROW_TO_WRITE, lastRangeLetter + currentWritePos)
                        .Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = 3d;

                    // Рисуем внутренние границы таблицы отчёта
                    xlWorkSheet.get_Range(columnLettersForSafeReport[0] + START_ROW_TO_WRITE, lastRangeLetter + currentWritePos)
                        .Borders[Excel.XlBordersIndex.xlInsideHorizontal].Weight =
                         xlWorkSheet.get_Range(columnLettersForSafeReport[0] + START_ROW_TO_WRITE, lastRangeLetter + currentWritePos)
                        .Borders[Excel.XlBordersIndex.xlInsideVertical].Weight = 2d;

                    string doctorPosition = dc.Settings.First().DoctorPosition;
                    string doctorLastName = dc.Settings.First().DoctorLastName;
                    string doctorFirstName = dc.Settings.First().DoctorFirstName;
                    string doctorMiddleName = dc.Settings.First().DoctorMiddleName;

                    currentWritePos += 2;
                    xlWorkSheet.get_Range(columnLettersForSafeReport[0] + currentWritePos,
                        columnLettersForSafeReport[2] + currentWritePos).Merge();
                    xlWorkSheet.get_Range(columnLettersForSafeReport[0] + currentWritePos).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    xlWorkSheet.get_Range(columnLettersForSafeReport[0] + currentWritePos).Font.Bold = true;
                    xlWorkSheet.get_Range(columnLettersForSafeReport[0] + currentWritePos++).Value = "СКЛАВ:";
                    
                    xlWorkSheet.get_Range(columnLettersForSafeReport[0] + currentWritePos,
                        columnLettersForSafeReport[2] + currentWritePos).Merge();
                    xlWorkSheet.get_Range(columnLettersForSafeReport[0] + currentWritePos).Font.Bold = true;
                    xlWorkSheet.get_Range(columnLettersForSafeReport[0] + currentWritePos).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    xlWorkSheet.get_Range(columnLettersForSafeReport[0] + currentWritePos).Value = doctorPosition;

                    xlWorkSheet.get_Range(firstRangeLetter + currentWritePos, lastRangeLetter + currentWritePos).Merge();
                    xlWorkSheet.get_Range(firstRangeLetter + currentWritePos).Font.Bold = true;
                    xlWorkSheet.get_Range(firstRangeLetter + currentWritePos).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    xlWorkSheet.get_Range(firstRangeLetter + currentWritePos).Value
                        = string.Format("{0}.{1}. {2}", doctorFirstName.Substring(0, 1), doctorMiddleName.Substring(0, 1), doctorLastName);

                    string directoryToSave = System.Configuration.ConfigurationManager.AppSettings["ReportsSaveDirectory"];
                    string fileName = "Звіт по пацієнтах";

                    xlWorkbook.SaveAs(System.IO.Path.Combine(directoryToSave, fileName + ".xls"));
                    Notificator.ShowInfo("Звіт успішно сформовано!");
                }
            }
            catch(Exception ex)
            {
                Notificator.ShowError(ex.Message);
                return;
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
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isWorkCancelled = true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if(backgroundWorker1.IsBusy)
            {
                isWorkCancelled = true;
                e.Cancel = true;
                return;
            }

            base.OnFormClosing(e);
        }
    }
}
