using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Diabet.View;

namespace Diabet
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            using (DAL.DiabetContext dc = new DAL.DiabetContext())
            {
                Models.ProgramSettings ps = dc.Settings.First();
                doctorFNameBox.Text = ps.DoctorFirstName;
                doctorLNameBox.Text = ps.DoctorLastName;
                doctorMNameBox.Text = ps.DoctorMiddleName;
                doctorPositionBox.Text = ps.DoctorPosition;
                hospitalNameBox.Text = ps.HospitalFullName;
                adressBox.Text = ps.HospitalAdress;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(doctorFNameBox.Text))
            {
                Notificator.ShowError("Ви не вказали ім'я лікаря");
                return;
            }
            if (string.IsNullOrWhiteSpace(doctorLNameBox.Text))
            {
                Notificator.ShowError("Ви не вказали прізвище лікаря");
                return;
            }
            if (string.IsNullOrWhiteSpace(doctorMNameBox.Text))
            {
                Notificator.ShowError("Ви не вказали по-батькові лікаря");
                return;
            }
            if (string.IsNullOrWhiteSpace(doctorPositionBox.Text))
            {
                Notificator.ShowError("Ви не вказали посаду лікаря");
                return;
            }
            if (string.IsNullOrWhiteSpace(hospitalNameBox.Text))
            {
                Notificator.ShowError("Ви не вказали назву лікувального закладу");
                return;
            }
            if (string.IsNullOrWhiteSpace(adressBox.Text))
            {
                Notificator.ShowError("Ви не вказали адресу лікувального закладу");
                return;
            }

            using(DAL.DiabetContext dc = new DAL.DiabetContext())
            {
                Models.ProgramSettings ps = dc.Settings.First();
                ps.DoctorFirstName = doctorFNameBox.Text.Trim();
                ps.DoctorLastName = doctorLNameBox.Text.Trim();
                ps.DoctorMiddleName = doctorMNameBox.Text.Trim();
                ps.DoctorPosition = doctorPositionBox.Text.Trim();
                ps.HospitalFullName = hospitalNameBox.Text.Trim();
                ps.HospitalAdress = adressBox.Text.Trim();
                dc.SaveChanges();

                Notificator.ShowInfo("Дані успішно збережені!");
            }
        }
    }
}
