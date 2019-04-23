using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shortcut
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
            Options_Load();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Options_Apply();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Options_Apply();
        }

        private void Options_Apply()
        {
            Properties.Settings.Default.optMinimizeToTrayAfterRun = chkMinimizeToTrayAfterRun.Checked;
            Properties.Settings.Default.optShowInTaskBar = chkShowInTaskBar.Checked;
            Properties.Settings.Default.Save();
        }

        private void Options_Load()
        {
            chkMinimizeToTrayAfterRun.Checked = Properties.Settings.Default.optMinimizeToTrayAfterRun;
            chkShowInTaskBar.Checked = Properties.Settings.Default.optShowInTaskBar;
        }

        public bool GetOption_MinimizeToTrayAfterRun()
        {
            return Properties.Settings.Default.optMinimizeToTrayAfterRun;
        }

        public bool GetOption_ShowInTaskBar()
        {
            return Properties.Settings.Default.optShowInTaskBar;
        }
    }
}
