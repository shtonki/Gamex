using System;
using System.Drawing;
using Gamex.src.Controller.Input;
using Gamex.src.Util.Coordinate;
using Gamex.src.GameModel;
using OpenTK.Input;
using Gamex.src.Controller.Graphics;
using Gamex.src.Util.Logging;

namespace Gamex.src.XDGE
{
    class GameScene : Scene
    {
        public GameState GameState { get; set; }

        public GameScene()
        {
        }

        protected override void SetupInputBindings()
        {
            Bindings.Add(new InputBinding(
                new GamexInputMouseButton(GamexInputMouseButton.Directions.Down, MouseButton.Right, null),
                HandleRightMouseDown));

            Bindings.Add(new InputBinding(new GamexInputMouseButton(GamexInputMouseButton.Directions.Up, MouseButton.Button1, null), HandleScrollUp));
            Bindings.Add(new InputBinding(new GamexInputMouseButton(GamexInputMouseButton.Directions.Up, MouseButton.Button2, null), HandleScrollDown));
            Bindings.Add(new InputBinding(new GamexInputMouseButton(GamexInputMouseButton.Directions.Up, MouseButton.Button3, null), HandleScrollUp));
            Bindings.Add(new InputBinding(new GamexInputMouseButton(GamexInputMouseButton.Directions.Up, MouseButton.Button4, null), HandleScrollDown));
        }

        private void HandleRightMouseDown(GamexInput input)
        {
            GamexInputMouseButton cinput = (GamexInputMouseButton)input;

            var c = new GameCoordinate(cinput.Location.X, cinput.Location.Y); // todo d8i7 this shouldn't be needed and shouldn't work and is probably because GameCoordinate is broken again
            GameState.Hero.MoveTo = c + GameState.Camera.Location;
        }

        private void HandleScrollDown(GamexInput input)
        {
            GameState.Camera.Zoom.ZoomOut();
        }

        private void HandleScrollUp(GamexInput input)
        {
            GameState.Camera.Zoom.ZoomIn();
        }

        public override void Draw(DrawAdapter adapter)
        {
            if (GameState != null)
            {
                var px = new PixelCoordinate(Mouse.GetState().X, Mouse.GetState().Y);
                var gl = px.ToGLCoordinate(GraphicsController.Window.ClientSize);
                var gm = GameCoordinate.FromGLCoordinate(gl, new GameCoordinate(0, 0));
                GameState.CurrentLevel.MouseHack.Location = gm + GameState.Camera.Location;

                var cameraLocation = GameState.Camera.Location;

                adapter.PushMatrix();
                adapter.Translate(new GameCoordinate(0, 0).ToGLCoordinate(cameraLocation));
                adapter.Scale(GameState.Camera.Zoom.CurrentZoom, GameState.Camera.Zoom.CurrentZoom);

                foreach (var v in GameState.CurrentLevel.Entities)
                {
                    v.Draw(adapter);
                }

                adapter.PopMatrix();
            }
        }
    }
}