using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Shortcut
{
    public partial class FrmInputDialog : Form
    {
        public FrmInputDialog()
        {
            InitializeComponent();
        }

        public FrmInputDialog(Dictionary<string, string> CmdSet)
        {
            InitializeComponent();
            txtCmd.Text = CmdSet["Cmd"];
            chkRun.Checked = (CmdSet["Run"] == "Checked");
            txtPath.Text = CmdSet["Path"];
            txtArguments.Text = CmdSet["Arguments"];
        }

        public FrmInputDialog(string Cmd)
        {
            InitializeComponent();
            txtCmd.Text = Cmd;
        }

        private void BtnFile_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtPath.Text))
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(txtPath.Text);
            }

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = openFileDialog.FileName;
            }
        }

        private void BtnFolder_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;

            if (Directory.Exists(txtPath.Text))
            {
                dialog.InitialDirectory = txtPath.Text;
            }

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                txtPath.Text = dialog.FileName; // 테스트용, 폴더 선택이 완료되면 선택된 폴더를 label에 출력
            }
        }

        public Dictionary <string, string> GetCmdSet()
        {
            Dictionary<string, string> cmdSet = new Dictionary<string, string>();
            cmdSet.Add("Cmd", txtCmd.Text);
            cmdSet.Add("Run", (chkRun.Checked) ? "Checked" : "Unchecked");
            cmdSet.Add("Path", txtPath.Text);
            cmdSet.Add("Arguments", txtArguments.Text);
            return cmdSet;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPath.Clear();
            txtArguments.Clear();
        }

        private void chkRun_CheckedChanged(object sender, EventArgs e)
        {
            grpRun.Enabled = chkRun.Checked;
        }
    }
}
