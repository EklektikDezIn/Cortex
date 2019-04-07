using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;   // For BitmMapData type

namespace NextGen
{
    class Texture
    {
        int[] skins = new int[6];
        //Path to resources

        public Texture(String Top, String Bottom, String Front, String Back, String Left, String Right)
        {
            //Multi Textured Object
            skins[0] = UploadTexture(Main.path[0] +" \\Resources\\" + Top + ".png");
            skins[1] = UploadTexture(Main.path[0] + "\\Resources\\" + Bottom + ".png");
            skins[2] = UploadTexture(Main.path[0] + "\\Resources\\" + Front + ".png");
            skins[3] = UploadTexture(Main.path[0] + "\\Resources\\" + Back + ".png");
            skins[4] = UploadTexture(Main.path[0] + "\\Resources\\" + Left + ".png");
            skins[5] = UploadTexture(Main.path[0] + "\\Resources\\" + Right + ".png");

        }
        public Texture(String Image)
        {//Single Textured Object
            String temp = Main.path[0] + "\\Resources\\" + Image + ".png";
            skins[0] = UploadTexture(temp);
            skins[1] = UploadTexture(temp);
            skins[2] = UploadTexture(temp);
            skins[3] = UploadTexture(temp);
            skins[4] = UploadTexture(temp);
            skins[5] = UploadTexture(temp);

        }

        public int getTop()
        {
            return skins[0];
        }

        public int getBottom()
        {
            return skins[1];
        }

        public int getFront()
        {
            return skins[2];
        }

        public int getBack()
        {
            return skins[3];
        }

        public int getLeft()
        {
            return skins[4];
        }

        public int getRight()
        {
            return skins[5];
        }
        public override string ToString()
        {
            return "(" + skins[0] + "," + skins[1] + "," + skins[2] + "," + skins[3] + "," + skins[4] + "," + skins[5] + ")";
        }
        static public int UploadTexture(string pathname)
        {// Create a new OpenGL texture object

            int id = GL.GenTexture();

            // Select the new texture
            GL.BindTexture(TextureTarget.Texture2D, id);

            // Load the image
            Bitmap bmp = new Bitmap(pathname);

            // Lock image data to allow direct access
            BitmapData bmp_data = bmp.LockBits(
                    new Rectangle(0, 0, bmp.Width, bmp.Height),
                    System.Drawing.Imaging.ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // Import the image data into the OpenGL texture
            GL.TexImage2D(TextureTarget.Texture2D,
                          0,
                          PixelInternalFormat.Rgba,
                          bmp_data.Width,
                          bmp_data.Height,
                          0,
                          OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                          OpenTK.Graphics.OpenGL.PixelType.UnsignedByte,
                          bmp_data.Scan0);

            // Unlock the image data
            bmp.UnlockBits(bmp_data);

            // Configure minification and magnification filters
            GL.TexParameter(TextureTarget.Texture2D,
                    TextureParameterName.TextureMinFilter,
                    (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D,
                    TextureParameterName.TextureMagFilter,
                    (int)TextureMagFilter.Linear);

            // Return the OpenGL object ID for use
            return id;
        }
    }

}
