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
            ToolTipSet();
        }

        private void ToolTipSet()
        {
            ToolTip_MinimizeToTrayAfterRun.SetToolTip(chkShowInTaskBar, "작업표시줄에 표시할지 여부를 결정합니다.");
            ToolTip_MinimizeToTrayAfterRun.SetToolTip(chkMinimizeToTrayAfterRun, "Command를 더블클릭하여 실행하면" + Environment.NewLine + "Shortcut이 윈도우 트레이로 최소화 됩니다.");
            ToolTip_MinimizeToTrayAfterRun.SetToolTip(chkMinimizeToTrayPressEsc, "Shortcut의 메인창에서 'Esc' 키를 누르면" + Environment.NewLine + "Shortcut이 윈도우 트레이로 최소화 됩니다.");
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            Options_Save();
            
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            Options_Save();
        }

        private void Options_Save()
        {
            Properties.Settings.Default.optShowInTaskBar = chkShowInTaskBar.Checked;
            Properties.Settings.Default.optMinimizeToTrayAfterRun = chkMinimizeToTrayAfterRun.Checked;
            Properties.Settings.Default.optMinimizeToTrayPressEsc = chkMinimizeToTrayPressEsc.Checked;

            Properties.Settings.Default.Save(); // Settings 값이 바뀌고 나면 꼭 Save() 해 주어야 함
        }

        private void Options_Load()
        {
            chkMinimizeToTrayAfterRun.Checked = Properties.Settings.Default.optMinimizeToTrayAfterRun;
            chkShowInTaskBar.Checked = Properties.Settings.Default.optShowInTaskBar;
            chkMinimizeToTrayPressEsc.Checked = Properties.Settings.Default.optMinimizeToTrayPressEsc;
        }

        public bool GetOption_ShowInTaskBar()
        {
            return Properties.Settings.Default.optShowInTaskBar;
        }

        public bool GetOption_MinimizeToTrayAfterRun()
        {
            return Properties.Settings.Default.optMinimizeToTrayAfterRun;
        }

        public bool GetOption_MinimizeToTrayPressEsc()
        {
            return Properties.Settings.Default.optMinimizeToTrayPressEsc;
        }

    }
}
