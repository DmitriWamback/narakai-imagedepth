using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using StbImageSharp;

namespace NarakaiImageDepth {

    public class Texture {

        public static Vector3i LoadImage(string path) {

            int texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texture);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            int w, h;

            using(var stream = File.OpenRead(path)) {
                ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                w = image.Width;
                h = image.Height;

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
            }

            GL.BindTexture(TextureTarget.Texture2D, 0);
            return new Vector3i(texture, w, h);
        }

        public static void BindTextureToTarget(TextureUnit unit, int texture) {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, texture);
        }

        public static float[] ImageGrid(Vector3i textureData) {

            int width  = textureData.Y;
            int height = textureData.Z;

            float aspect = (float)width/(float)height;
            width = 400;
            height = (int)((float)width * aspect);
            
            List<float> grid = new List<float>();

            for (int x = 0; x < width; x++) { for (int y = 0; y < height; y++) {

                grid.Add(((float)x     / (float)width)   * aspect * 2f - 1);
                grid.Add(((float)y     / (float)height)  * 2f - 1);
                grid.Add(((float)(x+1) / (float)width)   * aspect * 2f - 1);
                grid.Add(((float)y     / (float)height)  * 2f - 1);
                grid.Add(((float)(x+1) / (float)width)   * aspect * 2f - 1);
                grid.Add(((float)(y+1) / (float)height)  * 2f - 1);

                grid.Add(((float)x     / (float)width)   * aspect * 2f - 1);
                grid.Add(((float)y     / (float)height)  * 2f - 1);
                grid.Add(((float)x     / (float)width)   * aspect * 2f - 1);
                grid.Add(((float)(y+1) / (float)height)  * 2f - 1);
                grid.Add(((float)(x+1) / (float)width)   * aspect * 2f - 1);
                grid.Add(((float)(y+1) / (float)height)  * 2f - 1);
            }}

            return grid.ToArray();
        }
    }
}