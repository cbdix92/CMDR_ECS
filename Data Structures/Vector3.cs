using System;

namespace CMDR
{
    public struct Vector3 : IEquatable<Vector3>
    {

        public float X;
        public float Y;
        public float Z;


        public Vector3(float n)
        {
            (X, Y, Z) = (n, n, n);
        }

        public Vector3(float x, float y, float z)
        {
            (X, Y, Z) = (x, y, z);
        }
        public float this[int index]
        {
            get
            {
                if (index == 0)
                    return X;
                else if (index == 1)
                    return Y;
                else if (index == 2)
                    return Z;
                throw new IndexOutOfRangeException($"index of {index} is out of range of Vector4!");
            }
            set
            {
                if (index == 0)
                    X = value;
                else if (index == 1)
                    Y = value;
                else if (index == 2)
                    Z = value;
                throw new IndexOutOfRangeException($"index of {index} is out of range of Vector4!");
            }
        }

        public void Invert()
        {
            (X, Y, Z) = (-X, -Y, -Z);
        }

        public float Magnitude()
        {
            return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public static Vector3 Cross(Vector3 v1, Vector3 v2)
        {
            return new Vector3((v1.Y * v2.Z) - (v1.Z * v2.Y),
                               (v1.Z * v2.X) - (v1.X * v2.Z),
                               (v1.X * v2.Y) - (v1.Y * v2.X));
        }

        public static Vector3 Normalize(Vector3 v)
        {
            return v / v.Magnitude();
        }

        public static float Distance(Vector3 vec1, Vector3 vec2)
        {
            Vector3 result = vec1 - vec2;
            return result.Magnitude();
        }

        #region VECTOR_OPERATORS
        public static Vector3 operator +(Vector3 vec1, Vector3 vec2)
        {
            float x = vec1.X + vec2.X;
            float y = vec1.Y + vec2.Y;
            float z = vec1.Z + vec2.Z;
            return new Vector3() { X = x, Y = y, Z = z };
        }

        public static Vector3 operator -(Vector3 vec1, Vector3 vec2)
        {
            float x = vec1.X - vec2.X;
            float y = vec1.Y - vec2.Y;
            float z = vec1.Z - vec2.Z;
            return new Vector3() { X = x, Y = y, Z = z };
        }

        public static Vector3 operator *(Vector3 vec1, Vector3 vec2)
        {
            float x = vec1.X * vec2.X;
            float y = vec1.Y * vec2.Y;
            float z = vec1.Z * vec2.Z;
            return new Vector3() { X = x, Y = y, Z = z };
        }

        public static Vector3 operator /(Vector3 vec1, Vector3 vec2)
        {
            float x = vec1.X / vec2.X;
            float y = vec1.Y / vec2.Y;
            float z = vec1.Z / vec2.Z;
            return new Vector3() { X = x, Y = y, Z = z };
        }
        #endregion

        #region SCALAR_OPERATORS
        public static Vector3 operator +(Vector3 vec, float scalar)
        {
            Vector3 other = new Vector3(scalar);
            return vec + other;
        }
        
        public static Vector3 operator -(Vector3 vec, float scalar)
        {
            Vector3 other = new Vector3(scalar);
            return vec - other;
        }
        
        public static Vector3 operator *(Vector3 vec, float scalar)
        {
            Vector3 other = new Vector3(scalar);
            return vec * other;
        }
        
        public static Vector3 operator /(Vector3 vec, float scalar)
        {
            Vector3 other = new Vector3(scalar);
            return vec / other;
        }
        public static Vector3 operator +(float scalar, Vector3 vec)
        {
            Vector3 other = new Vector3(scalar);
            return vec + other;
        }

        public static Vector3 operator -(float scalar, Vector3 vec)
        {
            Vector3 other = new Vector3(scalar);
            return vec - other;
        }

        public static Vector3 operator *(float scalar, Vector3 vec)
        {
            Vector3 other = new Vector3(scalar);
            return vec * other;
        }

        public static Vector3 operator /(float scalar, Vector3 vec)
        {
            Vector3 other = new Vector3(scalar);
            return vec / other;
        }
        #endregion

        public bool Equals(Vector3 other)
        {
            return (X == other.X && Y == other.Y && Z == other.Z);
        }

        public float[] ToArray()
        {
            return new float[] { X, Y, Z };
        }
    }
}
