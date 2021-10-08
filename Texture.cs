using System;
using System.IO;
using System.Linq;
using System.Text;

namespace CMDR
{

    public struct Texture
    {

        public float[] ColorData;
        public int Width;
        public int Height;
        
        // How many floats per pixel
        public int Stride;

        internal Texture(float[] colorData, int width, int height, int stride)
        {
            (ColorData, Width, Height, Stride) = (colorData, width, height, stride);
        }

        public Pixel GetPixel(int x, int y)
        {
            if (x > Width || y > Height)
                throw new ArgumentOutOfRangeException("The specified pixel is outside of the bounds of this texture.");

            float[] result = new float[Stride];
            for (int i = 0; i < Stride; i++)
            {
                result[i] = ColorData[(y * Width + x) + i];
            }

            return new Pixel() { Data = result };
        }
    }

    public struct Pixel
    {
        public float[] Data;
    }
}
