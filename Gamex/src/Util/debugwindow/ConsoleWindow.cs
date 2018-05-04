using System;
using System.Windows.Forms;

namespace Gamex.src.Util.DebugWindow
{
    public partial class ConsoleWindow : UserControl
    {
        private const string PS1 = "shellx>";

        public ConsoleWindow()
        {
            InitializeComponent();
        }

        public void WriteLine(string line)
        {
            MainTextBox.AppendText(line + Environment.NewLine);
        }

        private void HandleCommand(string command)
        {
            switch (command)
            {
                case "exit":
                {
                    
                } break;

                default:
                {
                    WriteLine(String.Format("{0}Unknown command '{1}'", Environment.NewLine, command));
                } break;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                var commandString = textBox1.Text;
                WriteLine(commandString);
                textBox1.Text = "";
                HandleCommand(commandString);
            }
        }

        private void MainTextBox_TextChanged(object sender, EventArgs e)
        {
            MainTextBox.SelectionStart = MainTextBox.Text.Length;
            MainTextBox.ScrollToCaret();
        }
    }
}
