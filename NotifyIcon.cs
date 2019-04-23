using System;
using System.Windows.Forms;

namespace Shortcut
{
    public partial class FrmMain
    {
        private void ShowForm()
        {
            FormWindowState windowState_Backup = this.WindowState;
            this.WindowState = FormWindowState.Minimized;
            if (windowState_Backup != FormWindowState.Minimized)
                this.WindowState = windowState_Backup;
            else
                this.WindowState = FormWindowState.Normal;
            Show();
            BringToFront();
        }

        private void HideForm()
        {
            Hide();
        }

        private void ToolStripMenuItem_NotifyIcon_Open_Click(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void toolStripMenuItem_NotifyIcon_Config_Click(object sender, EventArgs e)
        {
            Options options = new Options();
            options.ShowDialog();

            this.ShowInTaskbar = options.GetOption_ShowInTaskBar();
        }

        private void ToolStripMenuItem_NotifyIcon_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuNotifyIcon.Show();
            else
                ShowForm();
        }
    }
}
