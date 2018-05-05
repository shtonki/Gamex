using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gamex.src.Util.Logging;

namespace Gamex.src.Util.DebugWindow
{
    public partial class LogWindow : UserControl, ILogger
    {
        public new bool AutoScroll
        {
            get { return _AutoScroll; }
            set
            {
                AutoScrollCheckBox.Checked = value;
                _AutoScroll = value;
            }
        }

        private bool _AutoScroll;

        public LogWindow()
        {
            InitializeComponent();

            Logger.RegisterDefaultLogger(this);
            AutoScroll = true;
        }

        public void Log(string logString, params object[] args)
        {
            string logmessage = String.Format(logString, args);

            if (LogDataGrid != null)
            {
                try
                {
                    Action a = () =>
                    {
                        LogDataGrid.Rows.Add(logmessage);

                        if (_AutoScroll)
                        {
                            LogDataGrid.FirstDisplayedScrollingRowIndex = LogDataGrid.Rows.Count - 1;
                        }
                    };

                    if (InvokeRequired)
                    {
                        Invoke(a);
                    }
                    else
                    {
                        a();
                    }
                }
                catch (InvalidAsynchronousStateException)
                {
                    // strong suspicion that the window was closed
                    // do nothing, chill out, tell noone
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        private void LogDataGrid_Scroll(object sender, ScrollEventArgs e)
        {
            // beautiful hack that detects if the user scrolled or if the software
            // scrolled, using the fact that scrolling through software generates
            // a LargeIncrement event type
            if (e.Type != ScrollEventType.LargeIncrement)
            {
                AutoScroll = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            AutoScroll = AutoScrollCheckBox.Checked;
        }
    }
}
