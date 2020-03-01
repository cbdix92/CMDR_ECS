using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDR.Components
{
    public struct Transform : IComponent<Transform>
    {
        #region IComponent
        public Component Handle { get; set; }
        public int ID { get; set; }
        public int Parent { get; set; }
        public Type Type { get; set; }
        public Scene Scene { get; set; }
        #endregion

        #region POSITION_PROPERTIES
        public float X { get; internal set; }
        public float Y { get; internal set; }
        public int Z { get; internal set; }
        #endregion

        public float Xvel { get; set; }
        public float Yvel { get; set; }

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
