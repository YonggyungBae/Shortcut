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
        enum MovingCmdPosition
        {
            UPPER,
            MIDDLE,
            LOWER
        }

        private Color topCmdColor = Color.BlueViolet;
        private Color normalCmdColor = Color.White;
        private string cfgFileName = "default_cfg.bin";
        private string dragNdropPath = null;
        private ImageList iconList = new ImageList();
        private MovingCmdPosition movingNodePositionBackup;

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
        #region Global Hot Key
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
        #endregion

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
            TreeNode targetCmd = TreeView.GetNodeAt(TreeView.PointToClient(Cursor.Position));

            if (e.Data.GetDataPresent(DataFormats.FileDrop))    // File Drag & Drop
            {
                string[] targetPaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                foreach (string targetPath in targetPaths)
                {
                    if (File.Exists(targetPath) || Directory.Exists(targetPath))
                    {
                        dragNdropPath = targetPath;
                        contextMenuTreeView.Show(TreeView, TreeView.PointToClient(Cursor.Position));
                    }
                }
            }
            else   // Node Drag & Drop
            {
                TreeNode movingCmd = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                TreeNode cloneCmd = (TreeNode)movingCmd.Clone();

                if (targetCmd != movingCmd)
                {
                    bool isMovingCmdExpanded = movingCmd.IsExpanded;

                    if (targetCmd == null)
                    {
                        TreeView.Nodes.Add(cloneCmd);
                    }
                    else
                    {
                        InsertCmd(TreeView, targetCmd, cloneCmd, TreeView.PointToClient(Cursor.Position).Y);
                    }
                    movingCmd.Remove();
                    cloneCmd.ForeColor = (cloneCmd.Parent == null) ? topCmdColor : normalCmdColor;
                    TreeView.SelectedNode = cloneCmd;
                    if (isMovingCmdExpanded) TreeView.SelectedNode.Expand();
                    SaveTree(TreeView, cfgFileName);
                }
            }

            TreeView.Refresh();
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
                foreach (TreeNode cmd in TreeView.Nodes)
                {
                    if ((cmd != clickedItem.Node) && (clickedItem.Node.Parent == null))
                        cmd.Collapse();
                }
            }
        }

        private void TreeView_DragOver(object sender, DragEventArgs e)
        {
            TreeNode targetCmd = TreeView.GetNodeAt(TreeView.PointToClient(Cursor.Position));
            TreeNode movingCmd = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");

            if (targetCmd != movingCmd)
            {
                if (targetCmd == null)
                {
                }
                else
                {
                    MovingCmdPosition movingCmdPosition = GetMovingCmdPositionOnTheTargetCmd(targetCmd, TreeView.PointToClient(Cursor.Position).Y);

                    if (movingNodePositionBackup != movingCmdPosition) TreeView.Refresh();
                    movingNodePositionBackup = movingCmdPosition;

                    if (movingCmdPosition == MovingCmdPosition.MIDDLE)
                    {
                        if (targetCmd.Nodes.Count > 0)
                        {
                            targetCmd.Expand();
                        }
                        else
                        {
                            DrawAddToFolderPlaceholder(targetCmd);
                        }
                    }
                    else
                    {
                        DrawPlaceholder(targetCmd, movingCmdPosition);
                    }
                }
            }
        }
    }
}
