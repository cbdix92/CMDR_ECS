using System;

namespace CMDR
{
    public struct Vector2I : IEquatable<Vector2I>
    {

        public int X;
        public int Y;


        public Vector2I(int n)
        {
            (X, Y) = (n, n);
        }

        public Vector2I(int x, int y)
        {
            (X, Y) = (x, y);
        }
        public int this[int index]
        {
            get
            {
                if (index == 0)
                    return X;
                else if (index == 1)
                    return Y;
                throw new IndexOutOfRangeException($"index of {index} is out of range of Vector2I!");
            }
            set
            {
                if (index == 0)
                    X = value;
                else if (index == 1)
                    Y = value;
                throw new IndexOutOfRangeException($"index of {index} is out of range of Vector2I!");
            }
        }

        public int Magnitude()
        {
            return (int)Math.Sqrt(X * X + Y * Y);
        }

        #region STATIC_METHODS

        public static int Dot(Vector2I v1, Vector2I v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        public static Vector2I Normalize(Vector2I v)
        {
            return v / v.Magnitude();
        }

        public static int Distance(Vector2I vec1, Vector2I vec2)
        {
            Vector2I result = vec1 - vec2;
            return result.Magnitude();
        }

        #endregion

        #region VECTOR_OPERATORS
        public static Vector2I operator +(Vector2I vec1, Vector2I vec2)
        {
            int x = vec1.X + vec2.X;
            int y = vec1.Y + vec2.Y;
            return new Vector2I() { X = x, Y = y };
        }

        public static Vector2I operator -(Vector2I vec1, Vector2I vec2)
        {
            int x = vec1.X - vec2.X;
            int y = vec1.Y - vec2.Y;
            return new Vector2I() { X = x, Y = y };
        }

        public static Vector2I operator *(Vector2I vec1, Vector2I vec2)
        {
            int x = vec1.X * vec2.X;
            int y = vec1.Y * vec2.Y;
            return new Vector2I() { X = x, Y = y };
        }

        public static Vector2I operator /(Vector2I vec1, Vector2I vec2)
        {
            int x = vec1.X / vec2.X;
            int y = vec1.Y / vec2.Y;
            return new Vector2I() { X = x, Y = y };
        }
        #endregion

        #region SCALAR_OPERATORS
        public static Vector2I operator +(Vector2I vec, int scalar)
        {
            Vector2I other = new Vector2I(scalar);
            return vec + other;
        }

        public static Vector2I operator -(Vector2I vec, int scalar)
        {
            Vector2I other = new Vector2I(scalar);
            return vec - other;
        }

        public static Vector2I operator *(Vector2I vec, int scalar)
        {
            Vector2I other = new Vector2I(scalar);
            return vec * other;
        }

        public static Vector2I operator /(Vector2I vec, int scalar)
        {
            Vector2I other = new Vector2I(scalar);
            return vec / other;
        }
        public static Vector2I operator +(int scalar, Vector2I vec)
        {
            Vector2I other = new Vector2I(scalar);
            return vec + other;
        }

        public static Vector2I operator -(int scalar, Vector2I vec)
        {
            Vector2I other = new Vector2I(scalar);
            return vec - other;
        }

        public static Vector2I operator *(int scalar, Vector2I vec)
        {
            Vector2I other = new Vector2I(scalar);
            return vec * other;
        }

        public static Vector2I operator /(int scalar, Vector2I vec)
        {
            Vector2I other = new Vector2I(scalar);
            return vec / other;
        }
        #endregion

        public bool Equals(Vector2I other)
        {
            return (X == other.X && Y == other.Y);
        }

        public int[] ToArray()
        {
            return new int[] { X, Y };
        }

        public override string ToString()
        {
            return $"X:{X}, Y:{Y}";
        }
    }
}
