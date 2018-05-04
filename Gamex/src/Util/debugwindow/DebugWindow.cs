using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gamex.src.Util.Coordinate;
using Gamex.src.Util.Logging;

namespace Gamex.src.Util.DebugWindow
{
    public partial class DebugWindow : Form
    {
        public DebugWindow()
        {
            InitializeComponent();
            
            tabControl1.SelectedIndex = Settings.SettingsTree.Debug.TabIndex;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.SettingsTree.Debug.TabIndex = tabControl1.SelectedIndex;
        }
    }
}
