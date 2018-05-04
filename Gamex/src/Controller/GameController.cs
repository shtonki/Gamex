using System;
using System.Timers;
using Gamex.src.GameModel;
using Gamex.src.Util.Logging;
using Gamex.src.Controller.Graphics;
using Gamex.src.Util;
using Gamex.src.XDGE;

namespace Gamex.src.Controller.Game
{
    public class GameController
    {
        public const int StepTickSpeed = 60;

        public GameModel.GameState Game { get; private set; }

        private Timer StepTimer { get; set; }

        public GameController()
        {
            Game = new GameModel.GameState();
        }

        public void StartGame()
        {
            if (Game == null)
            {
                throw new GameBrokenException("Have Fun.");
            }

            var gscene = new GameScene();
            gscene.GameState = Game;
            GraphicsController.ActiveScene = gscene;

            StepTimer = new Timer(1000.0/StepTickSpeed);
            StepTimer.Elapsed += StepTimerElapsed;
            StepTimer.Start();
        }

        public void EndGame()
        {
            if (Game == null)
            {
                Logger.Default.Log("Ended game without a game running.");
                return;
            }

            StepTimer.Stop();
            StepTimer = null;
            Game = null;
        }

        private void StepTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Game.Step();
        }

    }
}
