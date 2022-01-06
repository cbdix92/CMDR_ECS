using System;

namespace CMDR
{
    public struct Quaternion
    {
        private Vector4 _vec4;

        public Quaternion(Vector4 vec4)
        {
            _vec4 = vec4;
        }

        public Quaternion(float angle)
        {
            angle *= 0.5f;
            float im = MathHelper.Sin(angle);
            _vec4.W = MathHelper.Cos(angle);
            _vec4.X = im;
            _vec4.Y = im;
            _vec4.Z = im;
        }
    }
}
