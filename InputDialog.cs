using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Shortcut
{
    public partial class FrmInputDialog : Form
    {
        public FrmInputDialog(TreeView treeView)
        {
            InitializeComponent();

            cboPath.AutoCompleteCustomSource = GetCmdItems(treeView, "Path");
            cboArguments.AutoCompleteCustomSource = GetCmdItems(treeView, "Arguments");
        }

        public FrmInputDialog(Dictionary<string, string> CmdSet, TreeView treeView)
        {
            InitializeComponent();

            cboPath.AutoCompleteCustomSource = GetCmdItems(treeView, "Path");
            cboArguments.AutoCompleteCustomSource = GetCmdItems(treeView, "Arguments");

            cboPath.Text = CmdSet["Path"];
            cboArguments.Text = CmdSet["Arguments"];
            txtCmd.Text = CmdSet["Cmd"];
            chkRun.Checked = (CmdSet["Run"] == "Checked");
        }

        public FrmInputDialog(string Cmd, TreeView treeView)
        {
            InitializeComponent();
            txtCmd.Text = Cmd;
        }

        private void BtnFile_Click(object sender, EventArgs e)
        {
            if (File.Exists(cboPath.Text))
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(cboPath.Text);
            }

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                cboPath.Text = openFileDialog.FileName;
            }
        }

        private void BtnFolder_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;

            if (Directory.Exists(cboPath.Text))
            {
                dialog.InitialDirectory = cboPath.Text;
            }

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                cboPath.Text = dialog.FileName; // 테스트용, 폴더 선택이 완료되면 선택된 폴더를 label에 출력
            }
        }

        public Dictionary <string, string> GetCmdSet()
        {
            Dictionary<string, string> cmdSet = new Dictionary<string, string>();
            cmdSet.Add("Cmd", txtCmd.Text);
            cmdSet.Add("Run", (chkRun.Checked) ? "Checked" : "Unchecked");
            cmdSet.Add("Path", cboPath.Text);
            cmdSet.Add("Arguments", cboArguments.Text);
            return cmdSet;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cboPath.Text = null;
            cboArguments.Text = null;
        }

        private void chkRun_CheckedChanged(object sender, EventArgs e)
        {
            grpRun.Enabled = chkRun.Checked;
        }

        private AutoCompleteStringCollection GetCmdItems(TreeView treeView, string item)
        {
            List<Dictionary<string, string>> cmdList = GetPathListInTreeView(treeView);
            AutoCompleteStringCollection autoCompleteStringCollection = new AutoCompleteStringCollection();

            foreach (Dictionary<string, string> cmd in cmdList)
            {
                if ((cmd[item] != null) && (cmd[item] != ""))
                {
                    autoCompleteStringCollection.Add(cmd[item]);
                }
            }
            return autoCompleteStringCollection;
        }

        private List<Dictionary<string, string>> GetPathListInTreeView(TreeView treeView)
        {
            List<Dictionary<string, string>> cmdList = new List<Dictionary<string, string>>();

            foreach (TreeNode child in treeView.Nodes)
            {
                cmdList.AddRange(GetPathListOfAllNodes(child));
            }
            return cmdList;
        }

        private List<Dictionary<string, string>> GetPathListOfAllNodes(TreeNode cmd)
        {
            List<Dictionary<string, string>> cmdList = new List<Dictionary<string, string>>();

            if (cmd.Tag != null)
            {
                Dictionary<string, string> cmdSet = (Dictionary<string, string>)cmd.Tag;
                cmdList.Add(cmdSet);
            }

            foreach (TreeNode child in cmd.Nodes)
            {
                cmdList.AddRange(GetPathListOfAllNodes(child));
            }

            return cmdList;
        }
    }
}
