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
        private static bool LogWindowProbablyOpen { get; set; }

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


        public static void Initialize()
        {
            Thread t = new Thread(() =>
            {
                DebugWindow = new DebugWindow();
                DebugWindow.Closed += (sender, args) => LogWindowProbablyOpen = false;
                Application.Run(DebugWindow);
            });
            t.Start();
            LogWindowProbablyOpen = true;
        }

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
            catch (Exception e)
            {
                Logger.Default.Log("SafeInvoke failed miserably");
            }
        }
    }
}
