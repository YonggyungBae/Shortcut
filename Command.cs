using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace Shortcut
{
    public class Command
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
        public string Name
        {
            get { return Node.Name; }
            set { Node.Name = Node.Text = value;  }
        }

        #region Constructors 
        public Command(string name, bool run = false, string path = null, string arguments = null)
        {
            Run = run;
            Path = path;
            Arguments = arguments;
            Node = new TreeNode(name)
            {
                Name = name,
                Text = name,
                Tag = ToTag(name, run, path, arguments)
            };
        }

        public Command(TreeNode node)
        {
            if (node == null)
                return;
            else if (node.Tag == null)
            {
                Node = node;
            }
            else
            {
                try
                {
                    // TreeView를 SaveTree할 때 Command Obj는 node의 tag로 save가 안되어서 Dictionary로 저장함
                    Dictionary<Elements, string> cmdDict = (Dictionary<Elements, string>)node.Tag;
                    Run = (cmdDict[Elements.RUN] == strChecked);
                    Path = cmdDict[Elements.PATH];
                    Arguments = cmdDict[Elements.ARGUMENTS];
                    Node = node;
                }
                catch
                {
                    Dictionary<string, string> cmdDict = (Dictionary<string, string>)node.Tag;
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

        public TreeNode GetTreeNode()
        {
            Node.Tag = ToTag(Name, Run, Path, Arguments);
            return Node;
        }

        private Dictionary<Elements, string> ToTag(string name, bool run, string path, string arg) // TreeView를 SaveTree할 때 Command Obj는 node의 tag로 save가 안되어서 Dictionary로 저장함
        {
            Dictionary<Elements, string> cmdDict = new Dictionary<Elements, string>();
            cmdDict[Elements.NAME] = name;
            cmdDict[Elements.RUN] = (run) ? strChecked : strUnchecked;
            cmdDict[Elements.PATH] = path;
            cmdDict[Elements.ARGUMENTS] = arg;
            return cmdDict;
        }

        public Dictionary <Elements, string> GetDictionary() // TreeView를 SaveTree할 때 Command Obj는 node의 tag로 save가 안되어서 Dictionary로 저장함
        {
            return ToTag(Name, Run, Path, Arguments) ;
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
