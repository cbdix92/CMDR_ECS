﻿using System;
using System.IO;
using System.Linq;
using System.Text;
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

        internal Texture(float[] colorData, int width, int height, int stride)
        {
            (ColorData, Width, Height, Stride) = (colorData, width, height, stride);
			
			ID = GL.GenTexture();
			GL.BindTexture(GLenum.TEXTURE_2D, ID);
			GL.TexImage2D(GLenum.TEXTURE_2D, 0, 0, Width, Height, GLenum.RGBA, ColorData);
			GL.GenerateMipMap(GLenum.TEXTURE_2D);
			
			
			// Unbind Texture when finished
			GL.BindTexture(GLenum.TEXTURE_2D, 0);
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
		
		public void Bind()
		{
			GL.BindTexture(GLenum.TEXTURE_2D, ID);
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
