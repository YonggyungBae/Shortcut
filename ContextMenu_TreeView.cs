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
                Command cmdDragDrop = new Command(Path.GetFileNameWithoutExtension(dragNdropPath), true, dragNdropPath, null);
                inputDialog = new FrmInputDialog(cmdDragDrop, TreeView);
            }
            else
            {
                inputDialog = new FrmInputDialog(TreeView);
            }

            Command cmd = InputCmd(CmdEditType.ADD, ref inputDialog, NodeOver);
            if (cmd != null)
            {
                TreeNode newNode = cmd.ToTreeNode();

                if (NodeOver == null)
                {
                    TreeView.Nodes.Add(newNode);
                    newNode.ForeColor = (newNode.Parent == null) ? topCmdColor : normalCmdColor;
                }
                else
                {
                    InsertCmd(TreeView, NodeOver, newNode, positionContextmunu.Y);
                    NodeOver.Expand();
                }

                newNode.SelectedImageKey = newNode.ImageKey = SelectIcon(cmd.GetAbsolutePath(NodeOver));
                SaveTree(TreeView, cfgFileName);
            }
            dragNdropPath = null;
        }

        private void ToolStripMenuItem_Edit_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = TreeView.SelectedNode;
            FrmInputDialog inputDialog;

            if (selectedNode.Tag == null)
                inputDialog = new FrmInputDialog(selectedNode, TreeView);
            else
            {
                Command dragNdropCmd;
                if (dragNdropPath != null)
                    dragNdropCmd = new Command(selectedNode.Name, true, dragNdropPath, null);
                else
                    dragNdropCmd = new Command(selectedNode);
                inputDialog = new FrmInputDialog(dragNdropCmd, TreeView);
            }

            Command cmd = InputCmd(CmdEditType.EDIT, ref inputDialog, selectedNode);

            if (cmd != null)
            {
                selectedNode.Name = selectedNode.Text = cmd.Name;
                selectedNode.Tag = cmd.ToDictionary();
                selectedNode.SelectedImageKey = selectedNode.ImageKey = SelectIcon(cmd.GetAbsolutePath(selectedNode));
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
