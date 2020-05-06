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
            Activate();
        }

        private void HideForm()
        {
            Hide();
        }

        private void ToolStripMenuItem_NotifyIcon_Open_Click(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void ToolStripMenuItem_NotifyIcon_Config_Click(object sender, EventArgs e)
        {
            options.ShowDialog();
            Option_Apply_ShowInTaskbar();
            Option_Apply_MinimizeToTrayClickCloseButton();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout about = new frmAbout();
            about.ShowDialog();
        }

        private void ToolStripMenuItem_NotifyIcon_Exit_Click(object sender, EventArgs e)
        {
            exitReq = true;
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
