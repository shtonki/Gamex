using System;
using System.Collections.Generic;
using Gamex.src.Controller.Graphics;
using Gamex.src.Util.Coordinate;
using Gamex.src.Util.Logging;
using Gamex.src.XDGE;
using OpenTK.Input;

namespace Gamex.src.Controller.Input
{
    public delegate void GamexInputHandler(GamexInput input);

    public abstract class GamexInput : IEquatable<GamexInput>
    {
        public abstract bool Equals(GamexInput other);
    }

    public class GamexInputMouseMove : GamexInput
    {
        public PixelCoordinate CurrentLocation { get; }
        public PixelCoordinate PreviousLocation { get; }

        public GamexInputMouseMove(MouseMoveEventArgs args)
        {
            CurrentLocation = new PixelCoordinate(args.X, args.Y);
            PreviousLocation = new PixelCoordinate(args.X - args.XDelta, args.Y - args.YDelta);
        }

        public GamexInputMouseMove()
        {
        }

        public override bool Equals(GamexInput other)
        {
            return other is GamexInputMouseMove;
        }
    }

    public class GamexInputMouseButton : GamexInput
    {
        public enum Directions { Up, Down }

        public Directions Direction { get; }
        public MouseButton Button { get; }
        public GLCoordinate Location { get; }

        public GamexInputMouseButton(Directions direction, MouseButton button, GLCoordinate location)
        {
            Direction = direction;
            Button = button;
            Location = location;
        }

        public override bool Equals(GamexInput other)
        {
            var castOther = other as GamexInputMouseButton;
            if (castOther == null) return false;

            return
                castOther.Button == this.Button &&
                castOther.Direction == this.Direction;
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", Direction.ToString(), Button.ToString(), Location.ToString());
        }
    }

    public class GamexInputKeyboardKey : GamexInput
    {
        public enum Directions { Up, Down }
        public Directions Direction { get; }

        public OpenTK.Input.Key Key { get; }

        public GamexInputKeyboardKey(Directions direction, OpenTK.Input.Key key)
        {
            Direction = direction;
            Key = key;
        }


        public override bool Equals(GamexInput other)
        {
            var castOther = other as GamexInputKeyboardKey;
            if (castOther == null) return false;

            return
                castOther.Direction == this.Direction &&
                castOther.Key == this.Key;
        }
    }

    public struct InputBinding
    {
        public GamexInput Input { get; }
        public GamexInputHandler Handler { get; }

        public InputBinding(GamexInput input, GamexInputHandler handler)
        {
            Input = input;
            Handler = handler;
        }
    }

    public static class InputController
    {
        private static GlobalScene GlobalScene = new GlobalScene();

        public static void Initialize()
        {
            GraphicsController.Initialize();
            var window = GraphicsController.Window;
            if (window == null)
            {
                throw new Exception();
            }

            window.MouseDown += MouseDownHandler;
            window.MouseUp += MouseUpHandler;
            window.KeyDown += KeyDownHandler;
            window.KeyUp += KeyUpHandler;
            window.MouseMove += MouseMoveHandler;
        }



        private static void KeyUpHandler(object sender, KeyboardKeyEventArgs e)
        {
            DispatchInput(new GamexInputKeyboardKey(GamexInputKeyboardKey.Directions.Up, e.Key));
        }

        private static void MouseUpHandler(object sender, MouseButtonEventArgs e)
        {
            GamexWindow window = (GamexWindow)sender;
            var clickLocation = new PixelCoordinate(e.X, e.Y).ToGLCoordinate(window.ClientSize);
            var input = new GamexInputMouseButton(GamexInputMouseButton.Directions.Up, e.Button, clickLocation);
            DispatchInput(input);
        }

        private static void MouseMoveHandler(object sender, MouseMoveEventArgs e)
        {
            DispatchInput(new GamexInputMouseMove(e));
        }

        private static void KeyDownHandler(object sender, KeyboardKeyEventArgs e)
        {
            DispatchInput(new GamexInputKeyboardKey(GamexInputKeyboardKey.Directions.Down, e.Key));
        }

        private static void MouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            GamexWindow window = (GamexWindow)sender;
            var clickLocation = new PixelCoordinate(e.X, e.Y).ToGLCoordinate(window.ClientSize);
            var input = new GamexInputMouseButton(GamexInputMouseButton.Directions.Down, e.Button, clickLocation);
            DispatchInput(input);
        }

        private static void DispatchInput(GamexInput input)
        {
            if (GraphicsController.ActiveScene == null) return;
            if (!GraphicsController.ActiveScene.HandleInput(input))
            {
                GlobalScene.HandleInput(input);
            }
        }
    }
}
