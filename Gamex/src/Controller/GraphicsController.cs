using System.Threading;
using Gamex.src.Util.Coordinate;
using Gamex.src.XDGE;
using Gamex.src.Util.Logging;
using OpenTK;

namespace Gamex.src.Controller.Graphics
{
    static class GraphicsController
    {
        public static Scene ActiveScene { get; set; }

        public static GamexWindow Window { get; set; }
        private static ManualResetEventSlim mre; //todo d2i2 hack

        /// <summary>
        /// Creates and displays a game window.
        /// </summary>
        public static void Initialize()
        {
            if (Window != null)
            {
                Logger.Default.Log("Tried to initialize GraphicsController when it has already been initialized.");
                return;
            }


            Thread t = new Thread(xd);
            t.Start();
            while (mre == null)
            {
                Thread.Sleep(1);
            }
            mre.Wait();
        }

        private static void xd()
        {
            Window = new GamexWindow(Render);
            Window.Visible = false;
            mre = new ManualResetEventSlim();
            Window.Load += (_, __) => mre.Set();
            Window.Run();
        }

        private static void Render(DrawAdapter drawAdapter)
        {
            if (ActiveScene != null)
            {
                ActiveScene.Draw(drawAdapter);
            }
        }
    }
}
