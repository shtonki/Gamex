using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Gamex.src.Util.ImageBinder;
using Gamex.src.Controller.Debug;
using Gamex.src.Util.Coordinate;
using Gamex.src.Controller.GameFactory;
using OpenTK.Input;

namespace Gamex.src.XDGE
{
    class GamexWindow : GameWindow
    {
        public Action<DrawAdapter> RenderCallback { get; private set; }

        public GamexWindow(Action<DrawAdapter> renderCallback) : base(720, 720, new OpenTK.Graphics.GraphicsMode(32, 24, 0, 8))
        {
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            RenderCallback = renderCallback;
        }

        protected override void OnLoad(EventArgs e)
        {
            ImageBinder.LoadAllTextures();
            base.OnLoad(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color.CornflowerBlue);
            GL.PushMatrix();

            var drawAdapter = new DrawAdapter();
            RenderCallback(drawAdapter);
            

            SwapBuffers();
            GL.PopMatrix();

            if (Program.DEBUGMODE)
            {
                var pixelCoord = new PixelCoordinate(Mouse.GetState().X, Mouse.GetState().Y);
                var glCoord = pixelCoord.ToGLCoordinate(ClientSize);
                DebugController.MousePosPixel = pixelCoord;
                DebugController.MousePosGL = glCoord;
                if (GameFactoryController.LastCreated != null)
                {
                    var cameraLocation = GameFactoryController.LastCreated.Game.Camera.Location;
                    DebugController.MousePosGame = GameCoordinate.FromGLCoordinate(glCoord, cameraLocation);
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
        }
    }
}

