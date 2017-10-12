using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Diabet.View
{
    // Показ сообщений
    public class Notificator
    {
        private static DialogResult Show(string msg, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(msg, caption, buttons, icon);
        }

        public static void ShowInfo(string msg)
        {
            Show(msg, "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowError(string msg)
        {
            Show(msg, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult ShowActionConfirmation(string msg)
        {
            return Show(msg, "Підтвердження дії", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
