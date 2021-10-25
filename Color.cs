using System;


namespace CMDR
{
    public struct Color : IEquatable<Color>
    {
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }
        public float A { get; set; }


        #region CONSTANTS

        public static readonly Color Black = new Color(255f);
        public static readonly Color White = new Color(0);
        public static readonly Color Red = new Color(255f, 0, 0, 255f);
        public static readonly Color Green = new Color(0, 255, 0, 255f);
        public static readonly Color Blue = new Color(0, 0, 255f, 255f);

        #endregion

        public Color(float n)
        {
            (R, G, B, A) = (n, n, n, n);
        }

        public Color(float r, float g, float b, float a)
        {
            (R, G, B, A) = (r, g, b, a);
        }

        #region COLOR_OPERATORS
        public static Color operator +(Color col1, Color col2)
        {
            float r = col1.R + col2.R;
            float g = col1.G + col2.G;
            float b = col1.B + col2.B;
            float a = col1.A + col2.A;
            return new Color() { R = r, G = g, B = b, A = a};
        }

        public static Color operator -(Color col1, Color col2)
        {
            float r = col1.R - col2.R;
            float g = col1.G - col2.G;
            float b = col1.B - col2.B;
            float a = col1.A - col2.A;
            return new Color() { R = r, G = g, B = b, A = a };
        }

        public static Color operator *(Color col1, Color col2)
        {
            float r = col1.R * col2.R;
            float g = col1.G * col2.G;
            float b = col1.B * col2.B;
            float a = col1.A * col2.A;
            return new Color() { R = r, G = g, B = b, A = a };
        }

        public static Color operator /(Color col1, Color col2)
        {
            float r = col1.R / col2.R;
            float g = col1.G / col2.G;
            float b = col1.B / col2.B;
            float a = col1.A / col2.A;
            return new Color() { R = r, G = g, B = b, A = a };
        }
        #endregion

        #region SCALAR_OPERATORS
        public static Color operator +(Color col, float scalar)
        {
            Color other = new Color(scalar);
            return col + other;
        }

        public static Color operator -(Color col, float scalar)
        {
            Color other = new Color(scalar);
            return col - other;
        }

        public static Color operator *(Color col, float scalar)
        {
            Color other = new Color(scalar);
            return col * other;
        }

        public static Color operator /(Color col, float scalar)
        {
            Color other = new Color(scalar);
            return col / other;
        }
        public static Color operator +(float scalar, Color col)
        {
            Color other = new Color(scalar);
            return col + other;
        }

        public static Color operator -(float scalar, Color col)
        {
            Color other = new Color(scalar);
            return col - other;
        }

        public static Color operator *(float scalar, Color col)
        {
            Color other = new Color(scalar);
            return col * other;
        }

        public static Color operator /(float scalar, Color col)
        {
            Color other = new Color(scalar);
            return col / other;
        }
        #endregion

        public bool Equals(Color other)
        {
            return (R == other.R && G == other.G && B == other.B && A == other.A);
        }
    }
}
