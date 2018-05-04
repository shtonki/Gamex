using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Gamex.src.XDGE;
using OpenTK.Graphics.OpenGL;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace Gamex.src.Util.ImageBinder
{
    static class ImageBinder
    {
        private static Dictionary<Sprites, int> GLIDs = new Dictionary<Sprites, int>();
        private static Dictionary<Sprites, Image> Images = new Dictionary<Sprites, Image>();

        static ImageBinder()
        {
            Images[Sprites.Dude1] = Properties.Resources.guy1;
            Images[Sprites.Brick1] = Properties.Resources.brick1;
            Images[Sprites.FloorWood1] = Properties.Resources.floorwood1;
        }

        public static void LoadAllTextures()
        {
            foreach (var image in Enum.GetValues(typeof (Sprites)).Cast<Sprites>())
            {
                GetBinding(image);
            }
        }

        /// <summary>
        /// Gets the int used by OpenGL to identify the texture bound from the given Images
        /// </summary>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public static int GetBinding(Sprites sprite)
        {
            if (GLIDs.ContainsKey(sprite))
            {
                return GLIDs[sprite];
            }

            var loaded = CreateTexture(Images[sprite]);
            GLIDs[sprite] = loaded;
            return loaded;
        }

        /// <summary>
        /// Binds an Image in OpenGL 
        /// </summary>
        /// <param name="image">The image to be bound to a texture</param>
        /// <returns>The integer used by OpenGL to identify the created texture</returns>
        private static int CreateTexture(Image image)
        {
            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            Bitmap bmp = new Bitmap(image);
            BitmapData data = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);

            return id;
        }
    }
}
