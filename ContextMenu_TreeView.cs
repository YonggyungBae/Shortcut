using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Shortcut
{
    public partial class FrmMain
    {
        private void ToolStripMenuItem_Add_Click(object sender, EventArgs e)
        {
            Point positionContextmunu = TreeView.PointToClient(contextMenuTreeView.Bounds.Location);
            TreeNode NodeOver = TreeView.GetNodeAt(positionContextmunu);
            FrmInputDialog inputDialog;

            if (dragNdropPath != null)
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

            Dictionary<string, string> cmdSet = InputCmd(CmdEditType.ADD, ref inputDialog, NodeOver);

            if (cmdSet != null)
            {
                TreeNode NewCmd = new TreeNode
                {
                    Text = Name = cmdSet["Cmd"],
                    Tag = cmdSet,
                };

                NewCmd.SelectedImageKey = NewCmd.ImageKey = GetIcon(cmdSet["Path"]);

                if (NodeOver == null)
                {
                    TreeView.Nodes.Add(NewCmd);
                    NewCmd.ForeColor = (NewCmd.Parent == null) ? topCmdColor : normalCmdColor;
                }
                else
                {
                    switch (GetMovingNodePositionOverNode(NodeOver, positionContextmunu.Y))
                    {
                        case MovingNodePosition.UPPER:
                            NodeOver.Parent.Nodes.Insert(NodeOver.Index, NewCmd);
                            break;
                        case MovingNodePosition.MIDDLE:
                            NodeOver.Nodes.Add(NewCmd);
                            break;
                        case MovingNodePosition.LOWER:
                            NodeOver.Parent.Nodes.Insert(NodeOver.Index + 1, NewCmd);
                            break;
                        default:
                            break;
                    }
                    NodeOver.Expand();
                }
                
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
