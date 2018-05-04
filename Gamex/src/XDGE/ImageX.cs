using System.Drawing;
using Gamex.src.Util.ImageBinder;
using Gamex.src.Util.Size;

namespace Gamex.src.XDGE
{
    public enum Sprites
    {
        Dude1,

        Brick1,

        FloorWood1,
    }

    public class ImageX
    {
        public Color BrushColor { get; set; }
        public int GLTextureId { get; }
        public GLSize Size { get; set; }


        public ImageX(Sprites sprite, GLSize size)
        {
            GLTextureId = ImageBinder.GetBinding(sprite);
            BrushColor = Color.FloralWhite;
            Size = size;
        }

        public ImageX(Color brushColor, Sprites sprite, GLSize size)
        {
            BrushColor = brushColor;
            GLTextureId = ImageBinder.GetBinding(sprite);
            Size = size;
        }
    }
}
