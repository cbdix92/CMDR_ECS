using System;
using OpenGL;

namespace CMDR
{

    public struct Texture
    {

        public float[] ColorData;
        public int Width;
        public int Height;
		public uint ID;
        
        // How many floats per pixel
        public int Stride;

        public Texture(float[] colorData, int width, int height, int stride)
        {
            (ColorData, Width, Height, Stride) = (colorData, width, height, stride);
			
			ID = GL.GenTexture();
			GL.BindTexture(GL.TEXTURE_2D, ID);
			GL.TexImage2D(GL.TEXTURE_2D, 1, GL.RGBA, Width, Height, GL.RGBA, ColorData);

            // Texture Wrapping settings
            GL.TexParameteri(GL.TEXTURE_2D, GL.TEXTURE_WRAP_S, GL.REPEAT);
            GL.TexParameteri(GL.TEXTURE_2D, GL.TEXTURE_WRAP_T, GL.REPEAT);

            // Interpolation settings
            GL.TexParameteri(GL.TEXTURE_2D, GL.TEXTURE_MIN_FILTER, GL.NEAREST);
            GL.TexParameteri(GL.TEXTURE_2D, GL.TEXTURE_MAG_FILTER, GL.NEAREST);

            // Generate MipMaps
            // Doesn't work yet due to TEXTURE_MAG_FILTER setting.
            // Will fix this in the future.
            // GL.GenerateMipMap(GL.TEXTURE_2D);
			
			
			// Unbind Texture when finished
			GL.BindTexture(GL.TEXTURE_2D, 0);
        }

        public Pixel GetPixel(int x, int y)
        {
			// Adjust for zero based indexing and filter out negative numbers
			x = Math.Max(0, --x);
			y = Math.Max(0, --y);
			
            if (x > Width || y > Height)
                throw new ArgumentOutOfRangeException("The specified pixel is outside of the bounds of this texture.");

            float[] result = new float[Stride];
            for (int i = 0; i < Stride; i++)
            {
                result[i] = ColorData[(y * Width + x) + i];
            }

            return new Pixel() { Data = result };
        }
		
		public void Bind()
		{
			GL.BindTexture(GL.TEXTURE_2D, ID);
		}
    }

    public struct Pixel
    {
        public float[] Data;
		
		public float Red { get => Data[0]; }
		public float Green { get => Data[1]; }
		public float Blue { get => Data[2]; }
		public float Alpha { get => Data[3]; }
    }
}
