using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDR.Components
{
    public struct Transform : IComponent
    {
        #region IComponent
        public int Parent { get; set; }
        public int Handle { get; set; }
        public Type ID { get; set; }
        #endregion

        #region POSITION_PROPERTIES
        public float X { get; internal set; }
        public float Y { get; internal set; }
        public int Z { get; internal set; }
        #endregion

        public float Xvel { get; set; }
        public float Yvel { get; set; }

        public Transform(int parent, int handle, float initialX, float initialY, int initialZ)
        {
            // IComponent
            Parent = parent;
            Handle = handle;
            ID = typeof(Transform);

            X = initialX;
            Y = initialY;
            Z = initialZ;

            Xvel = 0;
            Yvel = 0;
        }
        public void Move()
        {
            // Move transform based on velocity
            X += Xvel;
            Y += Yvel;
        }
        public void Move(float x, float y)
        {
            X += x;
            Y += y;
        }
        public void Teleport(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
