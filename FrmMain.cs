using System;
using System.Collections.Generic;
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
            LOWER,
            OUTSIDE,
        }

        private Color topCmdColor = Color.BlueViolet;
        private Color normalCmdColor = Color.White;
        private string cfgFileName = "default_cfg.bin";
        private string dragNdropPath = null;
        private ImageList iconList = new ImageList();
        private MovingCmdPosition movingNodePositionBackup;
        private MouseButtons mouseButtons = MouseButtons.Left;
        private Options options = new Options();
        private bool nodeDoubleClicked = false;
        private bool IsTopParentClicked = false;

#region ============================== Form Load ==============================
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            AutoUpdater.UpdateFormSize = new System.Drawing.Size(800, 600);
            AutoUpdater.Start("https://raw.githubusercontent.com/yg-bae/Shortcut/master/Resources/Version.xml");

            LoadTree(TreeView, cfgFileName);

            // Icon Init.
            TreeView.ImageList = iconList;
            iconList.Images.Add("Folder", DefaultIcons.FolderLarge);
            iconList.Images.Add("Warning", SystemIcons.Error);
            iconList.Images.Add("Shortcut", this.Icon);

            // 처음 loading 할 때 모든 node의 icon을 설정(list 이용)
            List<TreeNode> treeNodes = GetAllNodes(TreeView);

            FrmSplash splash = new FrmSplash(treeNodes.Count);
            splash.Visible = true;

            for (int i = 0; i < treeNodes.Count; i++)
            {
                Command cmd = new Command(treeNodes[i]);
                splash.Step( (int)((double)i / (double)treeNodes.Count * 100) );
                treeNodes[i].SelectedImageKey = treeNodes[i].ImageKey = SelectIcon(cmd.GetAbsolutePath());
                //splash.Refresh();
            }
            Option_Apply_ShowInTaskbar();
            RegisterHotKeyGlobal();
            FrmMain_SizeAndLocationLoad();
            if(treeNodes.Count > 300)   // Node의 개수가 작으면 1초라도 splash 화면을 보여주기 위해.
                System.Threading.Thread.Sleep(1000);

            Show();
            BringToFront();
            Activate();

            splash.Close();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            FrmMain_SizeAncLocationSave();
            NotifyIcon.Dispose();
            UnregisterHotKey(this.Handle, 0);
        }

        private void FrmMain_SetDefaultSizeAndLocation()
        {
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Width = (int)(workingArea.Size.Width * 0.15);
            this.Height = (int)(workingArea.Size.Height * 0.6);
            this.Location = new Point(workingArea.Right - Size.Width,
                                      workingArea.Bottom - Size.Height);
        }

        private void FrmMain_SizeAndLocationLoad()
        {
            this.Size = Properties.Settings.Default.FrmSize;
            Rectangle workingArea = Screen.GetWorkingArea(this);
            int newLocationX = Properties.Settings.Default.FrmLocation.X;
            int newLocationY = Properties.Settings.Default.FrmLocation.Y;

            // 화면 밖에 배치되는 경우
            if ((newLocationX + this.Size.Width) > workingArea.Right)   // 화면 오른쪽 끝을 넘어가는 경우
                newLocationX = workingArea.Right - this.Size.Width;
            else if (newLocationX < 0)  // 화면 왼쪽 끝을 넘어가는 경우
                newLocationX = 0;

            if ((newLocationY + this.Size.Height) > workingArea.Bottom) // 화면 아래 끝을 넘어가는 경우
                newLocationY = workingArea.Bottom - this.Size.Height;
            else if (newLocationY < 0)  // 화면 위쪽 끝을 넘어가는 경우
                newLocationY = 0;

            this.Location = new Point(newLocationX, newLocationY);
        }

        private void FrmMain_SizeAncLocationSave()
        {
            Properties.Settings.Default.FrmSize = this.Size;
            Properties.Settings.Default.FrmLocation = this.Location;
            Properties.Settings.Default.Save(); // Settings 값이 바뀌고 나면 꼭 Save() 해 주어야 함
        }

        private void Option_Apply_ShowInTaskbar()
        {
            this.ShowInTaskbar = options.GetOption_ShowInTaskBar();

            // ShowInTaskbar 값을 바꾸면 RegisterHotKey 다시 해줘야함...이유를 모르겠음.
            RegisterHotKeyGlobal();
        }
#endregion ============================== Form Load ==============================

#region ============================== Global Hot Key ==============================
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

        private void RegisterHotKeyGlobal()
        {
            RegisterHotKey(this.Handle, (int)HotKeyId.SHOW_FORM, (int)KeyModifier.WinKey, Keys.Y.GetHashCode());
        }
#endregion ============================== Global Hot Key ==============================

