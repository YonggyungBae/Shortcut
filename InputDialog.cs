using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Shortcut
{
    public partial class FrmInputDialog : Form
    {
        string fullPath;
        string fullArguments;

        public FrmInputDialog(TreeView treeView)
        {
            InitializeComponent();
            ApplyAutoCompleteToInputDialog(treeView);
        }

        public FrmInputDialog(Command cmd, TreeView treeView)
        {
            InitializeComponent();

            ApplyAutoCompleteToInputDialog(treeView);
            SetCmdToDialogElements(cmd);
        }

        private void ApplyAutoCompleteToInputDialog(TreeView treeView)
        {
            cboPath.AutoCompleteCustomSource = ExtractPathInTreeView(treeView);
            cboArguments.AutoCompleteCustomSource = ExtractArgumentsInTreeView(treeView);
        }

        private void SetCmdToDialogElements(Command cmd)
        {
            txtCmd.Text = cmd.Name;
            chkRun.Checked = cmd.Run;
            cboPath.Text = cmd.Path;
            cboArguments.Text = cmd.Arguments;
            fullPath = cmd.GetAbsolutePath();
            fullArguments = cmd.GetAbsoluteArguments();
        }

        private void BtnFile_Click(object sender, EventArgs e)
        {

            if (File.Exists(fullPath))
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(fullPath);
                openFileDialog.FileName = Path.GetFileName(fullPath);
            }
            else if(Directory.Exists(fullPath))
            {
                openFileDialog.InitialDirectory = fullPath;
            }

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (fullPath != openFileDialog.FileName)
                {
                    cboPath.Text = fullPath = openFileDialog.FileName;
                }
                // else if(fullPath == openFileDialog.FileName) -> 기존의 cboPath.Text에 있던 path 내용이 #path#와 같은 상대경로 설정인 경우, Dialog에서 변경없이 OK를 눌렀을 때 상대경로를 그대로 유지하도록 함
            }
        }

        private void BtnFolder_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;

            if (File.Exists(fullPath))
            {
                dialog.InitialDirectory = Path.GetDirectoryName(fullPath);
            }
            else if (Directory.Exists(fullPath))
            {
                dialog.InitialDirectory = fullPath;
            }
            
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                if (fullPath != dialog.FileName)
                {
                    cboPath.Text = fullArguments = dialog.FileName;
                }
                // else if(fullPath == dialog.FileName) -> 기존의 cboPath.Text에 있던 path 내용이 #path#와 같은 상대경로 설정인 경우, Dialog에서 변경없이 OK를 눌렀을 때 상대경로를 그대로 유지하도록 함
            }
        }

        public Command GetCmdSet(TreeNode node)
        {
            Command cmd = new Command(txtCmd.Text, chkRun.Checked, cboPath.Text, cboArguments.Text);
            cmd.Node = node;
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
