using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace Shortcut
{
    public class Command : TreeNode
    {
        public enum Elements
        {
            NAME,
            RUN,
            PATH,
            ARGUMENTS
        }

        private const string strChecked = "Checked";
        private const string strUnchecked = "Unchecked";

        public bool Run { get; set; } = false;
        public string Path { get; set; } = null;
        public string Arguments { get; set; } = null;
        public TreeNode Node;

        #region Constructors 
        public Command(string name = null, bool run = false, string path = null, string arguments = null)
        {
            Text = Name = name;
            Run = run;
            Path = path;
            Arguments = arguments;
        }

        public Command(TreeNode node)
        {
            if (node == null)
                return;
            else if (node.Tag == null)
            {
                Text = Name = node.Name;
            }
            else
            {
                try
                {
                    // TreeView를 SaveTree할 때 Command Obj는 node의 tag로 save가 안되어서 Dictionary로 저장함
                    Dictionary<Elements, string> cmdDict = (Dictionary<Elements, string>)node.Tag;
                    Text = Name = node.Name;
                    Run = (cmdDict[Elements.RUN] == strChecked);
                    Path = cmdDict[Elements.PATH];
                    Arguments = cmdDict[Elements.ARGUMENTS];
                    Node = node;
                }
                catch
                {
                    Dictionary<string, string> cmdDict = (Dictionary<string, string>)node.Tag;
                    Text = Name = node.Name;
                    Run = (cmdDict["Run"] == "Checked");
                    Path = cmdDict["Path"];
                    Arguments = cmdDict["Arguments"];
                    Node = node;
                }
            }
        }
        #endregion

        public string GetAbsolutePath()
        {
            return RemakeStringWithReplacingKeywords(Path, Node);
        }

        public string GetAbsoluteArguments()
        {
            return RemakeStringWithReplacingKeywords(Arguments, Node);
        }

        public TreeNode ToTreeNode()
        {
            TreeNode node = new TreeNode();
            node.Name = node.Text = Name;
            node.Tag = ToDictionary();
            return node;
        }
        
        public Dictionary <Elements, string> ToDictionary() // TreeView를 SaveTree할 때 Command Obj는 node의 tag로 save가 안되어서 Dictionary로 저장함
        {
            Dictionary<Elements, string> cmdDict = new Dictionary<Elements, string>();
            cmdDict[Elements.NAME] = Name;
            cmdDict[Elements.RUN] = (Run) ? strChecked : strUnchecked;
            cmdDict[Elements.PATH] = Path;
            cmdDict[Elements.ARGUMENTS] = Arguments;
            return cmdDict;
        }

        public string RemakeStringWithReplacingKeywords(string originalString, TreeNode targetNode)
        {
            if ( (targetNode == null) || (targetNode.Parent == null)
                || (originalString == null) || (originalString == "") )
                return originalString;
            else
            {
                TreeNode parentNode = targetNode.Parent;

                Command parentCmd = new Command(parentNode);
                if ( originalString.Contains("#path#") && (parentCmd.Path != "") && (parentCmd.Path != null) )
                {
                    originalString = originalString.Replace("#path#", RemakeStringWithReplacingKeywords(parentCmd.Path, parentNode));
                }

                if ( originalString.Contains("#dir#") && (parentCmd.Path != "") && (parentCmd.Path != null) )
                {
                    originalString = originalString.Replace("#dir#", RemakeStringWithReplacingKeywords(System.IO.Path.GetDirectoryName(parentCmd.Path), parentNode));
                }

                if ( originalString.Contains("#arg#") && (parentCmd.Arguments != "") && (parentCmd.Arguments != null))
                {
                    originalString = originalString.Replace("#arg#", RemakeStringWithReplacingKeywords(parentCmd.Arguments, parentNode));
                }

                return originalString;
            }           
        }
    }
}
