using System;
using Gamex.src.Controller.Graphics;
using Gamex.src.GameModel;
using Gamex.src.Util.Logging;
using Gamex.src.Util.Size;

namespace Gamex.src.Util.Coordinate
{
    public class GLCoordinate : IEquatable<GLCoordinate>
    {
        public float X { get; }
        public float Y { get; }

        public GLCoordinate(float x, float y)
        {
            X = x;
            Y = y;
        }

        public GLCoordinate(GLCoordinate clonee) : this(clonee.X, clonee.Y)
        {
        }

    public bool Equals(GLCoordinate other)
        {
            return other != null &&
                   other.X == this.X &&
                   other.Y == this.Y;
        }

        public bool CloseEnough(GLCoordinate other, float distance)
        {
            return
                Math.Abs(X - other.X) < distance &&
                Math.Abs(Y - other.Y) < distance;
        }

        public static GLCoordinate operator +(GLCoordinate a, GLCoordinate b)
        {
            return new GLCoordinate(a.X + b.X, a.Y + b.Y);
        }

        public static GLCoordinate operator -(GLCoordinate a)
        {
            return new GLCoordinate(-a.X, -a.Y);
        }

        public static GLCoordinate operator -(GLCoordinate a, GLCoordinate b)
        {
            return a + -b;
        }

        public override string ToString()
        {
            return String.Format("[{0:0.00} {1:0.00}]", X, Y);
        }
    }

    public class GameCoordinate : IEquatable<GameCoordinate>
    {
        public float X { get; } 
        public float Y { get; }

        public GameCoordinate(float x, float y)
        {
            X = x;
            Y = y;
        }

        public GameCoordinate Add(float x, float y)
        {
            return new GameCoordinate(X + x, Y + y);
        }

        public GLCoordinate ToGLCoordinate(GameCoordinate cameraLocation)
        {
            var x = X - cameraLocation.X;
            var y = -(Y - cameraLocation.Y);
            return new GLCoordinate(x, y);
        }

        public GLCoordinate ToGLCoordinate()
        {
            return ToGLCoordinate(new GameCoordinate(0, 0));
        }

        public static GameCoordinate FromGLCoordinate(GLCoordinate raw, GameCoordinate cameraLocation)
        {
            return new GameCoordinate(raw.X + cameraLocation.X, raw.Y + cameraLocation.Y);
        }


        public bool Equals(GameCoordinate other)
        {
            return other != null &&
                   other.X == this.X &&
                   other.Y == this.Y;
        }

        public static GameCoordinate operator +(GameCoordinate a, GameCoordinate b)
        {
            return new GameCoordinate(a.X + b.X, a.Y + b.Y);
        }

        public static GameCoordinate operator -(GameCoordinate a)
        {
            return new GameCoordinate(-a.X, -a.Y);
        }

        public static GameCoordinate operator -(GameCoordinate a, GameCoordinate b)
        {
            return a + -b;
        }

        public override string ToString()
        {
            return String.Format("[{0:0.00} {1:0.00}]", X, Y);
        }

        public float Distance(GameCoordinate other)
        {
            var xDistance = Math.Abs(X - other.X);
            var yDistance = Math.Abs(Y - other.Y);
            return (float)Math.Sqrt(xDistance*xDistance + yDistance*yDistance);
        }
    }

    public class PixelCoordinate
    {
        public int X { get; }
        public int Y { get; }

        public PixelCoordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static PixelCoordinate FromGLCoordinate(GLCoordinate @base, System.Drawing.Size windowSize)
        {
            var xpercentage = (@base.X + 1)/2;
            var ypercentage = (@base.Y + 1)/2;
            return new PixelCoordinate(
                (int)(xpercentage*windowSize.Width),
                (int)(ypercentage*windowSize.Height)
                );
        }

        public GLCoordinate ToGLCoordinate(System.Drawing.Size windowSize)
        {
            //NB opentk will generate mousemove events where
            //the cursor is outside the client area, eg when
            //the user holds down the mouse button, leading
            //to 'out of bounds' x and y values

            //[0..1]
            var scaledx = ((float)X) / windowSize.Width;
            var scaledy = ((float)Y) / windowSize.Height;

            //[-1..1]
            scaledx = scaledx * 2 - 1;
            scaledy = scaledy * 2 - 1;

            return new GLCoordinate(scaledx, scaledy);
        }

        public static PixelCoordinate operator +(PixelCoordinate a, PixelCoordinate b)
        {
            return new PixelCoordinate(a.X + b.X, a.Y + b.Y);
        }

        public static PixelCoordinate operator -(PixelCoordinate a)
        {
            return new PixelCoordinate(-a.X, -a.Y);
        }

        public static PixelCoordinate operator -(PixelCoordinate a, PixelCoordinate b)
        {
            return a + -b;
        }

        public override string ToString()
        {
            return String.Format("[{0} {1}]", X, Y);
        }
    }
}
