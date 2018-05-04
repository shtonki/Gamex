using System;
using System.Globalization;
using System.Threading;
using Gamex.src.Controller;
using Gamex.src.Controller.App;
using Gamex.src.Controller.Debug;
using Gamex.src.testland;
using Gamex.src.Util.Logging;

namespace Gamex
{

    static class Program
    {
        public const bool TESTING = false;
        public const bool EDITING = false;
        public static bool DEBUGMODE { get; } = true;

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            Thread.CurrentThread.Name = "Gamex main thread";

            if (DEBUGMODE)
            {
                DebugController.Initialize();
            }

            if (TESTING)
            {
                TestMain.AllTests();
                AppController.ExitApp();
            }
            else if (EDITING)
            {
                AppController.LaunchApp();
                EditorController.InitEditor();
            }
            else
            {
                AppController.LaunchApp();
            }

        }
    }
}
