using System;
using System.Windows.Forms;
using Gamex.src.Util.Settingsx;

namespace Gamex.src.Util.DebugWindow
{
    public partial class DebugWindow : Form
    {
        public DebugWindow()
        {
            InitializeComponent();
            
            tabControl1.SelectedIndex = Settings.Tree.Debug.TabIndex;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Tree.Debug.TabIndex = tabControl1.SelectedIndex;
        }
    }
}
