using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamex.src.Util.Size
{

    public class FSize 
    {
        public float Width { get; set; }
        public float Height { get; set; }


        protected FSize(float width, float height)
        {
            Width = width;
            Height = height;
        }
    }

    public class GLSize : FSize
    {
        public GLSize(float width, float height) : base(width, height)
        {
        }
    }

    public class GameSize : FSize
    {
        public GameSize(float width, float height) : base(width, height)
        {
        }

        public static explicit operator GLSize(GameSize gameSize)
        {
            return new GLSize(gameSize.Width, gameSize.Height);
        }
    }

    public class PixelSize
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public PixelSize(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
