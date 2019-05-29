using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AutoUpdaterDotNET;

namespace Shortcut
{
    public partial class FrmMain : Form
    {
        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }
        enum HotKeyId
        {
            SHOW_FORM = 0,
        }
        enum MovingNodePosition
        {
            UPPER,
            MIDDLE,
            LOWER
        }

        private Color topCmdColor = Color.BlueViolet;
        private string cfgFileName = "default_cfg.bin";
        private string dragNdropPath = null;
        private ImageList iconList = new ImageList();

        //============================== Form Load ==============================//
        public FrmMain()
        {
            InitializeComponent();

            RegisterHotKey(this.Handle, (int)HotKeyId.SHOW_FORM, (int)KeyModifier.WinKey, Keys.Y.GetHashCode());
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            AutoUpdater.Start("https://raw.githubusercontent.com/yg-bae/Shortcut/master/Resources/Version.xml");

            LoadTree(TreeView, cfgFileName);

            // Icon Init.
            TreeView.ImageList = iconList;
            iconList.Images.Add("Folder", DefaultIcons.FolderLarge);
            iconList.Images.Add("Warning", SystemIcons.Error);
            iconList.Images.Add("Shortcut", this.Icon);
            foreach (TreeNode node in TreeView.Nodes)
            {
                node.ForeColor = topCmdColor;
                SetNodeIconRecursive(node);
            }

            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width,
                                      workingArea.Bottom - Size.Height);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            NotifyIcon.Dispose();
            UnregisterHotKey(this.Handle, 0);
        }

        //============================== Global Hot Key ==============================//
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0312)
            {
                /* Note that the three lines below are not needed if you only want to register one hotkey.
                 * The below lines are useful in case you want to register multiple keys, which you can use a switch with the id as argument, or if you want to know which key/modifier was pressed for some particular reason. */
                //Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
                //KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);       // The modifier of the hotkey that was pressed.
                int hotKeyId = m.WParam.ToInt32();                                        // The id of the hotkey that was pressed.

                switch ((HotKeyId)hotKeyId)
                {
                    case HotKeyId.SHOW_FORM:
                        ShowForm();
                        break;
                }
            }
        }

        //============================== Key Event ==============================//
        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if ( (e.KeyCode == Keys.Escape) && (Properties.Settings.Default.optMinimizeToTrayPressEsc == true) )
            {
                HideForm();
            }
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

        //============================== Mouse Event ==============================//
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
            else
            {
                // 왼쪽버튼 클릭의 경우 Mouse Click과 event 가 겹쳐서 "node 이름 변경" event가 실행된다.
                // TreeView.SelectedNode = TreeView.GetNodeAt(e.X, e.Y);
            }
        }

        private void TreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(sender != null)
            {
                RunCmd(TreeView.SelectedNode);
            }
        }

        private void TreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
            //DoDragDrop(strItem, DragDropEffects.Copy | DragDropEffects.Move);
        }

        private void TreeView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.Move;
        }

        private void TreeView_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode NodeOver = TreeView.GetNodeAt(TreeView.PointToClient(Cursor.Position));
            TreeNode NodeMoving = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");

            if (e.Data.GetDataPresent(DataFormats.FileDrop))    // File Drag & Drop
            {
                string[] targetPaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                foreach (string targetPath in targetPaths)
                {
                    if (File.Exists(targetPath) || Directory.Exists(targetPath))
                    {
                        TreeView.SelectedNode = NodeOver;
                        dragNdropPath = targetPath;
                        contextMenuTreeView.Show(TreeView, TreeView.PointToClient(Cursor.Position));
                    }
                }
            }
            else   // Node Drag & Drop
            {
                if (NodeOver != null && (NodeOver != NodeMoving || (NodeOver.Parent != null && NodeOver.Index == (NodeOver.Parent.Nodes.Count - 1))))
                {
                    MovingNodePosition movingNodePosition = GetPlaceInNode(NodeOver, TreeView.PointToClient(Cursor.Position).Y);

                    if (NodeOver.Parent == null)
                    {

                    }
                    else
                    {
                        TreeNode cloneNode = (TreeNode)NodeMoving.Clone();
                        bool isMovingNodeExpanded = NodeMoving.IsExpanded;

                        if (movingNodePosition == MovingNodePosition.UPPER)
                        {
                            NodeOver.Parent.Nodes.Insert(NodeOver.Index, cloneNode);
                        }
                        else if (movingNodePosition == MovingNodePosition.LOWER)
                        {
                            NodeOver.Parent.Nodes.Insert(NodeOver.Index + 1, cloneNode);
                        }
                        else
                        {
                            NodeOver.Nodes.Add(cloneNode);
                        }

                        NodeMoving.Remove();
                        TreeView.SelectedNode = cloneNode;
                        if (isMovingNodeExpanded)   TreeView.SelectedNode.Expand();
                        SaveTree(TreeView, cfgFileName);
                    }
                }
            }
        }

        private void TreeView_MouseClick(object sender, MouseEventArgs e)
        {
            var clickedItem = TreeView.HitTest(e.Location);
            if (clickedItem.Location == TreeViewHitTestLocations.PlusMinus)
            {
                // [+] clicked
            }
            else
            {
                clickedItem.Node.Expand();
                foreach (TreeNode node in TreeView.Nodes)
                {
                    if ((node != clickedItem.Node) && (clickedItem.Node.Parent == null))
                        node.Collapse();
                }
            }
        }

        private void TreeView_DragOver(object sender, DragEventArgs e)
        {
            TreeNode NodeOver = TreeView.GetNodeAt(TreeView.PointToClient(Cursor.Position));
            TreeNode NodeMoving = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");

            if (NodeOver != null && (NodeOver != NodeMoving || (NodeOver.Parent != null && NodeOver.Index == (NodeOver.Parent.Nodes.Count - 1))))
            {
                MovingNodePosition movingNodePosition = GetPlaceInNode(NodeOver, TreeView.PointToClient(Cursor.Position).Y);

                if(movingNodePosition == MovingNodePosition.MIDDLE)
                {
                    if (NodeOver.Nodes.Count > 0)
                    {
                        NodeOver.Expand();
                    }
                    else
                    {
                        DrawAddToFolderPlaceholder(NodeOver);
                    }
                }
                else
                {
                    DrawPlaceholder(NodeOver, movingNodePosition);
                }
            }
        }

        private MovingNodePosition GetPlaceInNode(TreeNode NodeOver, int cursorY)
        {
            int OffsetY = cursorY - NodeOver.Bounds.Top;

            if (OffsetY < (NodeOver.Bounds.Height / 3))
                return MovingNodePosition.UPPER;
            else if (OffsetY < (NodeOver.Bounds.Height * 2 / 3))
                return MovingNodePosition.MIDDLE; 
            else
                return MovingNodePosition.LOWER;
        }

        private void DrawPlaceholder(TreeNode NodeOver, MovingNodePosition placeHolderPosition)
        {
            Refresh();
            Graphics g = TreeView.CreateGraphics();

            int NodeOverImageWidth = TreeView.ImageList.Images[NodeOver.ImageKey].Size.Width + 8;
            int LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
            int RightPos = TreeView.Width - 4;
            int yPos = 0;
            if(placeHolderPosition == MovingNodePosition.UPPER)
                yPos = NodeOver.Bounds.Top;
            else if(placeHolderPosition == MovingNodePosition.LOWER)
                yPos = NodeOver.Bounds.Bottom;

            Point[] LeftTriangle = new Point[5]{
                                                   new Point(LeftPos, yPos - 4),
                                                   new Point(LeftPos, yPos + 4),
                                                   new Point(LeftPos + 4, yPos),
                                                   new Point(LeftPos + 4, yPos - 1),
                                                   new Point(LeftPos, yPos - 5)};

            Point[] RightTriangle = new Point[5]{
                                                    new Point(RightPos, yPos - 4),
                                                    new Point(RightPos, yPos + 4),
                                                    new Point(RightPos - 4, yPos),
                                                    new Point(RightPos - 4, yPos - 1),
                                                    new Point(RightPos, yPos - 5)};

            g.FillPolygon(System.Drawing.Brushes.White, LeftTriangle);
            g.FillPolygon(System.Drawing.Brushes.White, RightTriangle);
            g.DrawLine(new System.Drawing.Pen(Color.White, 2), new Point(LeftPos, yPos), new Point(RightPos, yPos));
        }

        private void DrawAddToFolderPlaceholder(TreeNode NodeOver)
        {
            this.Refresh();
            Graphics g = TreeView.CreateGraphics();

            int RightPos = NodeOver.Bounds.Right + 6;
            Point[] RightTriangle = new Point[5]{
                                                    new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) + 4),
                                                    new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) + 4),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2)),
                                                    new Point(RightPos - 4, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) - 1),
                                                    new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) - 5)};

            
            g.FillPolygon(System.Drawing.Brushes.White, RightTriangle);
        }
    }
}
