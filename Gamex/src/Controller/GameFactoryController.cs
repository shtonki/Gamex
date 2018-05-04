using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamex.src.Controller.Game;

namespace Gamex.src.Controller.GameFactory
{
    static class GameFactoryController
    {
        public static GameController LastCreated { get; private set; }

        public static void CreateGame()
        {
            var gc = new GameController();
            LastCreated = gc;
            gc.StartGame();
        }
    }
}
