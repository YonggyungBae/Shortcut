using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Shortcut
{
    public partial class FrmMain : Form
    {
        private string cfgFileName = "default_cfg.bin";
        private TreeNode NodeToBeDeleted;
        private string dragNdropPath;
        private ImageList iconList = new ImageList();

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadTree(TreeView, "default_cfg.bin");
            //TreeView.ExpandAll();

            // Icon Init.
            TreeView.ImageList = iconList;
            iconList.Images.Add("Folder", DefaultIcons.FolderLarge);
            iconList.Images.Add("Warning", SystemIcons.Error);
            iconList.Images.Add("Shortcut", this.Icon);
            foreach (TreeNode node in TreeView.Nodes)
            {
                node.ForeColor = GetTopNodeColor();
                SetNodeIconRecursive(node);
            }

            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width,
                                      workingArea.Bottom - Size.Height);
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
        }

        private void TreeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (TreeView.HitTest(e.X, e.Y).Node == null)
            {
                TreeView.SelectedNode = null;    // Deselect all nodes when click black area in the TreeView.
            }
            else if (e.Button == MouseButtons.Right)
            {
                TreeView.SelectedNode = TreeView.HitTest(e.X, e.Y).Node;    // 노드를 오른쪽 click 한 경우에도 바로 선택되도록 함.
            }
        }

        private void TreeView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenu.Show(TreeView, e.Location);
            }
        }

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
                    NewCmd.ForeColor = GetTopNodeColor();
                }
                else TreeView.SelectedNode.Nodes.Add(NewCmd);

                TreeView.SelectedNode.Expand();
                SaveCmd(TreeView, cfgFileName);
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
                SaveCmd(TreeView, cfgFileName);
            }
            dragNdropPath = null;
        }

        private void ToolStripMenuItem_Del_Click(object sender, EventArgs e)
        {
            if ((TreeView.SelectedNode != null)
                && (MessageBox.Show("정말로 삭제할까요?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
            {
                TreeView.SelectedNode.Remove();
                SaveCmd(TreeView, cfgFileName);
            }
        }

        private void ContextMenu_Opening(object sender, CancelEventArgs e)
        {
            bool MenuItemVisible = (TreeView.SelectedNode == null);
            toolStripMenuItem_Edit.Visible = !MenuItemVisible;
            toolStripMenuItem_Del.Visible = (!MenuItemVisible) && (dragNdropPath == null);
        }

        private void TreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (sender != null)
            {
                RunCmd(TreeView.SelectedNode);
            }
        }

        private void TreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //DoDragDrop(e.Item, DragDropEffects.Move);

            NodeToBeDeleted = (TreeNode)e.Item;
            string strItem = e.Item.ToString();
            DoDragDrop(strItem, DragDropEffects.Copy | DragDropEffects.Move);
        }

        private void TreeView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void TreeView_DragDrop(object sender, DragEventArgs e)
        {
            Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            TreeNode TargetPositionNode = ((TreeView)sender).GetNodeAt(pt);

            if (e.Data.GetDataPresent(DataFormats.FileDrop))    // File Drag & Drop
            {
                string[] targetPaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                foreach (string targetPath in targetPaths)
                {
                    if (File.Exists(targetPath) || Directory.Exists(targetPath))
                    {
                        TreeView.SelectedNode = TargetPositionNode;
                        dragNdropPath = targetPath;
                        contextMenu.Show(TreeView, pt);
                    }
                }
            }
            else if (e.Data.GetDataPresent(DataFormats.Text))   // Node Drag & Drop
            {
                if (TargetPositionNode != null && TargetPositionNode.Parent == NodeToBeDeleted.Parent)
                {
                    TreeNode clonedNode = NodeToBeDeleted;
                    TreeNodeCollection TargetParentNode = (NodeToBeDeleted.Parent == null) ? TreeView.Nodes : NodeToBeDeleted.Parent.Nodes;

                    NodeToBeDeleted.Remove();
                    TargetParentNode.Insert(TargetPositionNode.Index + 1, clonedNode);
                    TreeView.SelectedNode = clonedNode;
                    SaveCmd(TreeView, cfgFileName);
                }
            }
        }

        private Color GetTopNodeColor()
        {
            return Color.BlueViolet;
        }

        private void TreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RunCmd(TreeView.SelectedNode);
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void TreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode node in TreeView.Nodes)
            {
                if (node != e.Node)
                    node.Collapse();
            }
        }
    }
}
