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

        private static void RunCmd(TreeNode cmd)
        {
            if (cmd.Tag != null)
            {
                Dictionary<string, string> cmdSet = (Dictionary<string, string>)cmd.Tag;
                ProcessStartInfo processInfo = new ProcessStartInfo();
                Process process = new Process();

                if ( (cmdSet["Path"] != "") && (cmdSet["Run"] == "Checked") )
                {
                    processInfo.FileName = cmdSet["Path"];
                    if (cmdSet["Arguments"] != "")
                        processInfo.Arguments = cmdSet["Arguments"];
                    process.StartInfo = processInfo;
                    process.Start();
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

        private Dictionary<string, string> InputCmd(CmdEditType cmdEditType, ref FrmInputDialog inputDialog, TreeNode selectedNode)
        {
            while (inputDialog.ShowDialog() == DialogResult.OK)
            {
                if (ChkValidCmd(cmdEditType, selectedNode, inputDialog.GetCmdSet()) == true)
                    return inputDialog.GetCmdSet();
            }
            return null;
        }

        private bool ChkValidCmd(CmdEditType cmdEditType, TreeNode selectedNode, Dictionary<string, string> cmdSet)
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

            if (cmdSet["Cmd"] == "")
            {
                MessageBox.Show("커맨드 이름을 입력해주세요.");
                return false;
            }
            else if (cmdEditType == CmdEditType.ADD)
            {
                if (cmdGrp.ContainsKey(cmdSet["Cmd"]))
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
                TreeNode[] treeNodes = cmdGrp.Find(cmdSet["Cmd"], false);
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

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr ExtractAssociatedIcon(IntPtr hInst, StringBuilder lpIconPath, out ushort lpiIcon);
        private string GetIcon(string path)
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
            Dictionary<string, string> cmdTag = (Dictionary<string, string>)parentNode.Tag;
            parentNode.SelectedImageKey = parentNode.ImageKey = GetIcon(cmdTag["Path"]);

            foreach (TreeNode oSubNode in parentNode.Nodes)
            {
                SetNodeIconRecursive(oSubNode);
            }
        }
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
