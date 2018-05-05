using System;
using Gamex.src.Controller.Graphics;
using Gamex.src.Controller.Input;
using Gamex.src.Controller.Game;
using Gamex.src.Controller.GameFactory;
using Gamex.src.Util.Settingsx;

namespace Gamex.src.Controller.App
{
    public static class AppController
    {
        public static event EventHandler OnExit;
        
        public static void LaunchApp()
        {
            Settings.Load();

            GraphicsController.Initialize();
            InputController.Initialize();
            GameFactoryController.CreateGame();
        }

        public static void LaunchEditor()
        {
            GraphicsController.Initialize();
            InputController.Initialize();
            //GameFactoryController.CreateGame();
            //create editor "game"
        }

        public static void ExitApp()
        {
            OnExit?.Invoke(null, new EventArgs());
            Settings.Save();
            Environment.Exit(Int32.MaxValue);
        }
    }
}
