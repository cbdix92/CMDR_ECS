using System;

namespace CMDR
{
    public struct Quaternion
    {
        public Vector3 Vec;
        public float W;

        public float X
        {
            get => Vec.X;
            set
            {
                Vec.X = value;
            }
        }
        public float Y
        {
            get => Vec.Y;
            set
            {
                Vec.Y = value;
            }
        }
        public float Z
        {
            get => Vec.Z;
            set
            {
                Vec.Z = value;
            }
        }

        public static readonly Quaternion Identity = new Quaternion() { X = 0, Y = 0, Z = 0, W = 1f };

        public Quaternion(Vector3 vec3)
            : this(vec3.X, vec3.Y, vec3.Z)
        {
        }

        public Quaternion(float xRot, float yRot, float zRot)
        {
            xRot *= 0.05f;
            yRot *= 0.05f;
            zRot *= 0.05f;

            float cosX = MathHelper.Cos(xRot);
            float cosY = MathHelper.Cos(yRot);
            float cosZ = MathHelper.Cos(zRot);

            float sinX = MathHelper.Sin(xRot);
            float sinY = MathHelper.Sin(yRot);
            float sinZ = MathHelper.Sin(zRot);

            W = (cosZ * cosY * cosX) + (sinZ * sinY * sinX);
            X = (sinZ * cosY * cosX) - (cosZ * sinY * sinX);
            Y = (cosZ * sinY * cosX) + (sinZ * cosY * sinX);
            Z = (cosZ * cosY * sinX) - (sinZ * sinY * cosX);

        }

        public Matrix4 ToMatrix()
        {
            Matrix4 result = Matrix4.Identity;

            float xSqr = 2f * (Vec.X * Vec.X);
            float ySqr = 2f * (Vec.Y * Vec.Y);
            float zSqr = 2f * (Vec.Z * Vec.Z);

            float xy = 2f * Vec.X * Vec.Y;
            float xz = 2f * Vec.X * Vec.Z;
            float xw = 2f * Vec.X * W;

            float yz = 2f * Vec.Y * Vec.Z;
            float yw = 2f * Vec.Y * W;

            float zw = 2f * Vec.Z * W;

            result.M00 = (1 - ySqr - zSqr);
            result.M01 = (xy - zw);
            result.M02 = (xz + yw);

            result.M10 = (xy + zw);
            result.M11 = (1 - xSqr - zSqr);
            result.M12 = (yz - xw);

            result.M20 = (xy - yw);
            result.M21 = (yz + xw);
            result.M22 = (1 - xSqr - ySqr);

            return result;
        }

        public void Normalize()
        {
            Vector4 result = Vector4.Normalize(new Vector4(Vec.X, Vec.Y, Vec.Z, W));
            (Vec.X, Vec.Y, Vec.Z, W) = (result.X, result.Y, result.Z, result.W);
        }

        public void Conjugate()
        {
            Vec.Invert();
        }

        public static Quaternion operator *(Quaternion q1, Quaternion q2)
        {
            Quaternion result = Quaternion.Identity;
            result.W = (q1.W * q2.W) - (q1.X * q2.X) - (q1.Y * q2.Y) - (q1.Z * q2.Z);
            result.X = (q1.W * q2.X) + (q1.X * q2.W) + (q1.Y * q2.Z) - (q1.Z * q2.Y);
            result.Y = (q1.W * q2.Y) - (q1.X * q2.Z) + (q1.Y * q2.W) + (q1.Z * q2.X);
            result.Z = (q1.W * q2.Z) + (q1.X * q2.Y) - (q1.Y * q2.X) + (q1.Z * q2.W);
            return result;
        }
    }
}
