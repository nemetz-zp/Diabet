using Diabet.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Diabet
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                MainFormWaiter.BeginWaiting("Програма завантажуєтся ...");
                MainForm mf = new MainForm();
                MainFormWaiter.EndWaiting();
                Application.EnableVisualStyles();
                //Application.SetCompatibleTextRenderingDefault(false);
                mf.Activate();
                Application.Run(mf);
            }
            catch(Exception ex)
            {
                Notificator.ShowError(string.Format("{0}\n{1}", ex.Message, ex.StackTrace));
            }
            finally
            {
                MainFormWaiter.EndWaiting();
            }
        }
    }
}
