using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shortcut
{
    public partial class FrmMain : Form
    {
        private string cfgFileName = "default_cfg.bin";
        private TreeNode NodeToBeDeleted;
        private string dragNdropPath;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadTree(treeView, "default_cfg.bin");
            treeView.ExpandAll();

            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width,
                                      workingArea.Bottom - Size.Height);
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
        }

        private void TreeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (treeView.HitTest(e.X, e.Y).Node == null)
            {
                treeView.SelectedNode = null;    // Deselect all nodes when click black area in the TreeView.
            }
            else if (e.Button == MouseButtons.Right)
            {
                treeView.SelectedNode = treeView.HitTest(e.X, e.Y).Node;    // 노드를 오른쪽 click 한 경우에도 바로 선택되도록 함.
            }
        }

        private void TreeView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenu.Show(treeView, e.Location);
            }
        }

        private void ToolStripMenuItem_Add_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView.SelectedNode;  // Input Dialog가 show 되기 전에 선택 node를 backup 받아놔야 함(안그러면 InputDialog가 show되고 나면 treeview의 첫번째 node가 강제 선택됨)
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
                    Tag = cmdSet
                };

                if (selectedNode == null) treeView.Nodes.Add(NewCmd);
                else treeView.SelectedNode.Nodes.Add(NewCmd);

                treeView.SelectedNode.Expand();
                SaveCmd(treeView, cfgFileName);
            }
            dragNdropPath = null;
        }

        private void ToolStripMenuItem_Edit_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView.SelectedNode;
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
                SaveCmd(treeView, cfgFileName);
            }
            dragNdropPath = null;
        }

        private void ToolStripMenuItem_Del_Click(object sender, EventArgs e)
        {
            if ((treeView.SelectedNode != null)
                && (MessageBox.Show("정말로 삭제할까요?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
            {
                treeView.SelectedNode.Remove();
                SaveCmd(treeView, cfgFileName);
            }
        }

        private void ContextMenu_Opening(object sender, CancelEventArgs e)
        {
            bool MenuItemVisible = (treeView.SelectedNode == null);
            toolStripMenuItem_Edit.Visible = !MenuItemVisible;
            toolStripMenuItem_Del.Visible = (!MenuItemVisible) && (dragNdropPath == null);
        }

        private void TreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(sender != null)
            {
                TreeNode selectedNode = treeView.SelectedNode;

                if (selectedNode.Tag != null)
                {
                    Dictionary<string, string> cmdSet = (Dictionary<string, string>)selectedNode.Tag;
                    ProcessStartInfo processInfo = new ProcessStartInfo();
                    Process process = new Process();

                    if (cmdSet["Path"] != "")
                    {
                        processInfo.FileName = cmdSet["Path"];
                        if (cmdSet["Arguments"] != "")
                            processInfo.Arguments = cmdSet["Arguments"];
                        process.StartInfo = processInfo;
                        process.Start();
                    }   
                }
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
                        treeView.SelectedNode = TargetPositionNode;
                        dragNdropPath = targetPath;
                        contextMenu.Show(treeView, pt);
                    }
                }
            }
            else if (e.Data.GetDataPresent(DataFormats.Text))   // Node Drag & Drop
            {
                if (TargetPositionNode != null && TargetPositionNode.Parent == NodeToBeDeleted.Parent)
                {
                    TreeNode clonedNode = NodeToBeDeleted;
                    TreeNodeCollection TargetParentNode = (NodeToBeDeleted.Parent == null) ? treeView.Nodes : NodeToBeDeleted.Parent.Nodes;

                    NodeToBeDeleted.Remove();
                    TargetParentNode.Insert(TargetPositionNode.Index + 1, clonedNode);
                    treeView.SelectedNode = clonedNode;
                    SaveCmd(treeView, cfgFileName);
                }
            }
        }
    }
}
