using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Shortcut
{
    public partial class FrmMain
    {
        private void ToolStripMenuItem_Add_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = TreeView.SelectedNode;  // Input Dialog가 show 되기 전에 선택 node를 backup 받아놔야 함(안그러면 InputDialog가 show되고 나면 treeview의 첫번째 node가 강제 선택됨)
            FrmInputDialog inputDialog;

            if (dragNdropPath != "")
            {
                Dictionary<string, string> cmdSet_dragNdrop = new Dictionary<string, string>();
                cmdSet_dragNdrop["Cmd"] = "";
                cmdSet_dragNdrop["Path"] = dragNdropPath;
                cmdSet_dragNdrop["Arguments"] = "";
                cmdSet_dragNdrop["Run"] = "Checked";
                inputDialog = new FrmInputDialog(cmdSet_dragNdrop);
            }
            else
            {
                inputDialog = new FrmInputDialog();
            }

            Dictionary<string, string> cmdSet = InputCmd(CmdEditType.ADD, ref inputDialog, selectedNode);

            if (cmdSet != null)
            {
                TreeNode NewCmd = new TreeNode
                {
                    Text = Name = cmdSet["Cmd"],
                    Tag = cmdSet,
                };

                NewCmd.SelectedImageKey = NewCmd.ImageKey = GetIcon(cmdSet["Path"]);

                if (selectedNode == null)
                {
                    TreeView.Nodes.Add(NewCmd);
                    NewCmd.ForeColor = topCmdColor;
                }
                else TreeView.SelectedNode.Nodes.Add(NewCmd);

                TreeView.SelectedNode.Expand();
                SaveTree(TreeView, cfgFileName);
            }
            dragNdropPath = null;
        }

        private void ToolStripMenuItem_Edit_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = TreeView.SelectedNode;
            FrmInputDialog inputDialog;

            if (selectedNode.Tag == null)
                inputDialog = new FrmInputDialog(selectedNode.Name);
            else
            {
                if (dragNdropPath != null)
                {
                    Dictionary<string, string> dragNdropCmd = new Dictionary<string, string>();
                    dragNdropCmd["Cmd"] = selectedNode.Name;
                    dragNdropCmd["Path"] = dragNdropPath;
                    dragNdropCmd["Arguments"] = null;
                    dragNdropCmd["Run"] = "Checked";
                    inputDialog = new FrmInputDialog(dragNdropCmd);
                }
                else
                    inputDialog = new FrmInputDialog((Dictionary<string, string>)selectedNode.Tag);
            }

            Dictionary<string, string> cmdSet = InputCmd(CmdEditType.EDIT, ref inputDialog, selectedNode);
            if (cmdSet != null)
            {
                selectedNode.Name = selectedNode.Text = cmdSet["Cmd"];
                selectedNode.Tag = cmdSet;
                selectedNode.SelectedImageKey = selectedNode.ImageKey = GetIcon(cmdSet["Path"]);
                SaveTree(TreeView, cfgFileName);
            }
            dragNdropPath = null;
        }

        private void ToolStripMenuItem_Del_Click(object sender, EventArgs e)
        {
            if ((TreeView.SelectedNode != null)
                && (MessageBox.Show("정말로 삭제할까요?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
            {
                TreeView.SelectedNode.Remove();
                SaveTree(TreeView, cfgFileName);
            }
        }

        private void ContextMenu_Opening(object sender, CancelEventArgs e)
        {
            bool MenuItemVisible = (TreeView.SelectedNode == null);
            toolStripMenuItem_Edit.Visible = !MenuItemVisible;
            toolStripMenuItem_Del.Visible = (!MenuItemVisible) && (dragNdropPath == null);
        }
    }
}
