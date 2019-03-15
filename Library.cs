using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Shortcut
{
    public partial class FrmMain
    {
        enum CmdEditType { ADD, EDIT };
        
        public static void SaveCmd(TreeView tree, string filename)
        {
            using (Stream file = File.Open(filename, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(file, tree.Nodes.Cast<TreeNode>().ToList());
            }
        }

        public static void LoadTree(TreeView tree, string filename)
        {
            if(File.Exists(filename))
            {
                using (Stream file = File.Open(filename, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    object obj = bf.Deserialize(file);

                    TreeNode[] nodeList = (obj as IEnumerable<TreeNode>).ToArray();
                    tree.Nodes.AddRange(nodeList);
                }
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
                cmdGrp = treeView.Nodes;
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
    }
}
