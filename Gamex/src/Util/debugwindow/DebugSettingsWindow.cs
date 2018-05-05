using System.Windows.Forms;
using Gamex.src.Util.Settingsx;

namespace Gamex.src.Util.DebugWindow
{
    public partial class DebugSettingsWindow : UserControl
    {
        public DebugSettingsWindow()
        {
            InitializeComponent();

            InitializeTree();
        }

        private void InitializeTree()
        {
            return;
            var tree = Settings.Tree;
            var node = new TreeNode("jello");
            node.Nodes.Add(new TreeNode("xdd"));
            SettingsTreeView.Nodes.Add(node);
        }
    }
}
