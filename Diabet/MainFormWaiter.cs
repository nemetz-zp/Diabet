using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Diabet
{
    public static class MainFormWaiter
    {
        private static LoadingForm lf;
        private static bool IsWaitingFormClosed;
        private delegate void CloseDelegate();

        static MainFormWaiter()
        {
            lf = null;
            IsWaitingFormClosed = false;
        }

        public static void BeginWaiting(string msg)
        {
            if (lf != null)
                return;

            lf = new LoadingForm(msg);
            lf.FormClosed += (sender, args) => IsWaitingFormClosed = true;
            Thread wThread = new Thread(new ThreadStart(MainFormWaiter.ShowForm));
            wThread.IsBackground = false;
            wThread.SetApartmentState(ApartmentState.STA);
            wThread.Start();
        }

        private static void ShowForm()
        {
            Application.Run(lf);
        }

        public static void EndWaiting()
        {
            if (!IsWaitingFormClosed)
            {
                lf.Invoke(new CloseDelegate(MainFormWaiter.DirectClose));
            }
        }

        private static void DirectClose()
        {
            lf.Close();
        }
    }
}
