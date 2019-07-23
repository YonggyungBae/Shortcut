using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics;
using AutoUpdaterDotNET;

namespace Shortcut
{
    public partial class FrmMain
    {
        enum CmdEditType { ADD, EDIT };
        enum ValidPath
        {
            PATH_NONE = 0,
            PATH_VALID,
            PATH_INVALID
        };

        #region Command Control
        public Command SetProperRunState(Command cmd)
        {
            if (cmd.Path == "") cmd.Run = false;
            return cmd;
        }

        private ValidPath ChkValidPath(string path)
        {
            if ((path == null) || (path == ""))
                return ValidPath.PATH_NONE;
            if (Directory.Exists(path) || File.Exists(path))
                return ValidPath.PATH_VALID;
            else
                return ValidPath.PATH_INVALID;
        }

        private void RunCmd(TreeNode node)
        {
            if (node.Tag != null)
            {
                Command cmd = new Command(node);

                if (cmd.Run == true)
                {
                    string path = cmd.GetAbsolutePath(node);
                    string arguments = cmd.GetAbsoluteArguments(node);
                    ValidPath validPath = ChkValidPath(path);
                    if(validPath == ValidPath.PATH_NONE)
                    {
                        // No run.
                    }
                    else if (validPath == ValidPath.PATH_VALID)
                    {
                        ProcessStartInfo processInfo = new ProcessStartInfo();
                        Process process = new Process();

                        processInfo.FileName = path;
                        if (arguments != "")
                            processInfo.Arguments = arguments;
                        process.StartInfo = processInfo;
                        process.Start();
                        MinimizeToTray();
                    }
                    else
                    {
                        MessageBox.Show("Please check the \"Path\" or \"Arguments\" in the command.", "The File or Folder is NOT existed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        node.SelectedImageKey = node.ImageKey = SelectIcon(path);
                    }
                }
            }
        }

        private static void SaveTree(TreeView tree, string filename)
        {
            using (Stream file = File.Open(filename, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(file, tree.Nodes.Cast<TreeNode>().ToList());
            }
        }

        private static void LoadTree(TreeView tree, string filename)
        {

            if (!File.Exists(filename))
                filename = "init_cfg.bin";

            using (Stream file = File.Open(filename, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                object obj = bf.Deserialize(file);

                TreeNode[] nodeList = (obj as IEnumerable<TreeNode>).ToArray();
                tree.Nodes.AddRange(nodeList);
            }
        }

        private Command InputCmd(CmdEditType cmdEditType, ref FrmInputDialog inputDialog, TreeNode selectedNode)
        {
            while (inputDialog.ShowDialog() == DialogResult.OK)
            {
                if (ChkValidCmd(cmdEditType, selectedNode, inputDialog.GetCmdSet()) == true)
                    return SetProperRunState(inputDialog.GetCmdSet());
            }
            return null;
        }

        private bool ChkValidCmd(CmdEditType cmdEditType, TreeNode selectedNode, Command cmd)
        {
            // Check redundant command
            TreeNodeCollection cmdGrp;
            if ( (selectedNode == null)
                || ((cmdEditType == CmdEditType.EDIT) && (selectedNode.Level == 0)) )
                cmdGrp = TreeView.Nodes;
            else if (cmdEditType == CmdEditType.ADD)
                cmdGrp = selectedNode.Nodes;
            else
                cmdGrp = selectedNode.Parent.Nodes;

            if (cmd.Name == "")
            {
                MessageBox.Show("커맨드 이름을 입력해주세요.");
                return false;
            }
            else if (cmdEditType == CmdEditType.ADD)
            {
                if (cmdGrp.ContainsKey(cmd.Name))
                {
                    MessageBox.Show("같은 이름의 커맨드 가 존재합니다.");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                // 같은 이름의 node라도 그게 자기 자신인 경우는 제외
                TreeNode[] treeNodes = cmdGrp.Find(cmd.Name, false);
                if ( (treeNodes.Length == 0) || (treeNodes[0] == selectedNode) )
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("같은 이름의 커맨드 가 존재합니다.");
                    return false;
                }
            }
        }

        private void InsertCmd(TreeView targetTree, TreeNode targetCmd, TreeNode insertCmd, int insertNodePositionY)
        {
            TreeNodeCollection targetParentCmd = (targetCmd.Parent == null) ? targetTree.Nodes : targetCmd.Parent.Nodes;

            switch (GetMovingCmdPositionOnTheTargetCmd(targetCmd, insertNodePositionY))
            {
                case MovingCmdPosition.UPPER:
                    targetParentCmd.Insert(targetCmd.Index, insertCmd);
                    break;
                case MovingCmdPosition.MIDDLE:
                    targetCmd.Nodes.Add(insertCmd);
                    break;
                case MovingCmdPosition.LOWER:
                    targetParentCmd.Insert(targetCmd.Index + 1, insertCmd);
                    break;
                default:
                    break;
            }
        }

        private MovingCmdPosition GetMovingCmdPositionOnTheTargetCmd(TreeNode targetCmd, int cursorY)
        {
            int OffsetY = cursorY - targetCmd.Bounds.Top;

            if (OffsetY < (targetCmd.Bounds.Height / 3))
                return MovingCmdPosition.UPPER;
            else if (OffsetY < (targetCmd.Bounds.Height * 2 / 3))
                return MovingCmdPosition.MIDDLE;
            else
                return MovingCmdPosition.LOWER;
        }

        private void MoveCmdUpDown(TreeNode cmd, Keys dirKey)
        {
            int targetIdx = 0;
            TreeNode cloneNode = (TreeNode)cmd.Clone();

            switch (dirKey)
            {
                case Keys.Up:
                    targetIdx = (cmd.PrevNode == null) ? cmd.Index : cmd.PrevNode.Index;
                    break;
                case Keys.Down:
                    targetIdx = (cmd.NextNode == null) ? cmd.Index : cmd.NextNode.Index + 1;
                    break;
                default:
                    return;
            }

            if (cmd.Level == 0)
            {
                cmd.TreeView.Nodes.Insert(targetIdx, cloneNode);
                cmd.TreeView.SelectedNode = cloneNode;
                cmd.Remove();
            }
            else
            {
                cmd.Parent.Nodes.Insert(targetIdx, cloneNode);
                cmd.TreeView.SelectedNode = cloneNode;
                cmd.Remove();
            }
        }

        private void MoveCmdLeft(TreeNode cmd)
        {
            TreeNode cloneNode = (TreeNode)cmd.Clone();

            if (cmd.Level == 0)
            {
                ;
            }
            else if (cmd.Level == 1)
            {
                cmd.TreeView.Nodes.Insert(cmd.Parent.Index + 1, cloneNode);
                cmd.TreeView.SelectedNode = cloneNode;
                cmd.Remove();
            }
            else
            {
                cmd.Parent.Parent.Nodes.Insert(cmd.Parent.Index + 1, cloneNode);
                cmd.TreeView.SelectedNode = cloneNode;
                cmd.Remove();
            }
        }

        private void MoveCmdRight(TreeNode cmd)
        {
            TreeNode cloneNode = (TreeNode)cmd.Clone();

            if (cmd.PrevNode != null)
            {
                cmd.PrevNode.Nodes.Add(cloneNode);
                //cmd.PrevNode.Nodes.Insert(0, cloneNode);
                cmd.TreeView.SelectedNode = cloneNode;
                cmd.Remove();
            }
        }
        #endregion

        #region Tray Control
        private void MinimizeToTray()
        {
            if (options.GetOption_MinimizeToTrayAfterRun() == true)
                HideForm();
        }
        #endregion

        #region Plance Holder Drawing
        private void DrawPlaceholder(TreeNode NodeOver, MovingCmdPosition placeHolderPosition)
        {
            Graphics g = TreeView.CreateGraphics();

            int NodeOverImageWidth = TreeView.ImageList.Images[NodeOver.ImageKey].Size.Width + 8;
            int LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
            int RightPos = TreeView.Width - 4;
            int yPos = 0;
            if (placeHolderPosition == MovingCmdPosition.UPPER)
                yPos = NodeOver.Bounds.Top;
            else if (placeHolderPosition == MovingCmdPosition.LOWER)
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
        #endregion

        #region Icon Control
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr ExtractAssociatedIcon(IntPtr hInst, StringBuilder lpIconPath, out ushort lpiIcon);
        private string SelectIcon(string path)
        {
            if (path == "")
            {
                return "Shortcut";
            }
            else if (System.IO.File.Exists(path))
            {
                try
                {
                    ushort uicon;
                    StringBuilder strB = new StringBuilder(260); // Allocate MAX_PATH chars
                    strB.Append(path);
                    IntPtr handle = ExtractAssociatedIcon(IntPtr.Zero, strB, out uicon);
                    Icon icon = Icon.FromHandle(handle);
                    //Icon icon = Icon.ExtractAssociatedIcon(path);
                    iconList.Images.Add(path, icon);
                    return path;
                }
                catch (System.ArgumentException)  // CS0168
                {
                    return "Warning";
                }

            }
            else if (System.IO.Directory.Exists(path))
            {
                FileAttributes attr = File.GetAttributes(path);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    return "Folder";
                else
                    return "Warning";
            }
            else
            {
                return "Warning";
            }
        }

        private void SetNodeIconRecursive(TreeNode parentNode)
        {
            Command cmd = new Command(parentNode);
            parentNode.SelectedImageKey = parentNode.ImageKey = SelectIcon(cmd.GetAbsolutePath(parentNode));

            foreach (TreeNode oSubNode in parentNode.Nodes)
            {
                SetNodeIconRecursive(oSubNode);
            }
        }
        #endregion
    }

    public static class DefaultIcons
    {
        private static readonly Lazy<Icon> _lazyFolderIcon = new Lazy<Icon>(FetchIcon, true);

        public static Icon FolderLarge
        {
            get { return _lazyFolderIcon.Value; }
        }

        private static Icon FetchIcon()
        {
            var tmpDir = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString())).FullName;
            var icon = ExtractFromPath(tmpDir);
            Directory.Delete(tmpDir);
            return icon;
        }

        private static Icon ExtractFromPath(string path)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            SHGetFileInfo(
                path,
                0, ref shinfo, (uint)Marshal.SizeOf(shinfo),
                SHGFI_ICON | SHGFI_LARGEICON);
            return System.Drawing.Icon.FromHandle(shinfo.hIcon);
        }

        //Struct used by SHGetFileInfo function
        [StructLayout(LayoutKind.Sequential)]
        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
        private const uint SHGFI_ICON = 0x100;
        private const uint SHGFI_LARGEICON = 0x0;
        private const uint SHGFI_SMALLICON = 0x000000001;
    }
}
