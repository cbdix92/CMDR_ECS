using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDR
{
    public struct Matrix4 : IEquatable<Matrix4>
    {
        public Vector4 Row0;
        public Vector4 Row1;
        public Vector4 Row2;
        public Vector4 Row3;
		
		public static readonly Matrix4 Identity = new Matrix4( 1f, 0, 0, 0,
															   0, 1f, 0, 0,
															   0, 0, 1f, 0,
															   0, 0, 0, 1f );

        #region MATRIX_INDEXERS
        public float M00{ get => Row0.X; set { Row0.X = value; } }
        public float M01{ get => Row0.Y; set { Row0.Y = value; } }
        public float M02{ get => Row0.Z; set { Row0.Z = value; } }
        public float M03{ get => Row0.W; set { Row0.W = value; } }
        public float M10{ get => Row1.X; set { Row1.X = value; } }
        public float M11{ get => Row1.Y; set { Row1.Y = value; } }
        public float M12{ get => Row1.Z; set { Row1.Z = value; } }
        public float M13{ get => Row1.W; set { Row1.W = value; } }
        public float M20{ get => Row2.X; set { Row2.X = value; } }
        public float M21{ get => Row2.Y; set { Row2.Y = value; } }
        public float M22{ get => Row2.Z; set { Row2.Z = value; } }
        public float M23{ get => Row2.W; set { Row2.W = value; } }
        public float M30{ get => Row3.X; set { Row3.X = value; } }
        public float M31{ get => Row3.Y; set { Row3.Y = value; } }
        public float M32{ get => Row3.Z; set { Row3.Z = value; } }
        public float M33{ get => Row3.W; set { Row3.W = value; } }
        public Vector4 this[int index]
        {
            get
            {
                if (index == 0)
                    return Row0;
                else if (index == 1)
                    return Row1;
                else if (index == 2)
                    return Row2;
                else if (index == 3)
                    return Row3;

                throw new IndexOutOfRangeException($"index of {index} is out of range of Matrix4");
            }
            set
            {
                if (index == 0)
                    Row0 = value;
                else if (index == 1)
                    Row1 = value;
                else if (index == 2)
                    Row2 = value;
                else if (index == 3)
                    Row3 = value; ;
                throw new IndexOutOfRangeException($"index of {index} is out of range of Matrix4");
            }
        }

        #endregion

        #region CONTRUCTORS
		public Matrix4(Matrix4 matrix)
		{
			this = matrix;
		}
		
        public Matrix4(Vector4 row0, Vector4 row1, Vector4 row2, Vector4 row3)
        {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
            Row3 = row3;
        }

        public Matrix4
        (
            float m00, float m01, float m02, float m03,
            float m10, float m11, float m12, float m13,
            float m20, float m21, float m22, float m23,
            float m30, float m31, float m32, float m33
        )
        {
            Row0 = new Vector4(m00, m01, m02, m03);
            Row1 = new Vector4(m10, m11, m12, m13);
            Row2 = new Vector4(m20, m21, m22, m23);
            Row3 = new Vector4(m30, m31, m32, m33);
        }
        #endregion


        #region MATRIX_OPERATORS

        public static Matrix4 operator +(Matrix4 mat1, Matrix4 mat2)
        {
            Vector4 row0 = mat1.Row0 + mat2.Row0;
            Vector4 row1 = mat1.Row1 + mat2.Row1;
            Vector4 row2 = mat1.Row2 + mat2.Row2;
            Vector4 row3 = mat1.Row3 + mat2.Row3;
            return new Matrix4(row0, row1, row2, row3);
        }

        public static Matrix4 operator -(Matrix4 mat1, Matrix4 mat2)
        {
            Vector4 row0 = mat1.Row0 - mat2.Row0;
            Vector4 row1 = mat1.Row1 - mat2.Row1;
            Vector4 row2 = mat1.Row2 - mat2.Row2;
            Vector4 row3 = mat1.Row3 - mat2.Row3;
            return new Matrix4(row0, row1, row2, row3);
        }

        public static Matrix4 operator *(Matrix4 mat1, Matrix4 mat2)
        {
            Matrix4 result = new Matrix4
            {
                M00 = (mat1.M00 * mat2.M00) + (mat1.M01 * mat2.M10) + (mat1.M02 * mat2.M20) + (mat1.M03 * mat2.M30),
                M01 = (mat1.M00 * mat2.M01) + (mat1.M01 * mat2.M11) + (mat1.M02 * mat2.M21) + (mat1.M03 * mat2.M31),
                M02 = (mat1.M00 * mat2.M02) + (mat1.M01 * mat2.M12) + (mat1.M02 * mat2.M22) + (mat1.M03 * mat2.M32),
                M03 = (mat1.M00 * mat2.M03) + (mat1.M01 * mat2.M13) + (mat1.M02 * mat2.M23) + (mat1.M03 * mat2.M33),

                M10 = (mat1.M10 * mat2.M00) + (mat1.M11 * mat2.M10) + (mat1.M12 * mat2.M20) + (mat1.M13 * mat2.M30),
                M11 = (mat1.M10 * mat2.M01) + (mat1.M11 * mat2.M11) + (mat1.M12 * mat2.M21) + (mat1.M13 * mat2.M31),
                M12 = (mat1.M10 * mat2.M02) + (mat1.M11 * mat2.M12) + (mat1.M12 * mat2.M22) + (mat1.M13 * mat2.M32),
                M13 = (mat1.M10 * mat2.M03) + (mat1.M11 * mat2.M13) + (mat1.M12 * mat2.M23) + (mat1.M13 * mat2.M33),

                M20 = (mat1.M20 * mat2.M00) + (mat1.M21 * mat2.M10) + (mat1.M22 * mat2.M20) + (mat1.M23 * mat2.M30),
                M21 = (mat1.M20 * mat2.M01) + (mat1.M21 * mat2.M11) + (mat1.M22 * mat2.M21) + (mat1.M23 * mat2.M31),
                M22 = (mat1.M20 * mat2.M02) + (mat1.M21 * mat2.M12) + (mat1.M22 * mat2.M22) + (mat1.M23 * mat2.M32),
                M23 = (mat1.M20 * mat2.M03) + (mat1.M21 * mat2.M13) + (mat1.M22 * mat2.M23) + (mat1.M23 * mat2.M33),

                M30 = (mat1.M30 * mat2.M00) + (mat1.M31 * mat2.M10) + (mat1.M32 * mat2.M20) + (mat1.M33 * mat2.M30),
                M31 = (mat1.M30 * mat2.M01) + (mat1.M31 * mat2.M11) + (mat1.M32 * mat2.M21) + (mat1.M33 * mat2.M31),
                M32 = (mat1.M30 * mat2.M02) + (mat1.M31 * mat2.M12) + (mat1.M32 * mat2.M22) + (mat1.M33 * mat2.M32),
                M33 = (mat1.M30 * mat2.M03) + (mat1.M31 * mat2.M13) + (mat1.M32 * mat2.M23) + (mat1.M33 * mat2.M33)
            };

            return result;
        }

        public static Vector3 operator *(Matrix4 mat, Vector3 vec)
        {
            Vector3 result;
            result.X = mat.M00 * vec.X + mat.M01 * vec.Y + mat.M02 * vec.Z;
            result.Y = mat.M10 * vec.X + mat.M11 * vec.Y + mat.M12 * vec.Z;
            result.Z = mat.M20 * vec.X + mat.M21 * vec.Y + mat.M22 * vec.Z;
            return result;
        }
        public static Vector3 operator *(Vector3 vec, Matrix4 mat)
        {
            return mat * vec;
        }

        public static Vector4 operator *(Matrix4 mat, Vector4 vec)
        {
            Vector4 result;
            result.X = mat.M00 * vec.X + mat.M01 * vec.Y + mat.M02 * vec.Z + mat.M03 * vec.W;
            result.Y = mat.M10 * vec.X + mat.M11 * vec.Y + mat.M12 * vec.Z + mat.M13 * vec.W;
            result.Z = mat.M20 * vec.X + mat.M21 * vec.Y + mat.M22 * vec.Z + mat.M23 * vec.W;
            result.W = mat.M30 * vec.X + mat.M31 * vec.Y + mat.M32 * vec.Z + mat.M33 * vec.W;
            return result;
        }

        public static Vector4 operator *(Vector4 vec, Matrix4 mat)
        {
            return mat * vec;
        }


        #endregion

        public float Determinant()
        {
            return  + (M00 * M11 * M22 * M33) - (M00 * M11 * M23 * M32) - (M00 * M12 * M21 * M33) + (M00 * M12 * M23 * M31) + (M00 * M13 * M21 * M32) - (M00 * M13 * M22 * M31)
                    + (M01 * M10 * M22 * M33) - (M01 * M10 * M23 * M32) - (M01 * M12 * M20 * M33) + (M01 * M12 * M23 * M30) + (M01 * M13 * M21 * M32) - (M01 * M13 * M22 * M30)
                    + (M02 * M10 * M21 * M33) - (M02 * M10 * M23 * M31) - (M02 * M11 * M20 * M33) + (M02 * M11 * M23 * M30) + (M02 * M13 * M20 * M31) - (M02 * M13 * M21 * M30)
                    + (M03 * M10 * M21 * M32) - (M03 * M10 * M22 * M31) - (M03 * M11 * M20 * M32) + (M03 * M11 * M22 * M30) + (M03 * M12 * M20 * M31) - (M03 * M12 * M21 * M30);
        }
		
        /// <summary>
        /// Return a float[16] in row major for the current Matrix4.
        /// </summary>
        /// <returns></returns>
		public float[] ToArray()
		{
			return new float[]{
				M00, M01, M02, M03,
                M10, M11, M12, M13,
                M20, M21, M22, M23,
                M30, M31, M32, M33
            };
		}
		
        /// <summary>
        /// Return a float[16] in coulumn major for the current Matrix4.
        /// </summary>
        /// <returns></returns>
		public float[] ToArrayColumnMajor()
		{
			return new float[]{
				M00, M10, M20, M30,
				M01, M11, M21, M31,
				M02, M12, M22, M32,
				M03, M13, M23, M33
			};
		}

        public static Matrix4 CreatePerspective(float top, float bottom, float left, float right, float far, float near)
        {
            Matrix4 result = Matrix4.Identity;

            result.M00 = (2f * near) / (right - left);
            result.M11 = (2f * near) / (top - bottom);
            result.M22 = -(far + near) / (far - near);
            result.M02 = (right + left) / (right - left);
            result.M12 = (top + bottom) / (top - bottom);
            result.M23 = -(2f * far * near) / (far - near);
            result.M32 = -1f;

            return result;
        }
        public static Matrix4 CreatePerspectiveFOV(float fov, float aspect, float far, float near)
        {
            Matrix4 result = Identity;
            result.M00 = 1 / aspect * MathHelper.Tan((fov * 0.01745329f) / 2);
            result.M11 = 1 / MathHelper.Tan((fov * 0.01745329f) / 2);
            result.M22 = -((far + near) / (far - near));
            result.M32 = -1f;
            result.M23 = -((2 * far * near) / (far - near));
            return result;
        }

        public static Matrix4 CreateOrthographic(float top, float bottom, float left, float right, float far, float near)
        {
            Matrix4 result = Matrix4.Identity;

            /*
            float w_inv = 1.0f / (Right - Left);
            float h_inv = 1.0f / (Bottom - Top);
            float d_inv = 1.0f / (Far - Near);
            result.M00 = 2.0f * w_inv;
            result.M11 = 2.0f * h_inv;
            result.M22 = d_inv;
            result.M03 = -(Right + Left) * w_inv;
            result.M13 = -(Bottom + Top) * h_inv;
            result.M23 = -Near * d_inv;
			*/

            result.M00 = 2 / (right - left);
            result.M11 = 2 / (top - bottom);
            result.M22 = -(2 / (far - near));
            result.M03 = -((right + left) / (right - left));
            result.M13 = -((top + bottom) / (top - bottom));
            result.M23 = -((far + near) / (far - near));

            return result;
        }

        public static Matrix4 CreateTranslation(Vector3 vec)
        {
            return CreateTranslation(vec.X, vec.Y, vec.Z);
        }
		
        public static Matrix4 CreateTranslation(float x, float y, float z)
        {
			Matrix4 result = new Matrix4(Identity);
			result.Row0.W = x;
			result.Row1.W = y;
			result.Row2.W = z;
			return result;
        }
		
		public static Matrix4 CreateRotation(Vector3 vec)
		{
			return CreateRotation(vec.X, vec.Y, vec.Z);
		}
		
		public static Matrix4 CreateRotation(float x, float y, float z)
		{
			throw new NotImplementedException("CreateRotation");
		}
		
		public static Matrix4 CreateRotationX(float pitch)
		{
			Matrix4 result = Identity;
			result.M11 = MathHelper.Cos(pitch);
			result.M12 = -MathHelper.Sin(pitch);
			result.M21 = MathHelper.Sin(pitch);
			result.M22 = MathHelper.Cos(pitch);
			return result;
		}
		
		public static Matrix4 CreateRotationY(float yaw)
		{
			Matrix4 result = Identity;
			result.M00 = MathHelper.Cos(yaw);
			result.M02 = MathHelper.Sin(yaw);
			result.M20 = -MathHelper.Sin(yaw);
			result.M22 = MathHelper.Cos(yaw);
			return result;
		}
		
		public static Matrix4 CreateRotationZ(float roll)
		{
			Matrix4 result = Identity;
			result.M00 = MathHelper.Cos(roll);
			result.M01 = -MathHelper.Sin(roll);
			result.M10 = MathHelper.Sin(roll);
			result.M11 = MathHelper.Cos(roll);
			return result;
		}
		
		public static Matrix4 CreateScale(Vector3 vec)
		{
			return CreateScale(vec.X, vec.Y, vec.Z);
		}
		
		public static Matrix4 CreateScale(float x, float y, float z)
		{
			Matrix4 result = new Matrix4(Identity);
			result.Row0.X = x;
			result.Row1.Y = y;
			result.Row2.Z = z;
			return result;
		}

        public override string ToString()
        {
            return $"{M00}, {M01}, {M02}, {M03},\n{M10}, {M11}, {M12}, {M13},\n{M20}, {M21}, {M22}, {M23},\n{M30}, {M31}, {M32}, {M33}\n";
        }
        public bool Equals(Matrix4 other)
		{
			return Row0.Equals(other.Row0) && Row1.Equals(other.Row1) && Row2.Equals(other.Row2) && Row3.Equals(other.Row3);
		}


    }
}
