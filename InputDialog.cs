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

            ApplyAutoCompleteToInputDialog(treeView);
        }

        public FrmInputDialog(Command cmd, TreeView treeView)
        {
            InitializeComponent();

            ApplyAutoCompleteToInputDialog(treeView);
            CopyCmdToInputDialog(cmd);
        }

        public FrmInputDialog(TreeNode node, TreeView treeView)
        {
            InitializeComponent();

            ApplyAutoCompleteToInputDialog(treeView);
            CopyCmdToInputDialog(new Command(node));
            
        }

        private void ApplyAutoCompleteToInputDialog(TreeView treeView)
        {
            cboPath.AutoCompleteCustomSource = ExtractPathInTreeView(treeView);
            cboArguments.AutoCompleteCustomSource = ExtractArgumentsInTreeView(treeView);
        }

        private void CopyCmdToInputDialog(Command cmd)
        {
            txtCmd.Text = cmd.Name;
            chkRun.Checked = cmd.Run;
            cboPath.Text = cmd.Path;
            cboArguments.Text = cmd.Arguments;
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

        public Command GetCmdSet()
        {
            Command cmd = new Command(txtCmd.Text, chkRun.Checked, cboPath.Text, cboArguments.Text);
            return cmd;
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

        private AutoCompleteStringCollection ExtractPathInTreeView(TreeView treeView)
        {
            List<Command> cmdList = GetPathListInTreeView(treeView);
            AutoCompleteStringCollection autoCompleteStringCollection = new AutoCompleteStringCollection();

            foreach (Command cmd in cmdList)
            {
                if ((cmd.Path != null) && (cmd.Path != ""))
                {
                    autoCompleteStringCollection.Add(cmd.Path);
                }
            }
            return autoCompleteStringCollection;
        }

        private AutoCompleteStringCollection ExtractArgumentsInTreeView(TreeView treeView)
        {
            List<Command> cmdList = GetPathListInTreeView(treeView);
            AutoCompleteStringCollection autoCompleteStringCollection = new AutoCompleteStringCollection();

            foreach (Command cmd in cmdList)
            {
                if ((cmd.Arguments != null) && (cmd.Arguments != ""))
                {
                    autoCompleteStringCollection.Add(cmd.Arguments);
                }
            }
            return autoCompleteStringCollection;
        }

        private List<Command> GetPathListInTreeView(TreeView treeView)
        {
            List<Command> cmdList = new List<Command>();

            foreach (TreeNode child in treeView.Nodes)
            {
                cmdList.AddRange(GetPathListOfAllNodes(child));
            }
            return cmdList;
        }

        private List<Command> GetPathListOfAllNodes(TreeNode node)
        {
            List<Command> cmdList = new List<Command>();

            if (node.Tag != null)
            {
                Command cmd = new Command(node);
                cmdList.Add(cmd);
            }

            foreach (TreeNode child in node.Nodes)
            {
                cmdList.AddRange(GetPathListOfAllNodes(child));
            }

            return cmdList;
        }
    }
}
