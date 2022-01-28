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

        public Quaternion Conjugate
        {
            get
            {
                return new Quaternion() { W = W, X = -X, Y = -Y, Z = -Z };
            }
        }

        public static readonly Quaternion Identity = new Quaternion() { X = 0, Y = 0, Z = 0, W = 1f };
        public Vector4 ToAxisAngle()
        {
            Vector4 result;

            float theta = 2 * MathHelper.Acos(W);

            if(theta == 1)
            {
                return new Vector4(){ X = 1,Y = 0 ,Z = 0, W = theta };
            }

            result.X = Vec.X / MathHelper.Sin(theta / 2);
            result.Y = Vec.Y / MathHelper.Sin(theta / 2);
            result.Z = Vec.Z / MathHelper.Sin(theta / 2);
            result.W = theta;

            return result;
        }
        public static Quaternion QuaternionFromPoint(Vector3 vec)
        {
            return QuaternionFromPoint(vec.X, vec.Y, vec.Z);
        }

        public static Quaternion QuaternionFromPoint(float x, float y, float z)
        {
            Quaternion result = Identity;

            result.X = x;
            result.Y = y;
            result.Z = z;
            result.W = 0;

            return result;
        }

        public static Vector3 RotatePoint(Vector3 angles, Vector3 point)
        {
            Quaternion p = QuaternionFromPoint(point);
            Quaternion qx = CreateRotation(Vector3.Right, angles.X);
            Quaternion qy = CreateRotation(Vector3.Down, angles.Y);
            Quaternion qz = CreateRotation(Vector3.Forward, angles.Z);
            
            p = qx * qy * qz * p * qx.Conjugate * qy.Conjugate * qz.Conjugate;

            return p.Vec;
        }

        public static Quaternion CreateRotation(Vector3 axis, float angle)
        {
            angle *= 0.5f;

            axis *= MathHelper.Sin(angle);
            
			return new Quaternion() { W = MathHelper.Cos(angle), Vec = axis };
        }

        public static Quaternion FromEuler(Vector3 vec)
        {
            return FromEuler(vec.X, vec.Y, vec.Z);
        }

        public static Quaternion FromEuler(float xRot, float yRot, float zRot)
        {
            Quaternion result = Identity;

            xRot *= 0.05f;
            yRot *= 0.05f;
            zRot *= 0.05f;

            float cosX = MathHelper.Cos(xRot);
            float cosY = MathHelper.Cos(yRot);
            float cosZ = MathHelper.Cos(zRot);

            float sinX = MathHelper.Sin(xRot);
            float sinY = MathHelper.Sin(yRot);
            float sinZ = MathHelper.Sin(zRot);

            result.W = (cosZ * cosY * cosX) + (sinZ * sinY * sinX);
            result.X = (sinZ * cosY * cosX) - (cosZ * sinY * sinX);
            result.Y = (cosZ * sinY * cosX) + (sinZ * cosY * sinX);
            result.Z = (cosZ * cosY * sinX) - (sinZ * sinY * cosX);

            return result;

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

        public void Invert()
        {
            X = -X;
            Y = -Y;
            Z = -Z;
        }
		/*
        public static Quaternion operator *(Quaternion q1, Quaternion q2)
        {
            Quaternion result = Quaternion.Identity;
            result.W = (q1.W * q2.W) - (q1.X * q2.X) - (q1.Y * q2.Y) - (q1.Z * q2.Z);
            result.X = (q1.W * q2.X) + (q1.X * q2.W) + (q1.Y * q2.Z) - (q1.Z * q2.Y);
            result.Y = (q1.W * q2.Y) - (q1.X * q2.Z) + (q1.Y * q2.W) + (q1.Z * q2.X);
            result.Z = (q1.W * q2.Z) + (q1.X * q2.Y) - (q1.Y * q2.X) + (q1.Z * q2.W);
            return result;
        }
		*/
		public static Quaternion operator *(Quaternion q1, Quaternion q2)
		{
			return new Quaternion()
			{
				W = q1.W * q2.W - Vector3.Dot(q1.Vec, q2.Vec),
				Vec = q1.W * q2.Vec + q2.W * q1.Vec + Vector3.Cross(q1.Vec, q2.Vec)
			};
		}
        public static Vector3 operator*(Quaternion q, Vector3 v)
        {
            Vector3 result;
            result.X = (q.W * v.X) + (q.Y * v.Z) - (q.Z * v.Y);
            result.Y = (q.W * v.Y) - (q.X * v.Z) + (q.Z * v.X);
            result.Z = (q.W * v.Z) + (q.X * v.Y) - (q.Y * v.X);

            return result;
        }
    }
}