#region ============================== Key Event ==============================
        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if ( (e.KeyCode == Keys.Escape) && (Properties.Settings.Default.optMinimizeToTrayPressEsc == true) )
            {
                HideForm();
            }
        }

        private void TreeView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Enter):
                    RunCmd(TreeView.SelectedNode);
                    e.Handled = true;
                    break;
                case (Keys.Control | Keys.Up):
                case (Keys.Control | Keys.Down):
                    MoveCmdUpDown(TreeView.SelectedNode, (e.KeyData & Keys.Up) | (e.KeyData & Keys.Down));
                    SaveTree(TreeView, cfgFileName);
                    e.Handled = true;
                    break;
                case (Keys.Control | Keys.Left):
                    MoveCmdLeft(TreeView.SelectedNode);
                    SaveTree(TreeView, cfgFileName);
                    e.Handled = true;
                    break;
                case (Keys.Control | Keys.Right):
                    MoveCmdRight(TreeView.SelectedNode);
                    SaveTree(TreeView, cfgFileName);
                    e.Handled = true;
                    break;
                case (Keys.Control | Keys.Home):
                    while (TreeView.SelectedNode.Parent != null)
                        TreeView.SelectedNode = TreeView.SelectedNode.Parent;
                    e.Handled = true;
                    break;
                case (Keys.Alt | Keys.Up):
                    TreeView.SelectedNode = GotoNode_PrevNodeOrParentPrevNode(TreeView.SelectedNode);
                    e.Handled = true;
                    break;
                case (Keys.Alt | Keys.Down):
                    TreeView.SelectedNode = GotoNode_LastNodeOrParentNextNode(TreeView.SelectedNode);
                    e.Handled = true;
                    break;
                case (Keys.Alt | Keys.Right):
                    if (TreeView.SelectedNode.IsExpanded)
                        TreeView.SelectedNode = TreeView.SelectedNode.FirstNode;
                    else
                        TreeView.SelectedNode.ExpandAll();
                    e.Handled = true;
                    break;
                case (Keys.Alt | Keys.Left):
                    if ((TreeView.SelectedNode.IsExpanded == false) && (TreeView.SelectedNode.Parent != null))
                        TreeView.SelectedNode = TreeView.SelectedNode.Parent;

                    TreeView.SelectedNode.Collapse();
                    e.Handled = true;
                    break;
                case (Keys.Alt | Keys.Shift | Keys.Left):
                    TreeNode selectedNode = TreeView.SelectedNode;
                    TreeView.CollapseAll();
                    TreeView.SelectedNode = GotoNode_TopParent(selectedNode);
                    e.Handled = true;
                    break;
                case (Keys.Alt | Keys.Shift | Keys.Right):
                    TreeView.ExpandAll();
                    e.Handled = true;
                    break;
                case (Keys.Control | Keys.Alt | Keys.NumPad3):
                    FrmMain_SetDefaultSizeAndLocation();
                    e.Handled = true;
                    break;
                case Keys.Apps:
                    ShowContextMenu(TreeView.SelectedNode);
                    e.Handled = true;
                    break;
                default:
                    e.Handled = false;
                    break;
            }
        }
#endregion ============================== Key Event ==============================

