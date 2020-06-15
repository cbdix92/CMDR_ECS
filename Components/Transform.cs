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
        public int ID { get; set; }
        public int Parent { get; set; }
        public Type Type { get; set; }
        public Scene Scene { get; set; }
        #endregion

        #region POSITION_PROPERTIES
        private float _x, _y, _z;
        public float X
        {
            get => _x;
            internal set
            {
                _x = value;
                Update();
            }
        }
        public float Y
        {
            get => _y;
            internal set
            {
                _y = value;
                Update();
            }
        }
        public float Z
        {
            get => _z;
            internal set
            {
                _z = value;
                Update();
            }
        }
        #endregion

        #region VELOCITY_PROPERTIES
        private float _xvel, _yvel;
        public float Xvel
        {
            get => _xvel;
            set
            {
                _xvel = value;
                Update();
            }
        }
        public float Yvel
        {
            get => _yvel;
            set
            {
                _yvel = value;
                Update();
            }
        }
        #endregion

        public void Move()
        {
            // Move transform based on velocity
            X += Xvel;
            Y += Yvel;
            Update();
        }
        public void Move(float x, float y)
        {
            X += x;
            Y += y;
            Update();
        }
        public void Teleport(float x, float y)
        {
            X = x;
            Y = y;
            Update();
        }
        private void Update()
        {
            Data.Update<Transform>(this);
        }
    }
    
}
