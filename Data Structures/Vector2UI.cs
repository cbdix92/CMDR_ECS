using System;

namespace CMDR
{
    public struct Vector2UI : IEquatable<Vector2UI>
    {

        public uint X;
        public uint Y;


        public Vector2UI(uint n)
        {
            (X, Y) = (n, n);
        }

        public Vector2UI(uint x, uint y)
        {
            (X, Y) = (x, y);
        }
        public uint this[int index]
        {
            get
            {
                if (index == 0)
                    return X;
                else if (index == 1)
                    return Y;
                throw new IndexOutOfRangeException($"index of {index} is out of range of Vector2UI!");
            }
            set
            {
                if (index == 0)
                    X = value;
                else if (index == 1)
                    Y = value;
                throw new IndexOutOfRangeException($"index of {index} is out of range of Vector2UI!");
            }
        }

        public uint Magnitude()
        {
            return (uint)Math.Sqrt(X * X + Y * Y);
        }

        #region STATIC_METHODS

        public static uint Dot(Vector2UI v1, Vector2UI v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        public static Vector2UI Normalize(Vector2UI v)
        {
            return v / v.Magnitude();
        }

        public static uint Distance(Vector2UI vec1, Vector2UI vec2)
        {
            Vector2UI result = vec1 - vec2;
            return result.Magnitude();
        }

        #endregion

        #region VECTOR_OPERATORS
        public static Vector2UI operator +(Vector2UI vec1, Vector2UI vec2)
        {
            uint x = vec1.X + vec2.X;
            uint y = vec1.Y + vec2.Y;
            return new Vector2UI() { X = x, Y = y };
        }

        public static Vector2UI operator -(Vector2UI vec1, Vector2UI vec2)
        {
            uint x = vec1.X - vec2.X;
            uint y = vec1.Y - vec2.Y;
            return new Vector2UI() { X = x, Y = y };
        }

        public static Vector2UI operator *(Vector2UI vec1, Vector2UI vec2)
        {
            uint x = vec1.X * vec2.X;
            uint y = vec1.Y * vec2.Y;
            return new Vector2UI() { X = x, Y = y };
        }

        public static Vector2UI operator /(Vector2UI vec1, Vector2UI vec2)
        {
            uint x = vec1.X / vec2.X;
            uint y = vec1.Y / vec2.Y;
            return new Vector2UI() { X = x, Y = y };
        }
        #endregion

        #region SCALAR_OPERATORS
        public static Vector2UI operator +(Vector2UI vec, uint scalar)
        {
            Vector2UI other = new Vector2UI(scalar);
            return vec + other;
        }

        public static Vector2UI operator -(Vector2UI vec, uint scalar)
        {
            Vector2UI other = new Vector2UI(scalar);
            return vec - other;
        }

        public static Vector2UI operator *(Vector2UI vec, uint scalar)
        {
            Vector2UI other = new Vector2UI(scalar);
            return vec * other;
        }

        public static Vector2UI operator /(Vector2UI vec, uint scalar)
        {
            Vector2UI other = new Vector2UI(scalar);
            return vec / other;
        }
        public static Vector2UI operator +(uint scalar, Vector2UI vec)
        {
            Vector2UI other = new Vector2UI(scalar);
            return vec + other;
        }

        public static Vector2UI operator -(uint scalar, Vector2UI vec)
        {
            Vector2UI other = new Vector2UI(scalar);
            return vec - other;
        }

        public static Vector2UI operator *(uint scalar, Vector2UI vec)
        {
            Vector2UI other = new Vector2UI(scalar);
            return vec * other;
        }

        public static Vector2UI operator /(uint scalar, Vector2UI vec)
        {
            Vector2UI other = new Vector2UI(scalar);
            return vec / other;
        }
        #endregion

        public bool Equals(Vector2UI other)
        {
            return (X == other.X && Y == other.Y);
        }

        public uint[] ToArray()
        {
            return new uint[] { X, Y };
        }

        public override string ToString()
        {
            return $"X:{X}, Y:{Y}";
        }
    }
}
