﻿using System;

namespace CMDR
{
    public struct Vector2 : IEquatable<Vector2>
    {

        public float X;
        public float Y;


        public Vector2(float n)
        {
            (X, Y) = (n, n);
        }

        public Vector2(float x, float y)
        {
            (X, Y) = (x, y);
        }
        public float this[int index]
        {
            get
            {
                if (index == 0)
                    return X;
                else if (index == 1)
                    return Y;
                throw new IndexOutOfRangeException($"index of {index} is out of range of Vector2!");
            }
            set
            {
                if (index == 0)
                    X = value;
                else if (index == 1)
                    Y = value;
                throw new IndexOutOfRangeException($"index of {index} is out of range of Vector2!");
            }
        }

        public float Magnitude()
        {
            return MathHelper.Sqrt(X * X + Y * Y);
        }
        public static Vector2 Normalize(Vector2 v)
        {
            return v / v.Magnitude();
        }
        public static float Distance(Vector2 vec1, Vector2 vec2)
        {
            Vector2 result = vec1 - vec2;
            return result.Magnitude();
        }
        #region VECTOR_OPERATORS
        public static Vector2 operator +(Vector2 vec1, Vector2 vec2)
        {
            float x = vec1.X + vec2.X;
            float y = vec1.Y + vec2.Y;
            return new Vector2() { X = x, Y = y };
        }

        public static Vector2 operator -(Vector2 vec1, Vector2 vec2)
        {
            float x = vec1.X - vec2.X;
            float y = vec1.Y - vec2.Y;
            return new Vector2() { X = x, Y = y };
        }

        public static Vector2 operator *(Vector2 vec1, Vector2 vec2)
        {
            float x = vec1.X * vec2.X;
            float y = vec1.Y * vec2.Y;
            return new Vector2() { X = x, Y = y };
        }

        public static Vector2 operator /(Vector2 vec1, Vector2 vec2)
        {
            float x = vec1.X / vec2.X;
            float y = vec1.Y / vec2.Y;
            return new Vector2() { X = x, Y = y };
        }
        #endregion

        #region SCALAR_OPERATORS
        public static Vector2 operator +(Vector2 vec, float scalar)
        {
            Vector2 other = new Vector2(scalar);
            return vec + other;
        }
        
        public static Vector2 operator -(Vector2 vec, float scalar)
        {
            Vector2 other = new Vector2(scalar);
            return vec - other;
        }
        
        public static Vector2 operator *(Vector2 vec, float scalar)
        {
            Vector2 other = new Vector2(scalar);
            return vec * other;
        }
        
        public static Vector2 operator /(Vector2 vec, float scalar)
        {
            Vector2 other = new Vector2(scalar);
            return vec / other;
        }
        public static Vector2 operator +(float scalar, Vector2 vec)
        {
            Vector2 other = new Vector2(scalar);
            return vec + other;
        }

        public static Vector2 operator -(float scalar, Vector2 vec)
        {
            Vector2 other = new Vector2(scalar);
            return vec - other;
        }

        public static Vector2 operator *(float scalar, Vector2 vec)
        {
            Vector2 other = new Vector2(scalar);
            return vec * other;
        }

        public static Vector2 operator /(float scalar, Vector2 vec)
        {
            Vector2 other = new Vector2(scalar);
            return vec / other;
        }
        #endregion

        public bool Equals(Vector2 other)
        {
            return (X == other.X && Y == other.Y);
        }
    }
}
