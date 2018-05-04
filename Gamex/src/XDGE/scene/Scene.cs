
using System;
using System.Collections.Generic;
using System.Linq;
using Gamex.src.Controller;
using Gamex.src.Controller.App;
using Gamex.src.Controller.Game;
using Gamex.src.Controller.GameFactory;
using Gamex.src.Controller.Graphics;
using Gamex.src.Controller.Input;
using Gamex.src.GameModel;
using Gamex.src.Util.Logging;
using OpenTK.Input;

namespace Gamex.src.XDGE
{
    abstract class Scene
    {
        protected List<InputBinding> Bindings = new List<InputBinding>();

        public abstract void Draw(DrawAdapter adapter);

        public Scene()
        {
            SetupInputBindings();
        }

        /// <summary>
        /// Handles the given input.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>True if the handler handled the input, false if it ignored it.</returns>
        public bool HandleInput(GamexInput input)
        {
            foreach (var binding in Bindings)
            {
                if (binding.Input.Equals(input))
                {
                    binding.Handler(input);
                    return true;
                    //this will cause it to ignore any binding but the 
                    //first one that would have triggered
                }
            }
            return false;
        }

        protected abstract void SetupInputBindings();
    }

    class GlobalScene : Scene
    {
        public override void Draw(DrawAdapter adapter)
        {
            
        }

        protected override void SetupInputBindings()
        {
            Bindings.Add(new InputBinding(new GamexInputKeyboardKey(GamexInputKeyboardKey.Directions.Down, Key.Q), (input) => AppController.ExitApp()));
            //Bindings.Add(new InputBinding(new GamexInputKeyboardKey(GamexInputKeyboardKey.Directions.Down, Key.T), (input) => GameFactoryController.LastCreated.Game.Camera.Zoom += 0.2f));
        }
    }
}
