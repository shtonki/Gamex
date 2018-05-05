using System;
using System.Threading;
using System.Windows.Forms;
using Gamex.src.Util.Coordinate;
using Gamex.src.Util.DebugWindow;
using Gamex.src.Util.Logging;

namespace Gamex.src.Controller.Debug
{
    static class DebugController
    {
        public static DebugWindow DebugWindow { get; set; }
        public static PixelCoordinate MousePosPixel
        {
            set
            {
                SafeInvoke(() => DebugWindow.MouseInfoWindow.MouseCoordinatePixelLabel.Text = value.ToString());
            }
        }
        public static GLCoordinate MousePosGL
        {
            set
            {
                SafeInvoke(() => DebugWindow.MouseInfoWindow.MouseCoordinateGLLabel.Text = value.ToString());
            }
        }
        public static GameCoordinate MousePosGame
        {
            set
            {
                SafeInvoke(() => DebugWindow.MouseInfoWindow.MouseCoordinateGameLabel.Text = value.ToString());
            }
        }

        private static bool LogWindowProbablyOpen { get; set; }


        /// <summary>
        /// Shows the debug window
        /// </summary>
        public static void Initialize()
        {
            // Application.Run blocks so we make a thread
            // new Thread(...).Start() because we're balling outta control here
            new Thread(() =>
            {
                DebugWindow = new DebugWindow();
                DebugWindow.Closed += (sender, args) => LogWindowProbablyOpen = false;
                Application.Run(DebugWindow);
            }).Start();
            LogWindowProbablyOpen = true;
        }

        /// <summary>
        /// Prints a message to the log panel
        /// </summary>
        /// <param name="message"></param>
        public static void Log(string message)
        {
            if (LogWindowProbablyOpen)
            {
                DebugWindow.LogWindow.Log(message);
            }
        }


        private static void SafeInvoke(Action action)
        {
            try
            {
                if (DebugWindow.InvokeRequired)
                {
                    DebugWindow.Invoke(action);
                }
                else
                {
                    action();
                }
            }
            catch (Exception)
            {
                Logger.Default.Log("SafeInvoke failed miserably");
            }
        }
    }
}