#region ============================== Mouse Event ==============================
        private void TreeView_MouseDown(object sender, MouseEventArgs e)
        {
            mouseButtons = e.Button;
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

        private void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            IsTopParentClicked = false;
            var clickedItem = TreeView.HitTest(e.Location);
            if (clickedItem.Location == TreeViewHitTestLocations.PlusMinus)
            {   // [+] clicked
            }
            else
            {
                if(e.Button == MouseButtons.Right)
                {
                    ShowContextMenu(e.Node);
                }
                else
                {
                    IsTopParentClicked = true;
                    e.Node.Expand();
                }
            }            
        }

        private void TreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            nodeDoubleClicked = true;
            if (sender != null)
            {
                RunCmd(TreeView.SelectedNode);
            }
        }

        private void TreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (IsTopParentClicked)
            {
                foreach (TreeNode cmd in TreeView.Nodes)
                {
                    if ((cmd != e.Node) && (e.Node.Parent == null))
                        cmd.Collapse();
                }
                IsTopParentClicked = false;
            }
        }

        private void TreeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (nodeDoubleClicked)
            {
                nodeDoubleClicked = false;
                e.Cancel = true;
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
            TreeNode targetCmd = TreeView.SelectedNode;

            Activate(); // Drag & Drop했을 때 InputDialog가 화면의 가장 앞으로 보일 수 있도록 함.

            if (e.Data.GetDataPresent(DataFormats.FileDrop))    // File Drag & Drop
            {
                string[] targetPaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                foreach (string targetPath in targetPaths)
                {
                    if (File.Exists(targetPath) || Directory.Exists(targetPath))
                    {
                        dragNdropPath = targetPath;
		            	OpenDialog_NodeAdd(TreeView.PointToClient(Cursor.Position));
                    }
                }
            }
            else   // Node Drag & Drop
            {
                TreeNode movingNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                TreeNode clonedNode = (TreeNode)movingNode.Clone();

                if (movingNode.IsExpanded)  clonedNode.Expand();

                if (mouseButtons == MouseButtons.Right) clonedNode.Name = clonedNode.Text = clonedNode.Name + " - Copy";

                if ( (targetCmd != movingNode)  // 자기가 자기에게 drop될 수 없다.
                    && (SearchCmd_ToParentsNode(targetCmd, movingNode.Name) == null))  // 자기 자식에게 drop될 수 없다.
                {
                    InsertCmd(TreeView, targetCmd, clonedNode, GetMovingCmdPositionOnTheTargetCmd(targetCmd, TreeView.PointToClient(Cursor.Position).Y));
                    if (mouseButtons != MouseButtons.Right) movingNode.Remove();
                    TreeView.SelectedNode = clonedNode;
                    SaveTree(TreeView, cfgFileName);
                }
            }

            TreeView.Refresh();
        }

        private void TreeView_DragOver(object sender, DragEventArgs e)
        {
            TreeNode targetCmd = TreeView.GetNodeAt(TreeView.PointToClient(Cursor.Position));
            TreeNode movingCmd = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");

            if (targetCmd == null)
            {
                TreeView.SelectedNode = null;
            }
            else if (targetCmd != movingCmd)
            {
                MovingCmdPosition movingCmdPosition = GetMovingCmdPositionOnTheTargetCmd(targetCmd, TreeView.PointToClient(Cursor.Position).Y);

                if (movingNodePositionBackup != movingCmdPosition)
                {
                    TreeView.Refresh();   // Node를 drag 할 때 화면이 깜박이는 것을 방지하기 위해서 node의 위치가 MovingCmdPosition에 준해서 바뀔 때에만 Refresh 한다.
                    movingNodePositionBackup = movingCmdPosition;
                    TreeView.SelectedNode = targetCmd;
                }

                if (movingCmdPosition == MovingCmdPosition.MIDDLE)
                {
                    if (targetCmd.Nodes.Count > 0)
                    {
                        tmrNodeOver.Start();
                    }
                    else
                    {
                        tmrNodeOver.Stop();
                        DrawAddToFolderPlaceholder(targetCmd);
                    }
                }
                else
                {
                    tmrNodeOver.Stop();
                    DrawPlaceholder(targetCmd, movingCmdPosition);
                }
            }
        }
#endregion ============================== Mouse Event ==============================

#region ============================== Etc ==============================
        private void Timer_Tick(object sender, EventArgs e)
        {
            TreeNode targetCmd = TreeView.GetNodeAt(TreeView.PointToClient(Cursor.Position));
            targetCmd.Expand();
            throw new NotImplementedException();
        }

        private void tmrNodeOver_Tick(object sender, EventArgs e)
        {
            TreeNode targetCmd = TreeView.GetNodeAt(TreeView.PointToClient(Cursor.Position));

            if (targetCmd != null)
                targetCmd.Expand();
            tmrNodeOver.Stop();
        }

#endregion ============================== Etc ==============================

#region ============================== Tool Strip (Menu) ==============================
        private void ToolItem_Add_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = (TreeView.SelectedNode == null) ? TreeView.Nodes[TreeView.Nodes.Count - 1] : TreeView.SelectedNode;
            Point position = selectedNode.Bounds.Location;
            position.Y = position.Y + selectedNode.Bounds.Height / 2;
            OpenDialog_NodeAdd(position);
        }

        private void ToolItem_Remove_Click(object sender, EventArgs e)
        {
            if ((TreeView.SelectedNode != null)
                && (MessageBox.Show("정말로 삭제할까요?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
            {
                TreeView.SelectedNode.Remove();
                SaveTree(TreeView, cfgFileName);
            }
        }

        private void ToolItem_Edit_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = TreeView.SelectedNode;
            FrmInputDialog inputDialog;

            if (selectedNode.Tag == null)
                inputDialog = new FrmInputDialog(new Command(selectedNode), TreeView);
            else
            {
                Command dragNdropCmd = new Command(selectedNode);
                inputDialog = new FrmInputDialog(dragNdropCmd, TreeView);

            }
            Command cmd = OpenCmdDialog(CmdEditType.EDIT, ref inputDialog, selectedNode);

            if (cmd != null)
            {
                selectedNode.Name = selectedNode.Text = cmd.Node.Name;
                selectedNode.Tag = cmd.GetDictionary();
                string path = cmd.GetAbsolutePath();
                selectedNode.SelectedImageKey = selectedNode.ImageKey = SelectIcon(path);
                SaveTree(TreeView, cfgFileName);
            }
            dragNdropPath = null;
        }
        
#endregion ============================== Tool Strip ==============================
    }
}
