using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Shortcut
{
    public partial class FrmMain
    {
        private void ContextMenu_Opening(object sender, CancelEventArgs e)
        {
            toolStripMenuItem_Add.Visible = false;
            toolStripMenuItem_Edit.Visible = false;
            toolStripMenuItem_Del.Visible = false;

            if (TreeView.SelectedNode == null)
            {
                toolStripMenuItem_Add.Visible = true;
            }
            else
            {
                toolStripMenuItem_Add.Visible = true;
                if (dragNdropPath == null)  // file / folder를 drag&drop할 때.
                {
                    toolStripMenuItem_Edit.Visible = true;
                    toolStripMenuItem_Del.Visible = true;
                }
            }
        }

        private void ToolStripMenuItem_Add_Click(object sender, EventArgs e)
        {
            Point positionContextmunu = TreeView.PointToClient(contextMenuTreeView.Bounds.Location);
            TreeNode NodeOver = TreeView.GetNodeAt(positionContextmunu);
            FrmInputDialog inputDialog;

            if (dragNdropPath != null)
            {
                Dictionary<string, string> cmdSet_dragNdrop = new Dictionary<string, string>();

                //if (File.Exists(dragNdropPath))
                //    cmdSet_dragNdrop["Cmd"] = Path.GetFileNameWithoutExtension(dragNdropPath);
                //else if (Directory.Exists(dragNdropPath))
                //    cmdSet_dragNdrop["Cmd"] = Path.GetDirectoryName(dragNdropPath);
                cmdSet_dragNdrop["Cmd"] = Path.GetFileNameWithoutExtension(dragNdropPath);
                cmdSet_dragNdrop["Path"] = dragNdropPath;
                cmdSet_dragNdrop["Arguments"] = "";
                cmdSet_dragNdrop["Run"] = "Checked";
                inputDialog = new FrmInputDialog(cmdSet_dragNdrop, TreeView);
            }
            else
            {
                inputDialog = new FrmInputDialog(TreeView);
            }

            Dictionary<string, string> cmdSet = InputCmd(CmdEditType.ADD, ref inputDialog, NodeOver);

            if (cmdSet != null)
            {
                TreeNode NewCmd = new TreeNode
                {
                    Text = Name = cmdSet["Cmd"],
                    Tag = cmdSet,
                };

                if (NodeOver == null)
                {
                    TreeView.Nodes.Add(NewCmd);
                    NewCmd.ForeColor = (NewCmd.Parent == null) ? topCmdColor : normalCmdColor;
                }
                else
                {
                    InsertCmd(TreeView, NodeOver, NewCmd, positionContextmunu.Y);
                    NodeOver.Expand();
                }

                string path = RemakeStringWithReplacingKeywords(cmdSet["Path"], NewCmd);
                NewCmd.SelectedImageKey = NewCmd.ImageKey = SelectIcon(path);
                SaveTree(TreeView, cfgFileName);
            }
            dragNdropPath = null;
        }

        private void ToolStripMenuItem_Edit_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = TreeView.SelectedNode;
            FrmInputDialog inputDialog;

            if (selectedNode.Tag == null)
                inputDialog = new FrmInputDialog(selectedNode.Name, TreeView);
            else
            {
                if (dragNdropPath != null)
                {
                    Dictionary<string, string> dragNdropCmd = new Dictionary<string, string>();
                    dragNdropCmd["Cmd"] = selectedNode.Name;
                    dragNdropCmd["Path"] = dragNdropPath;
                    dragNdropCmd["Arguments"] = null;
                    dragNdropCmd["Run"] = "Checked";
                    inputDialog = new FrmInputDialog(dragNdropCmd, TreeView);
                }
                else
                    inputDialog = new FrmInputDialog((Dictionary<string, string>)selectedNode.Tag, TreeView);
            }

            Dictionary<string, string> cmdSet = InputCmd(CmdEditType.EDIT, ref inputDialog, selectedNode);
            if (cmdSet != null)
            {
                selectedNode.Name = selectedNode.Text = cmdSet["Cmd"];
                selectedNode.Tag = cmdSet;

                string path = RemakeStringWithReplacingKeywords(cmdSet["Path"], selectedNode);
                selectedNode.SelectedImageKey = selectedNode.ImageKey = SelectIcon(path);
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
    }
}
